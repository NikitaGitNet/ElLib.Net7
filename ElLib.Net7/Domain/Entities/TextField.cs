using System.ComponentModel.DataAnnotations;

namespace ElLib.Net7.Domain.Entities
{
    public class TextField : EntityBase
    {
        [Required]
        public string CodeWord { get; set; }
        [Display(Name = "Название страницы (заголовок)")]
        public string SubTitle = "";
        public string TitleImagePath = "";
        public override string Title { get; set; } = "Информационная страница";
        [Display(Name = "Содержание страницы")]
        public override string Text { get; set; } = "Сейчас здесь пусто, содержание заполняется администратором";
    }
}
