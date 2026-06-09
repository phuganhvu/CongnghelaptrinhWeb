using System.ComponentModel.DataAnnotations;

namespace Lesson3_CNLTWeb.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(200)]
        [Display(Name = "Tên sách")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(100)]
        [Display(Name = "Tác giả")]
        public string Author { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        [Display(Name = "Giá")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Ngày xuất bản")]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Today;
    }
}
