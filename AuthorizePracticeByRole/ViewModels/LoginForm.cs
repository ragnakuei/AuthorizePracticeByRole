using System.ComponentModel.DataAnnotations;

namespace AuthorizePracticeByRole.ViewModels
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
