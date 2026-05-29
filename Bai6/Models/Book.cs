using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bai6.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string Name { get; set; }

        [Range(1, double.MaxValue,
            ErrorMessage = "Giá phải lớn hơn 0")]
        public double Price { get; set; }
    }
}
