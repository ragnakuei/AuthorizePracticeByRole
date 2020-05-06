using System.Web.Mvc;
using System.Web.Security;
using AuthorizePracticeByRole.Consts;
using AuthorizePracticeByRole.Infra;
using AuthorizePracticeByRole.Infra.Attributes;

namespace AuthorizePracticeByRole.Controllers
{
    public class MemberController : BaseController
    {
        [CustomAuthorize(RoleConst.Role1)]
        [CustomAuthorize(RoleConst.Role1, RoleConst.Role2)]
        public ActionResult Index()
        {
            var identity = User.Identity as FormsIdentity;
            ViewBag.user = identity?.Name;

            return View();
        }

        [CustomAuthorize(RoleConst.Role1)]
        public ActionResult Role1()
        {
            return View();
        }

        [CustomAuthorize(RoleConst.Role2)]
        public ActionResult Role2()
        {
            return View();
        }
    }
}