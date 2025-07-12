namespace EShop.Contracts
{
    public class CartDetailsDto
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
