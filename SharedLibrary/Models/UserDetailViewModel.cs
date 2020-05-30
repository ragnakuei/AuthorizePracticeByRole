using SharedLibrary.Entities;

namespace SharedLibrary.Models
{
    public class UserDetailViewModel
    {
        public User User { get; set; }

        public string[] GroupNames { get; set; }
    }
}