using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private static List<Student> students = new List<Student>()
        {
            new Student
            {
                Id = 1,
                Name = "Nguyen Van A",
                Email = "a@gmail.com",
                Phone = "0123456789"
            },
            new Student
            {
                Id = 2,
                Name = "Tran Van B",
                Email = "b@gmail.com",
                Phone = "0987654321"
            },
            new Student
            {
                Id = 3,
                Name = "Le Thi C",
                Email = "c@gmail.com",
                Phone = "0912345678"
            }
        };

        public IActionResult Index(string? search, string sortBy = "Id", string sortOrder = "asc")
        {
            var query = students.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s =>
                    s.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    s.Email.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    s.Phone.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            query = sortBy.ToLower() switch
            {
                "name" => sortOrder == "desc"
                    ? query.OrderByDescending(s => s.Name)
                    : query.OrderBy(s => s.Name),
                "email" => sortOrder == "desc"
                    ? query.OrderByDescending(s => s.Email)
                    : query.OrderBy(s => s.Email),
                "phone" => sortOrder == "desc"
                    ? query.OrderByDescending(s => s.Phone)
                    : query.OrderBy(s => s.Phone),
                _ => sortOrder == "desc"
                    ? query.OrderByDescending(s => s.Id)
                    : query.OrderBy(s => s.Id)
            };

            ViewBag.Search = search;
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;

            return View(query.ToList());
        }

        public IActionResult Detail(int id)
        {
            var student = students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            student.Id = students.Count > 0 ? students.Max(x => x.Id) + 1 : 1;
            students.Add(student);
            TempData["SuccessMessage"] = "Thêm sinh viên thành công!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var student = students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            var existing = students.FirstOrDefault(x => x.Id == student.Id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = student.Name;
            existing.Email = student.Email;
            existing.Phone = student.Phone;

            TempData["SuccessMessage"] = "Cập nhật sinh viên thành công!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var student = students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            students.Remove(student);
            TempData["SuccessMessage"] = "Xóa sinh viên thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
