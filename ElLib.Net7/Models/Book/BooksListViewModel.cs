using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Models.Author;
using ElLib.Net7.Models.Genre;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ElLib.Net7.Models.Book
{
    public class BooksListViewModel
    {
        public IEnumerable<BookViewModel> Books { get; set; }
        public IEnumerable<GenreViewModel> Genres { get; set; }
        public IEnumerable<AuthorViewModel> Authors { get; set; }
    }
}
