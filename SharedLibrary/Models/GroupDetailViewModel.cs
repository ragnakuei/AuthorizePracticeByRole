using System.Collections.Generic;
using SharedLibrary.Entities;

namespace SharedLibrary.Models
{
    public class GroupDetailViewModel
    {
        public Group  Group     { get; set; }
        public IEnumerable<string> UserNames { get; set; }
        public IEnumerable<string> RoleNames { get; set; }
    }
}