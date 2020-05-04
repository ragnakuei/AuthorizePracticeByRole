using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public partial class RouteRole
    {
        [Key]
        public int RouteId { get; set; }
        
        [Key]
        public int RoleId { get; set; }
        
        public System.DateTime Created { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual Route Route { get; set; }
    }
}
