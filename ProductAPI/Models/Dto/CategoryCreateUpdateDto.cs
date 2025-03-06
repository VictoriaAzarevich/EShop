using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models.Dto
{
    public class CategoryCreateUpdateDto
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
