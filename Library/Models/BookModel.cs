using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(100, ErrorMessage = "Название должно содержать не более 100 символов")]
        [MinLength(5, ErrorMessage = "Название должно содержать не менее 5 символов")]
        public string Title { get; set; }

        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public AuthorModel Author { get; set; }
    }
}
