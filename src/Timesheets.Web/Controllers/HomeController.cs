using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Timesheets.Web.Models.Shared;

namespace Timesheets.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["Header"] = new HeaderViewModel();
            ViewData["Footer"] = new FooterViewModel();
            
            base.OnActionExecuting(context);
        }
    }
}
