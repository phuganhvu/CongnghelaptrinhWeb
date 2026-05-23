using Microsoft.AspNetCore.Mvc;

namespace Bai2.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Info()
        {
            ViewBag.Name = "Vũ Thị Phương Anh";
            ViewData["Age"] = 20;
            string major = "Công nghệ thông tin";

            return View((object)major);
        }
    }
}
