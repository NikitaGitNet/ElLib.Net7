using System.Collections.Generic;
using System.Linq;

namespace ElLib.Net7.Models.Author
{
    public class AuthorListViewModel
    {
        public IEnumerable<AuthorViewModel> Authors { get; set; }
    }
}
