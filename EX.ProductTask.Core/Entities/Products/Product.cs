using Core.Common;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Products
{
	public class Product : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; } // Duration in minutes, hours, etc.
        public decimal Price { get; set; }
        public string URL { get; set; } // File name of the product image



        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        // Foreign Key to Category

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
