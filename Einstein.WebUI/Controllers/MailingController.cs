using System.Web.Mvc;

namespace Einstein.WebUI.Controllers
{
    [Authorize]
    public class MailingController : Controller
    {
        
        public ActionResult Index()
        {
            return PartialView();
        }

    }
}