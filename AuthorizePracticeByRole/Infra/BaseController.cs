using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using SharedLibrary.Models;

namespace AuthorizePracticeByRole.Infra
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            if (System.Web.HttpContext.Current.User.Identity is FormsIdentity identity)
            {
                this.UserDto = JsonConvert.DeserializeObject<UserDto>(identity.Ticket.UserData);
                ViewBag.IsAuthenticated = identity.IsAuthenticated;
            }
        }

        protected UserDto UserDto { get; private set; }

        protected override IActionInvoker CreateActionInvoker()
        {
            return new CustomControllerActionInvoker(UserDto);
        }
    }
}