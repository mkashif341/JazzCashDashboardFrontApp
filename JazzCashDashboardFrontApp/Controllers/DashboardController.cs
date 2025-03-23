using Microsoft.AspNetCore.Mvc;

namespace JazzCashDashboardFrontApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult home()
        {
            return View();
        }

        public IActionResult analytics()
        {
            return View();
        }
    }
}
