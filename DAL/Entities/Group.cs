using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public System.DateTime Created { get; set; }

        public virtual ICollection<GroupRole> GroupRoles { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}