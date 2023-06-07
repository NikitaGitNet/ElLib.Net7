using System.Collections.Generic;
using System.Linq;
namespace ElLib.Net7.Models.Genre
{
    public class GenresListViewModel
    {
        public IEnumerable<GenreViewModel> Genres { get; set; }
    }
}
