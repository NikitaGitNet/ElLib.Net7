using System.Collections.Generic;
using System.Linq;
using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Models.Booking;
using System;
using ElLib.Net7.Models.Comment;

namespace ElLib.Net7.Models.User
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreateOn { get; set; }
        public IEnumerable<BookingViewModel> Bookings { get; set; }
        public IEnumerable<AddCommentModel> Comments { get; set; }
    }
}
