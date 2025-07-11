namespace EShop.Contracts
{
    public interface IUpdatePaymentResultMessage : IBaseMessage
    {
        int OrderId { get; }
        bool Status { get; }
    }
}
