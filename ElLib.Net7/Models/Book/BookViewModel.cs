using ElLib.Net7.Domain.Entities;
using ElLib.Net7.Models.Comment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ElLib.Net7.Models.Book
{
    public class BookViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Заполните название книги")]
        [Display(Name = "Название книги")]
        public string Title { get; set; }
        [Display(Name = "Краткое описание книги")]
        public string SubTitle { get; set; }
        [Display(Name = "Полное описание книги")]
        public string Author { get; set; }
        [Display(Name = "Автор")]
        public string Genre { get; set; }
        [Display(Name = "Жанр")]
        public string TitleImagePath { get; set; }
        public string CurentUserId { get; set; }
        public string Text { get; set; }
        public bool IsBooking { get; set; }
        public string CommentText { get;set; }
        public DateTime DateAdded { get; set; }
        public IEnumerable<AddCommentModel> Comments { get; set; }
    }
}
