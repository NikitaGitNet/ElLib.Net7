using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElLib.Net7.Domain.Entities
{
    public class Book : EntityBase
    {
        [Required(ErrorMessage = "Заполните название книги")]
        [Display(Name = "Название книги")]
        public override string Title { get; set; }
        [Display(Name = "Краткое описание книги")]
        public override string SubTitle { get; set; }
        [Display(Name = "Полное описание книги")]
        public bool IsBooking { get; set; }
        public override string Text { get; set; }
        [Display(Name = "Автор книги, Заполнить в формате - Фамилия Имя Отчество")]
        public string AuthorName { get; set; }
        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        [Display(Name = "Жанр книги")]
        public string GenreName { get; set; }
        public Guid GenreId { get; set; }
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
