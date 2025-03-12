namespace ShoppingCartAPI.Models
{
    public class Cart
    {
        public CartHeader? CartHeader { get; set; }
        public List<CartDetails> CartDetails { get; set; } = [];
    }
}
