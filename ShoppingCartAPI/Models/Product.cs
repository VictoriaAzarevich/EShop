using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public required string CategoryName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
