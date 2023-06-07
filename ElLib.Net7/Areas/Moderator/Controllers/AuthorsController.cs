using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Domain.Interfaces;
using ElLib.Net7.Models.Author;
using ElLib.Net7.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElLib.Net7.Areas.Moderator.Controllers
{
    [Area("Moderator")]
    public class AuthorsController : Controller
    {
        private readonly IWebHostEnvironment hostingEnviroment;
        private readonly IRepository<Author> authorRepository;
        private readonly IRepository<Book> bookRepository;
        public AuthorsController(IWebHostEnvironment hostingEnviroment, IRepository<Author> authorRepository, IRepository<Book> bookRepository)
        {
            this.hostingEnviroment = hostingEnviroment;
            this.authorRepository = authorRepository;
            this.bookRepository = bookRepository;
        }
        public IActionResult AddAuthor(Guid id)
        {
            var entity = id == default ? new Author() : authorRepository.GetById(id);
            return View(entity);
        }
        [HttpPost]
        public IActionResult AddAuthor(Author model)
        {
            if (model.Id != default)
            {
                authorRepository.Save(model);
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }
            //if (ModelState.IsValid)
            //{
                IEnumerable<Author> authors = authorRepository.GetAll();
                var sortAuthors = from author in authors where author.Name.ToUpper() == model.Name.ToUpper() select author;
                if (sortAuthors.Any())
                {
                    return View("ErrorAuthor");
                }
                authorRepository.Save(model);
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            //}
            return View(model);
        }
        public IActionResult WarningDeleteAuthor(Guid id)
        {
            Author author = authorRepository.GetById(id);
            IEnumerable<Book> books = bookRepository.GetAll();
            var sortBooks = from book in books where book.AuthorId == author.Id select book;
            if (sortBooks.Any())
            {
                return View(new AuthorViewModel { Id = id, Name = author.Name });
            }
            return RedirectToAction(nameof(AuthorsController.DeleteAuthor), nameof(AuthorsController).CutController(), new AuthorViewModel { Id = id });
        }
        public IActionResult DeleteAuthor(AuthorViewModel model)
        {
            authorRepository.Delete(model.Id);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
    }
}
