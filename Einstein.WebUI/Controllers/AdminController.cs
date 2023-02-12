using System.Web.Mvc;

namespace Einstein.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        
        public ActionResult Index()
        {
            return RedirectToAction("GridView", "Order");
        }
        [Authorize(Roles = "1")]
        public ActionResult Settings()
        {
            return View();
        }

    }
}