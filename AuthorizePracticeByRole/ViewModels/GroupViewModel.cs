using System.Collections.Generic;
using SharedLibrary.Entities;

namespace AuthorizePracticeByRole.ViewModels
{
    public class GroupViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        public int? EditId { get; set; }
    }
}