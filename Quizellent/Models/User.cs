namespace Quizellent.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Firstame { get; set; }
        public required string Lastname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
