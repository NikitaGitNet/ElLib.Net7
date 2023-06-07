using System.Collections.Generic;
using System.Linq;
namespace ElLib.Net7.Models.Booking
{
    public class BookingListViewModel
    {
        public IEnumerable<BookingViewModel> Bookings { get; set; }
    }
}
