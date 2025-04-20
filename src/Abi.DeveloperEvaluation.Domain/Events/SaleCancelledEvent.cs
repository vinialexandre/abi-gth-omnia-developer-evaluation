namespace Abi.DeveloperEvaluation.Domain.Events
{
    public class SaleCancelledEvent : BaseEvent
    {
        public Guid SaleId { get; }
        public string Reason { get; }
        public DateTime CancelledAt { get; }

        public SaleCancelledEvent(Guid saleId, string reason)
        {
            SaleId = saleId;
            Reason = reason;
            CancelledAt = DateTime.UtcNow;
        }
    }
}
