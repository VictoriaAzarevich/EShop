namespace ProductAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public required Category Category { get; set; }
        public string? ImageUrl { get; set; }
    }
}
