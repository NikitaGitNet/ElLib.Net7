using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ElLib.Net7.Domain.Interfaces;
using ElLib.Net7.Models.Book;

namespace ElLib.Net7.Models.ViewCompanents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly IRepository<Domain.Entities.Book> bookRepository;
        public SidebarViewComponent(IRepository<Domain.Entities.Book> bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Domain.Entities.Book> books = bookRepository.GetAll();
            var sortBooks = from book in books orderby book.DateAdded descending select book;
            List<BookViewModel> booksViewModel = new();
            int count = 0;
            foreach (var item in sortBooks)
            {
                if (count < 5)
                {
                    BookViewModel book = new()
                    {
                        Author = item.AuthorName,
                        Genre = item.GenreName,
                        Id = item.Id,
                        IsBooking = item.IsBooking,
                        SubTitle = item.SubTitle,
                        Text = item.Text,
                        Title = item.Title,
                        TitleImagePath = item.TitleImagePath
                    };
                    booksViewModel.Add(book);
                    count++;
                }
            }
            return Task.FromResult((IViewComponentResult)View("Default", new BooksListViewModel {Books = booksViewModel }));
        }
    }
}
