using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthorizePractice.DI;
using DAL.Repository;
using SharedLibrary.Attributes;
using SharedLibrary.Models;

namespace AuthorizePractice.Infra
{
    /// <summary>
    ///     透過 CustomAuthorizeAttribute 來驗証是否有進入 Action 的資格
    /// </summary>
    public class CustomControllerActionInvoker : ControllerActionInvoker
    {
        private readonly UserDto _userDto;

        public CustomControllerActionInvoker(UserDto userDto)
        {
            _userDto = userDto;
        }

        public override bool InvokeAction(ControllerContext controllerContext, string actionName)
        {
            ValidateCustomAuthorize(controllerContext, actionName);

            return base.InvokeAction(controllerContext, actionName);
        }

        /// <summary>
        ///     驗証是否符合 CustomAuthorizeAttribute 資格
        /// </summary>
        private void ValidateCustomAuthorize(ControllerContext controllerContext, string actionName)
        {
            var controllerDescriptor = GetControllerDescriptor(controllerContext);
            var action               = FindAction(controllerContext, controllerDescriptor, actionName);
            var attributeRoles = action.GetCustomAttributes(typeof(CustomAuthorizeAttribute), true)
                                       .Cast<CustomAuthorizeAttribute>()
                                       .SelectMany(attributes => attributes.Roles)
                                       .Distinct();

            if (attributeRoles.Any() == false)
            {
                return;
            }

            if (_userDto == null)
            {
                throw new CustomException { ErrorCode = HttpStatusCode.Unauthorized };
            }

            var authorizeRepository = DIResolver.GetService<IAuthorizeRepository>();
            var dto = new AuthorizationDto
                      {
                          UserId         = _userDto.UserId,
                          AttributeRoles = attributeRoles
                      };

            if (authorizeRepository.Auth(dto) == false)
            {
                throw new CustomException { ErrorCode = HttpStatusCode.Unauthorized };
            }
        }
    }
}