using System.Web.Mvc;
using AuthorizePracticeByRole.Infra;

namespace AuthorizePracticeByRole.Controllers
{
    public class ErrorController : BaseController
    {
        [HttpGet]
        public ActionResult AccessDenied()
        {
            ViewBag.Title = "AccessDenied";
            return View("CommonTemplate");
        }
        
        [HttpGet]
        public ActionResult NotFound()
        {
            ViewBag.Title = "NotFound";
            return View("CommonTemplate");
        }
    }
}