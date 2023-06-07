using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Domain.Interfaces;
using ElLib.Net7.Models.Author;
using ElLib.Net7.Models.Book;
using ElLib.Net7.Models.Genre;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ElLib.Net7.Areas.Moderator.Controllers
{
    [Area("Moderator")]
    public class HomeController : Controller
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Genre> genreRepository;
        private readonly IRepository<Author> authorRepository;
        public HomeController(IRepository<Book> bookRepository, IRepository<Genre> genreRepository, IRepository<Author> authorRepository)
        {
            this.bookRepository = bookRepository;
            this.genreRepository = genreRepository;
            this.authorRepository = authorRepository;
        }
        public IActionResult Index()
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
                    SubTitle = book.SubTitle,
                    Title = book.Title,
                    Text = book.Text,
                    TitleImagePath = book.TitleImagePath,
                };
                booksViewModels.Add(bookViewModel);
            }
            IEnumerable<Genre> genres = genreRepository.GetAll();
            var sortGenres = from genre in genres orderby genre.Name select genre;
            List<GenreViewModel> genreViewModels = new();
            foreach (var genre in sortGenres)
            {
                GenreViewModel model = new()
                {
                    Id = genre.Id,
                    Name = genre.Name
                };
                genreViewModels.Add(model);
            }
            IEnumerable<Author> authors = authorRepository.GetAll();
            var sortAuthors = from author in authors orderby author.Name select author;
            List<AuthorViewModel> authorVeiwModels = new();
            foreach (var author in sortAuthors)
            {
                AuthorViewModel model = new()
                {
                    Id = author.Id,
                    Name = author.Name
                };
                authorVeiwModels.Add(model);
            }
            return View(new BooksListViewModel { Books = booksViewModels, Authors = authorVeiwModels, Genres = genreViewModels });
        }
    }
}
