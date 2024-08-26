using System.ComponentModel.DataAnnotations;

namespace Quizellent.Models
{
    public class LoginViewModel
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Emailname { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
