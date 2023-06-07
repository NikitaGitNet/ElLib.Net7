using ElLib.Net7.Domain;
using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Domain.Interfaces;
using ElLib.Net7.Models.Genre;
using ElLib.Net7.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElLib.Net7.Areas.Moderator.Controllers
{
    [Area("Moderator")]
    public class GenresController : Controller
    {
        private readonly IRepository<Genre> genreRepository;
        private readonly IWebHostEnvironment hostingEnviroment;
        private readonly IRepository<Book> bookRepository;
        public GenresController(IRepository<Genre> genreRepository, IWebHostEnvironment hostingEnviroment, IRepository<Book> bookRepository)
        {
            this.genreRepository = genreRepository;
            this.hostingEnviroment = hostingEnviroment;
            this.bookRepository = bookRepository;
        }
        public IActionResult AddGenre(Guid id)
        {
            var entity = id == default ? new Genre() : genreRepository.GetById(id);
            return View(entity);
        }
        [HttpPost]
        public IActionResult AddGenre(Genre model)
        {
            if (model.Id != default)
            {
                genreRepository.Save(model);
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }
            //if (ModelState.IsValid)
            //{
                IEnumerable<Genre> genres = genreRepository.GetAll();
                var sortGenres = from genre in genres where genre.Name.ToUpper() == model.Name.ToUpper() select genre;
                if (sortGenres.Any())
                {
                    return View("ErrorGenre");
                }
                genreRepository.Save(model);
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            //}
            return View(model);
        }
        public IActionResult WarningDeleteGenre(Guid id)
        {
            Genre genre = genreRepository.GetById(id);
            IEnumerable<Book> books = bookRepository.GetAll();
            var sortBooks = from book in books where book.GenreId == genre.Id select book;
            if (sortBooks.Any())
            {
                return View(new GenreViewModel { Id = id, Name = genre.Name });
            }
            return RedirectToAction(nameof(GenresController.DeleteGenre), nameof(GenresController).CutController(), new GenreViewModel { Id = id });
        }
        public IActionResult DeleteGenre(GenreViewModel model)
        {
            genreRepository.Delete(model.Id);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
    }
}
