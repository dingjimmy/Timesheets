using Microsoft.AspNetCore.Mvc;

namespace Timesheets.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
