using ElLib.Net7.Areas.Admin.Models;
using ElLib.Net7.Domain;
using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Domain.Interfaces;
using ElLib.Net7.Models.Booking;
using ElLib.Net7.Models.Comment;
using ElLib.Net7.Models.User;
using ElLib.Net7.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElLib.Net7.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersShowController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Booking> bookingRepository;
        private readonly IRepository<Comment> commentRepository;
        public UsersShowController(UserManager<ApplicationUser> userManager, IRepository<Book> bookRepository, IRepository<ApplicationUser> userRepository, IRepository<Booking> bookingRepository, IRepository<Comment> commentRepository)
        {
            this.userManager = userManager;
            this.bookRepository = bookRepository;
            this.userRepository = userRepository;
            this.bookingRepository = bookingRepository;
            this.commentRepository = commentRepository;
        }
        public IActionResult UsersShow()
        {
            IEnumerable<ApplicationUser> users = userRepository.GetAll();
            var sortUsers = from user in users orderby user.UserName select user;
            List<UserViewModel> usersViewModel = new();
            foreach (var user in sortUsers)
            {
                UserViewModel userViewModel = new() 
                {
                    Email = user.Email,
                    CreateOn = user.CreateOn,
                    Id = user.Id,
                    UserName = user.UserName,
                    Bookings = user.Bookings,
                    Comments = user.Comments
                };
                usersViewModel.Add(userViewModel);
            }
            return View(new UsersListViewModel {Users = usersViewModel });
        }
        public IActionResult ShowCurentUser(UserModel model)
        {
            if (model != null)
            {
                ApplicationUser user = userRepository.GetById(new Guid(model.Id));
                if (user.Bookings.Any())
                {
                    List<BookingViewModel> bookings = new();
                    foreach (var i in user.Bookings)
                    {
                        BookingViewModel booking = new()
                        {
                            IssueBooking = i.IssueBooking,
                            UserEmail = i.UserEmail,
                            UserId = user.Id,
                            CreateOn = i.CreateOn,
                            FinishedOn = i.FinishedOn,
                            BookId = i.BookId,
                            BooksTitle = i.BooksTitle,
                            Id = i.Id
                        };
                        bookings.Add(booking);
                    }
                    return View(new UserModel { Id = user.Id, UserEmail = user.Email, UserName = user.UserName, Bookings = bookings, CreateOn = user.CreateOn });
                }
                return View(new UserModel { Id = user.Id, UserEmail = user.Email, UserName = user.UserName, Bookings = null, CreateOn = user.CreateOn, Comments = null });
            }
            return RedirectToAction(nameof(UsersShowController.UsersShow), nameof(UsersShowController).CutController());
        }
        [HttpPost]
        public IActionResult SearchByEmail(UserModel model)
        {
            if (model.UserEmail != null)
            {
                IEnumerable<ApplicationUser> users = userRepository.GetAll();
                var sortUsers = from user in users where user.Email.ToUpper().Contains(model.UserEmail.ToUpper()) select user;
                List<UserViewModel> userViewModels = new();
                foreach (var user in sortUsers)
                {
                    UserViewModel userViewModel = new()
                    { 
                        Email = user.Email,
                        UserName = user.UserName,
                        CreateOn = user.CreateOn,
                        Id = user.Id
                    };
                    userViewModels.Add(userViewModel);
                }
                return View("UsersShow", new UsersListViewModel { Users = userViewModels });
            }
            return RedirectToAction(nameof(UsersShowController.UsersShow), nameof(UsersShowController).CutController());
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserModel model)
        {
            ApplicationUser user = userRepository.GetById(new Guid(model.Id));
            if (user.Bookings.Any())
            {
                foreach (var booking in user.Bookings)
                {
                    Guid bookId = booking.BookId;
                    Book book = bookRepository.GetById(bookId);
                    book.IsBooking = false;
                    bookRepository.Save(book);
                }
                bookingRepository.DeleteRange(model.Id);
            }
            if (user.Comments.Any())
            {
                commentRepository.DeleteRange(model.Id);
            }
            await userManager.IsLockedOutAsync(user);
            await userManager.DeleteAsync(user);
            return View("Delete");
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserModel model)
        {
            ApplicationUser user = await userManager.FindByIdAsync(model.Id);
            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, model.Password);
            await userManager.UpdateAsync(user);
            return View(new UserModel {Password = model.Password});
        }
    }
}
