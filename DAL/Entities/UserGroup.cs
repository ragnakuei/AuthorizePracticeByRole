using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class UserGroup
    {
        [Key]
        public int UserId { get; set; }

        [Key]
        public int GroupId { get; set; }

        public System.DateTime Created { get; set; }

        public virtual Group Group { get; set; }

        public virtual User User { get; set; }
    }

    public class AuthResultDto
    {
        public int UserId { get; set; }
        
        public bool AuthResult { get; set; }
    }
}