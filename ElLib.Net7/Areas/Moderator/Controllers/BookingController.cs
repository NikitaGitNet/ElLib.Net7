using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Domain.Interfaces;
using ElLib.Net7.Models.Booking;
using ElLib.Net7.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElLib.Net7.Areas.Moderator.Controllers
{
    [Area("Moderator")]
    public class BookingController : Controller
    {
        private readonly IRepository<Booking> bookingRepository;
        private readonly IRepository<Book> bookRepository;
        public BookingController(IRepository<Booking> bookingRepository, IRepository<Book> bookRepository)
        {
            this.bookingRepository = bookingRepository;
            this.bookRepository = bookRepository;
        }
        public IActionResult Show(Guid id)
        {
            if (id != default)
            {
                try
                {
                    Booking booking = bookingRepository.GetById(id);
                    return View("CurentBookingShow", new BookingViewModel { BookId = booking.BookId, BooksTitle = booking.BooksTitle, FinishedOn = booking.FinishedOn, CreateOn = booking.CreateOn, Id = booking.Id, IssueBooking = booking.IssueBooking, UserEmail = booking.UserEmail, UserId = booking.UserId });
                }
                catch
                {
                    return View("ErrorPage");
                }
            }
            IEnumerable<Booking> bookings = bookingRepository.GetAll();
            var sortBookings = from booking in bookings orderby booking.CreateOn select booking;
            List<BookingViewModel> bookingViewModels = new();
            foreach (var booking in sortBookings)
            {
                BookingViewModel bookingModel = new()
                { 
                    FinishedOn = booking.FinishedOn,
                    BookId = booking.BookId,
                    BooksTitle = booking.BooksTitle,
                    CreateOn = booking.CreateOn,
                    Id = booking.Id,
                    IssueBooking = booking.IssueBooking,
                    UserEmail = booking.UserEmail,
                    UserId = booking.UserId
                };
                bookingViewModels.Add(bookingModel);
            }
            return View(new BookingListViewModel {Bookings = bookingViewModels });
        }
        [HttpPost]
        public IActionResult IssueBooking(Guid id)
        {
            Booking booking = bookingRepository.GetById(id);
            booking.IssueBooking = true;
            booking.FinishedOn = DateTime.Now.AddDays(7);
            bookingRepository.Save(booking);
            return View(new BookingViewModel {FinishedOn=booking.FinishedOn, Id = booking.Id,  BooksTitle = booking.BooksTitle, CreateOn = booking.CreateOn, BookId = booking.BookId, IssueBooking = booking.IssueBooking, UserEmail = booking.UserEmail, UserId = booking.UserId });
        }
        [HttpPost]
        public IActionResult Delete(BookingViewModel booking)
        {
            Book book = bookRepository.GetById(booking.BookId);
            bookingRepository.Delete(booking.Id);
            book.IsBooking = false;
            bookRepository.Save(book);
            return RedirectToAction(nameof(BookingController.Show), nameof(BookingController).CutController());
        }
    }
}
