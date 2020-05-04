using System.ComponentModel.DataAnnotations;

namespace AuthorizePractice.Models
{
    public class LoginForm 
    {
        public string ReturnUrl { get; set; }

        [Required]
        public string Account { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
