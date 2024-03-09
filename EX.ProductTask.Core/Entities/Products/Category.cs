using Core.Common;

namespace Core.Entities.Products
{
	public class Category : BaseId
    {
        public string Name { get; set; }

        // Navigation property
        public ICollection<Product> Products { get; set; }
    }
}
