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
        
        [MaxLength(50)]
        public string Account { get; set; }
        
        [MaxLength(100)]
        public string Password { get; set; }
        public string Name { get; set; }
        public System.DateTime Created { get; set; }
    
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
