using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AuthorizePracticeByRole.Infra.Attributes;

namespace AuthorizePracticeByRole.Infra.Helpers
{
    public static class DescriptorHelpers
    {
        public static IEnumerable<string> GetControllerCustomAttributes(this ControllerDescriptor controller)
        {
            return controller.GetCustomAttributes(typeof(CustomAuthorizeAttribute), true)
                             .Cast<CustomAuthorizeAttribute>()
                             .SelectMany(attributes => attributes.Roles);
        }

        public static IEnumerable<string> GetActionCustomAttributes(this ActionDescriptor action)
        {
            return action.GetCustomAttributes(typeof(CustomAuthorizeAttribute), true)
                         .Cast<CustomAuthorizeAttribute>()
                         .SelectMany(attributes => attributes.Roles);
        }
    }
}