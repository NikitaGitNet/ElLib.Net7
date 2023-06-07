using System.Collections.Generic;
namespace ElLib.Net7.Areas.Admin.Models
{
    public class UsersListViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
