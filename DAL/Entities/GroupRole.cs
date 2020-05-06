using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class GroupRole
    {
        public int GroupId { get; set; }

        public int RoleId { get; set; }

        public System.DateTime Created { get; set; }

        public virtual Group Group { get; set; }

        public virtual Role Role { get; set; }
    }
}