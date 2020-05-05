using System.Web.Mvc;
using System.Web.Security;
using AuthorizePractice.Infra;
using SharedLibrary.Attributes;

namespace AuthorizePractice.Controllers
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

        public ActionResult Role1()
        {
            return View();
        }

        public ActionResult Role2()
        {
            return View();
        }
    }
}