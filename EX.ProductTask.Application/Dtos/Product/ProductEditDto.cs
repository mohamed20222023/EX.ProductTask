using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Product
{
    public class ProductEditDto
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; } // Duration in minutes, hours, etc.
        public decimal Price { get; set; }
        public string URL { get; set; } // File name of the product image
		public int CategoryId { get; set; }
		public IFormFile File { get; set; }
		public string? CategoryName { get; set; }
        public string UserCreated { get; set; }


    }
}
