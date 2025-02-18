using System.ComponentModel.DataAnnotations;

namespace EShop.Services.ProductAPI.Models.Dto
{
    public class CategoryCreateUpdateDto
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
