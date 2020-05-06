using System.Collections.Generic;

namespace SharedLibrary.Models
{
    public class AuthorizationDto
    {
        public int UserId { get; set; }
        public IEnumerable<string> AttributeRoles { get; set; }
    }
}