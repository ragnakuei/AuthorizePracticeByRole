using System.Web.Mvc;
using System.Web.Security;
using AuthorizePracticeByRole.Infra;
using SharedLibrary.Attributes;

namespace AuthorizePracticeByRole.Controllers
{
    public class MemberController : BaseController
    {
        [CustomAuthorize("Role1")]
        [CustomAuthorize("Role1", "Role2")]
        public ActionResult Index()
        {
            var identity = User.Identity as FormsIdentity;
            ViewBag.user = identity?.Name;

            return View();
        }

        [CustomAuthorize("Role1")]
        public ActionResult Role1()
        {
            return View();
        }

        [CustomAuthorize("Role2")]
        public ActionResult Role2()
        {
            return View();
        }
    }
}