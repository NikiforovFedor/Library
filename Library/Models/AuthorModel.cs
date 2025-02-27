using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class AuthorModel 
    {
        [Column("Name")]
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(30, ErrorMessage = "Имя должно содержать не более 15 символов")]
        [MinLength(5, ErrorMessage = "Имя должно содержать не менее 5 символов")]
        [RegularExpression(@"^[А-ЯЁ]\. [А-ЯЁ]\. [А-ЯЁа-яё\s-]+$", ErrorMessage = "Некорректное данные")]
        public string Name { get; set; }
        public int Id { get; set; }
        public List<BookModel> Books { get; set; }
    }
}
