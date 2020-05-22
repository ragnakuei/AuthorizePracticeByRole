using System.Collections.Generic;
using SharedLibrary.Entities;

namespace AuthorizePracticeByRole.ViewModels
{
    public class RoleViewModel
    {
        public IEnumerable<Role> Roles { get; set; }

        public int? EditId { get; set; }
    }
}