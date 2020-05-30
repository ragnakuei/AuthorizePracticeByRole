using System.Collections.Generic;
using SharedLibrary.Entities;

namespace SharedLibrary.Models
{
    public class GroupDetailViewModel
    {
        public Group Group { get; set; }

        public string[] UserNames { get; set; }

        public string[] RoleNames { get; set; }
    }
}