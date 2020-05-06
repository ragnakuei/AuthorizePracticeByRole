using System.Web.Mvc;
using AuthorizePracticeByRole.Infra;

namespace AuthorizePracticeByRole.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}