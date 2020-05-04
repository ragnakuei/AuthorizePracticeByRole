using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class GroupRole
    {
        [Key]
        public int GroupId { get; set; }
        
        [Key]
        public int RoleId { get; set; }
        
        public System.DateTime Created { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual Role Role { get; set; }
    }
}
