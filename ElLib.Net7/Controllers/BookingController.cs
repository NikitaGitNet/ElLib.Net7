using ElLib.Net7.Domain;
using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Domain.Interfaces;
using ElLib.Net7.Models.Book;
using ElLib.Net7.Models.Booking;
using ElLib.Net7.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElLib.Net7.Controllers
{
    public class BookingController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment hostingEnviroment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Booking> bookingRepository;
        public BookingController(IWebHostEnvironment hostingEnviroment, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IRepository<ApplicationUser> userRepository, IRepository<Book> bookRepository, IRepository<Booking> bookingRepository)
        {
            this.userRepository = userRepository;
            this.hostingEnviroment = hostingEnviroment;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.bookRepository = bookRepository;
            this.bookingRepository = bookingRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Booking(BookViewModel model)
        {
            string userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationUser user = userRepository.GetById(new Guid(userId));
            if (user == null)
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            Book book = bookRepository.GetById(model.Id);
            if (book == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (user.Bookings.Count < 5)
            {
                book.IsBooking = true;
                Booking booking = new()
                {
                    BooksTitle = book.Title,
                    BookId = model.Id,
                    UserEmail = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value,
                    CreateOn = DateTime.Now,
                    FinishedOn = DateTime.Now.AddDays(3),
                    UserId = userId,
                };
                bookingRepository.Save(booking);
                bookRepository.Save(book);
                return View(new BookingViewModel { BookId = booking.BookId, CreateOn = booking.CreateOn, FinishedOn = booking.FinishedOn, Id = booking.Id, UserEmail = booking.UserEmail, UserId = booking.UserId, BooksTitle = booking.BooksTitle });
            }
            return View("LimitBooking");
        }
        [HttpPost]
        public IActionResult Delete(Booking booking)
        {
            Book book = bookRepository.GetById(booking.BookId);
            bookingRepository.Delete(booking.Id);
            book.IsBooking = false;
            bookRepository.Save(book);
            return RedirectToAction(nameof(BookingShowController.Index), nameof(BookingShowController).CutController());
        }
        
    }
}
