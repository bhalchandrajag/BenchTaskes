using Microsoft.AspNetCore.Mvc;

namespace EudHubConsume.MVC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
