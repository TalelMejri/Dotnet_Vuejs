using Microsoft.AspNetCore.Mvc;

namespace DashboardElsaWorflow.Controllers
{
    public class ElsaDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
