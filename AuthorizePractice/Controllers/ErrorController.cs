using System.Web.Mvc;

namespace AuthorizePractice.Controllers
{
    public class ErrorController : Controller
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