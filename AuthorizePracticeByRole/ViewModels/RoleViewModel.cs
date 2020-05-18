using System.Collections.Generic;
using DAL.Entities;

namespace AuthorizePracticeByRole.ViewModels
{
    public class RoleViewModel
    {
        public IEnumerable<Role> Roles { get; set; }

        public int? EditId { get; set; }
    }
}