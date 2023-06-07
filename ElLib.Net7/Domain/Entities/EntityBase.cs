using System;
using System.ComponentModel.DataAnnotations;

namespace ElLib.Net7.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase() => DateAdded = DateTime.UtcNow;
        [Required]
        public Guid Id { get; set; }
        [Display(Name ="Название (заголовок)")]
        public virtual string Title { get; set; }
        [Display(Name = "Краткое описание")]
        public virtual string SubTitle { get; set; }
        [Display(Name = "Полное описание")]
        public virtual string Text { get; set; }
        [Display(Name = "Титульная картинка")]
        public virtual string TitleImagePath { get; set; }
        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }
    }
}
