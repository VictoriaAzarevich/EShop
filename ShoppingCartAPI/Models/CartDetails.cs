using ProductAPI.Models;

namespace ShoppingCartAPI.Models
{
    public class CartDetails
    {
        public int CartDetailsId { get; set; }
        public required CartHeader CartHeader { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
