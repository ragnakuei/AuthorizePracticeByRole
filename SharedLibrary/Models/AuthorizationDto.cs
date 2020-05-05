using System.Collections.Generic;
using SharedLibrary.Attributes;

namespace SharedLibrary.Models
{
    public class AuthorizationDto
    {
        public int UserId { get; set; }
        public IEnumerable<string> AttributeRoles { get; set; }
    }
}