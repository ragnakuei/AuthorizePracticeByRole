using System.Collections.Generic;
using SharedLibrary.Entities;

namespace AuthorizePracticeByRole.ViewModels
{
    public class UserViewModel
    {
        public IList<User> Users { get; set; }

        public int? EditId { get; set; }
    }
}