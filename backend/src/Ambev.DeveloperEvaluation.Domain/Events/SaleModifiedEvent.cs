namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleModifiedEvent : BaseEvent
    {
        public Guid SaleId { get; }
        public string SaleNumber { get; }
        public DateTime ModifiedAt { get; }

        public SaleModifiedEvent(Guid saleId, string saleNumber, DateTime modifiedAt)
        {
            SaleId = saleId;
            SaleNumber = saleNumber;
            ModifiedAt = modifiedAt;
        }
    }
}
