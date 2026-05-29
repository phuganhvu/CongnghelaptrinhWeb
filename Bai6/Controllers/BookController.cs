using Bai6.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bai6.Controllers
{
    public class BookController : Controller
    {
        // Danh sách giả lập
        static List<Book> books = new List<Book>()
        {
            new Book{ Id = 1, Name = "Clean Code", Price = 20 },
            new Book{ Id = 2, Name = "ASP.NET MVC", Price = 15 },
            new Book{ Id = 3, Name = "Design Pattern", Price = 25 }
        };

        // Hiển thị danh sách
        public IActionResult Index()
        {
            return View(books);
        }

        // Chi tiết sách
        public IActionResult Detail(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);

            return View(book);
        }

        // GET: Hiển thị form thêm sách
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Xử lý thêm sách
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                books.Add(book);

                // Quay về trang danh sách
                return RedirectToAction("Index");
            }

            return View(book);
        }
    }
}