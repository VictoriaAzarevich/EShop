namespace ShoppingCartAPI.Models
{
    public class Cart
    {
        public required CartHeader CartHeader { get; set; }
        public List<CartDetails> CartDetails { get; set; } = [];
    }
}
