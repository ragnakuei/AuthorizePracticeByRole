using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Route
    {
        public Route()
        {
            this.RouteRoles = new HashSet<RouteRole>();
        }

        [Key]
        public int Id { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public System.DateTime Created { get; set; }
    
        public virtual ICollection<RouteRole> RouteRoles { get; set; }
    }
}
