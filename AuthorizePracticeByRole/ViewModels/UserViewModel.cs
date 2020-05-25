using System.Collections.Generic;
using SharedLibrary.Entities;

namespace AuthorizePracticeByRole.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<Group> Users { get; set; }

        public int? EditId { get; set; }
    }
}