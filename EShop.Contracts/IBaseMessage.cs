namespace EShop.Contracts
{
    public interface IBaseMessage
    {
        public Guid Id { get; }
        public DateTime MessageCreated { get; }
    }
}
