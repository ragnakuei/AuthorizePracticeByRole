using System;

namespace SharedLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : Attribute
    {
        public CustomAuthorizeAttribute(params string[] roles)
        {
            Roles = roles;
        }

        public string[] Roles { get; }
    }
}