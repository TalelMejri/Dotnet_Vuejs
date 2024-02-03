using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class ElsaDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
