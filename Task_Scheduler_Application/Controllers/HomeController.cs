using Microsoft.AspNetCore.Mvc;

namespace Task_Scheduler_Application.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}