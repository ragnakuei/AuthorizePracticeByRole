using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Role
    {
        public Role()
        {
            this.GroupRoles = new HashSet<GroupRole>();
        }
    
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Remark { get; set; }
        
        public Nullable<System.DateTime> Created { get; set; }
    
        public virtual ICollection<GroupRole> GroupRoles { get; set; }
    }
}
