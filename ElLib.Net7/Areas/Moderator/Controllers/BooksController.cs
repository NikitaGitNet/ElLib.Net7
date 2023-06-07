using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Domain.Interfaces;
using ElLib.Net7.Models.Author;
using ElLib.Net7.Models.Book;
using ElLib.Net7.Models.Genre;
using ElLib.Net7.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ElLib.Net7.Areas.Moderator.Controllers
{
    [Area("Moderator")]
    public class BooksController : Controller
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Author> authorRepository;
        private readonly IRepository<Genre> genreRepository;
        private readonly IWebHostEnvironment hostingEnviroment;
        public BooksController(IRepository<Book> bookRepository, IWebHostEnvironment hostingEnviroment, IRepository<Author> authorRepository, IRepository<Genre> genreRepository)
        {
            this.bookRepository = bookRepository;
            this.hostingEnviroment = hostingEnviroment;
            this.authorRepository = authorRepository;
            this.genreRepository = genreRepository;
        }
        public IActionResult Edit(Guid id)
        {
            var entity = id == default ? new Book() : bookRepository.GetById(id);
            return View(entity);
        }
        [HttpPost]
        public IActionResult Edit(Book model, IFormFile titleImageFile)
        {
            //if (ModelState.IsValid)
            //{
                if (titleImageFile != null)
                {
                    model.TitleImagePath = titleImageFile.FileName;
                    using (FileStream stream = new FileStream(Path.Combine(hostingEnviroment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                if (model.AuthorName != null)
                {
                    IEnumerable<Author> authors = authorRepository.GetAll();
                    var sortAuthors = from author in authors where model.AuthorName.ToUpper() == author.Name.ToUpper() select author;
                    if (sortAuthors.Any())
                    {
                        model.AuthorName = sortAuthors.First().Name;
                        model.AuthorId = sortAuthors.First().Id;
                    }
                    else
                    {
                        Author author = new(){Name = model.AuthorName, Id = new Guid()};
                        authorRepository.Save(author);
                        model.AuthorName = author.Name;
                        model.AuthorId = author.Id;
                    }
                }
                else
                {
                    model.AuthorName = UnknownAuthor.Name;
                    model.AuthorId = new Guid(UnknownAuthor.Id);
                }
                if (model.GenreName != null)
                {
                    IEnumerable<Genre> genres = genreRepository.GetAll();
                    var sortGenres = from genre in genres where model.GenreName == genre.Name select genre;
                    if (sortGenres.Any())
                    {
                        model.GenreId = sortGenres.First().Id;
                        model.GenreName = sortGenres.First().Name;
                    }
                    else
                    {
                        Genre genre = new() { Name = model.GenreName, Id = new Guid() };
                        genreRepository.Save(genre);
                        model.GenreName = genre.Name;
                        model.GenreId = genre.Id;
                    }
                }
                else
                {
                    model.GenreName = UnknownGenre.Name;
                    model.GenreId = new Guid(UnknownGenre.Id);
                }
                model.DateAdded = DateTime.Now;
                bookRepository.Save(model);
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
           //}
            return View(model);
        }
        public IActionResult SearchByName(BookViewModel model)
        {
            if (model.Title != null)
            {
                IEnumerable<Book> books = bookRepository.GetAll();
                var sortBooks = from book in books where book.Title.ToUpper().Contains(model.Title.ToUpper()) select book;
                List<BookViewModel> bookViewModels = new();
                foreach (var book in sortBooks)
                {
                    BookViewModel bookViewModel = new()
                    {
                        Title = book.Title,
                        Id = book.Id,
                        Author = book.AuthorName,
                        Genre = book.GenreName,
                        IsBooking = book.IsBooking,
                        TitleImagePath = book.TitleImagePath
                    };
                    bookViewModels.Add(bookViewModel);
                }
                return View("BooksShow", new BooksListViewModel { Books = bookViewModels});
            }
            return RedirectToAction(nameof(BooksController.BooksShow), nameof(BooksController).CutController());
        }
        public IActionResult BooksShow()
        {
            IEnumerable<Book> books = bookRepository.GetAll();
            var sortBooks = from book in books orderby book.Title select book;
            List<BookViewModel> booksViewModels = new();
            foreach (Book book in sortBooks)
            {
                BookViewModel bookViewModel = new()
                {
                    Author = book.AuthorName,
                    Genre = book.GenreName,
                    Id = book.Id,
                    IsBooking = book.IsBooking,
                    Title = book.Title,
                    TitleImagePath = book.TitleImagePath,
                };
                booksViewModels.Add(bookViewModel);
            }
            return View(new BooksListViewModel{Books = booksViewModels});
        }
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            bookRepository.Delete(id);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
    }
}
