using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElLib.Net7.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime FinishedOn { get; set; }
        public bool IssueBooking { get; set; }
        public string UserEmail { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public Guid BookId { get; set; }
        [ForeignKey("Id")]
        public string BooksTitle { get; set; }
        public Book Book { get; set; }
        

    }
}
