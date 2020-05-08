using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AuthorizePracticeByRole.DI;
using AuthorizePracticeByRole.Infra.Helpers;
using DAL.Repository.@interface;
using SharedLibrary.Models;

namespace AuthorizePracticeByRole.Infra
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
            
            var actionDescriptor = FindAction(controllerContext, controllerDescriptor, actionName)
                                ?? controllerDescriptor.GetCanonicalActions()
                                                       .FirstOrDefault(a => a.ActionName == actionName);
            
            var attributeRoles = controllerDescriptor.GetControllerCustomAttributes()
                                                     .Concat(actionDescriptor.GetActionCustomAttributes())
                                                     .Distinct()
                                                     .ToArray();

            if (attributeRoles.Length > 0 == false)
            {
                return;
            }

            if (_userDto == null)
            {
                throw new CustomException { ErrorCode = HttpStatusCode.Unauthorized };
            }

            var authorizeRepository = DiResolver.GetService<IAuthorizeRepository>();
            var dto = new AuthorizationDto
                      {
                          UserId         = _userDto.UserId,
                          AttributeRoles = attributeRoles
                      };

            if (authorizeRepository.Auth(dto) == false)
            {
                throw new CustomException
                      {
                          ErrorCode       = HttpStatusCode.Unauthorized,
                          IsAuthenticated = _userDto != null
                      };
            }
        }
    }
}