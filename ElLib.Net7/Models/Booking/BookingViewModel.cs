using System;

namespace ElLib.Net7.Models.Booking
{
    public class BookingViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime FinishedOn { get; set; }
        public bool IssueBooking { get; set; }
        public string UserEmail { get; set; }
        public string UserId { get; set; }
        public Guid BookId { get; set; }
        public string BooksTitle { get; set; }
    }
}
