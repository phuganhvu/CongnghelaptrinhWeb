using Microsoft.AspNetCore.Mvc;

namespace Bai2.Controllers
{
    public class ProductController : Controller
    {
        // Action 1: Nhận tham số id từ Route
        // Ví dụ: /Product/Detail/5
        public IActionResult Detail(int? id)
        {
            // Kiểm tra xem có truyền id hay không
            if (!id.HasValue)
            {
                ViewBag.ErrorMessage = "Lỗi: Vui lòng cung cấp Product ID trên đường dẫn (ví dụ: /Product/Detail/5)";
                return View();
            }

            ViewBag.ProductId = id.Value;
            return View();
        }

        // Action 2: Nhận tham số name từ Query String
        // Ví dụ: /Product/Category?name=Laptop
        public IActionResult Category(string name)
        {
            // Kiểm tra xem có truyền name hay không
            if (string.IsNullOrWhiteSpace(name))
            {
                ViewBag.ErrorMessage = "Lỗi: Vui lòng cung cấp tên danh mục (ví dụ: /Product/Category?name=Laptop)";
                return View();
            }

            ViewBag.CategoryName = name;
            return View();
        }
    }
}
