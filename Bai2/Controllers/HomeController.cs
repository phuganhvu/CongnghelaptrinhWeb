using Microsoft.AspNetCore.Mvc;

namespace Bai2.Controllers
{
    public class HomeController : Controller
    {
        // GET /Home/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET /Home/About
        // Replace the sample name below with the actual student name.
        public IActionResult About()
        {
            return View();
        }

        // GET /Home/Contact
        // Replace the sample email below with the actual student email.
        public IActionResult Contact()
        {
            return View();
        }
        //GET /Home/New
        public IActionResult New()
        {
            return View();

        }

    }
}
