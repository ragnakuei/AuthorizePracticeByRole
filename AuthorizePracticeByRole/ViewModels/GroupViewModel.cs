using System.Collections.Generic;
using DAL.Entities;

namespace AuthorizePracticeByRole.ViewModels
{
    public class GroupViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        public int? EditId { get; set; }
    }
}