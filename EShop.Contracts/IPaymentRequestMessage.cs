namespace EShop.Contracts
{
    public interface IPaymentRequestMessage : IBaseMessage
    {
        int OrderId { get; }
        string Name { get; }
        string CartNumber { get; }
        string CVV { get; }
        string ExpiryMonthYear { get; }
        double OrderTotal { get; }
        string Email { get; }
    }
}
