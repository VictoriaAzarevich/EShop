using System.ComponentModel.DataAnnotations;

namespace EShop.Services.ProductAPI.Models.Dto
{
    public class ProductCreateUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Range(1, 1000)]
        public double Price { get; set; }
        public string? Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
