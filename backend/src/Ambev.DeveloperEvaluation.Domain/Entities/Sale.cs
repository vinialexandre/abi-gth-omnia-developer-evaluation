using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
        public SaleStatus Status { get; private set; } = SaleStatus.Pending;
        public decimal TotalAmount => Items.Sum(i => i.Total);

        public List<BaseEvent> DomainEvents { get; private set; } = new();

        public void AddItem(SaleItem item)
        {
            item.ApplyDiscount();
            Items.Add(item);
        }

        public void Cancel(string reason)
        {
            if (Status == SaleStatus.Cancelled)
                throw new DomainException("A venda já está cancelada.");

            Status = SaleStatus.Cancelled;

            DomainEvents.Add(new SaleCancelledEvent(Id, reason));
        }

        public void MarkAsCompleted()
        {
            if (!Items.Any())
                throw new DomainException("A venda deve conter ao menos um item.");

            foreach (var item in Items)
                item.ApplyDiscount();

            Status = SaleStatus.Completed;

            DomainEvents.Add(new SaleCreatedEvent(Id, SaleDate, CustomerId, TotalAmount));
        }

        public void Modify(string saleNumber, List<SaleItem> newItems)
        {
            SaleNumber = saleNumber;
            Items = newItems;

            DomainEvents.Add(new SaleModifiedEvent(Id, SaleNumber, DateTime.UtcNow));
        }

        public void ClearEvents()
        {
            DomainEvents.Clear();
        }

        public void CalculateDiscounts()
        {
            foreach (var item in Items)
                item.ApplyDiscount();
        }

    }
}
