using System;

namespace AuthorizePracticeByRole.Infra.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : Attribute
    {
        public CustomAuthorizeAttribute(params string[] roles)
        {
            Roles = roles;
        }

        public string[] Roles { get; }
    }
}