using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class User
    {
        public User()
        {
            this.UserGroups = new HashSet<UserGroup>();
        }
    
        [Key]
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public System.DateTime Created { get; set; }
    
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
