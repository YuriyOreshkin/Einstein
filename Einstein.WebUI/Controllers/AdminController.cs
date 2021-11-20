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

        public ActionResult Settings()
        {
            return View();
        }

    }
}