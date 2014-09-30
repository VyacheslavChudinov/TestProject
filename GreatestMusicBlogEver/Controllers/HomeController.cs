using System.Web.Mvc;

namespace Service.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllSongs()
        {
            return View();            
        }
    }
}
