using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Product
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string URL { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
        public IFormFile File { get; set; }


    }
}
