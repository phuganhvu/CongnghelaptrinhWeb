using Microsoft.AspNetCore.Mvc;
using UploadImageDemo.Models;

namespace UploadImageDemo.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new();
        private readonly IWebHostEnvironment _environment;

        public ProductController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                string extension = Path.GetExtension(imageFile.FileName).ToLower();

                if (extension != ".jpg" &&
                    extension != ".jpeg" &&
                    extension != ".png")
                {
                    ViewBag.Error = "Chỉ cho phép file JPG hoặc PNG";
                    return View();
                }

                string fileName = Guid.NewGuid().ToString()
                                  + extension;

                string uploadFolder = Path.Combine(
                    _environment.WebRootPath,
                    "images"
                );
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string filePath = Path.Combine(
                    uploadFolder,
                    fileName
                );

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                product.ImagePath = fileName;
            }

            product.Id = products.Count + 1;
            products.Add(product);

            return RedirectToAction("Index");
        }
    }
}