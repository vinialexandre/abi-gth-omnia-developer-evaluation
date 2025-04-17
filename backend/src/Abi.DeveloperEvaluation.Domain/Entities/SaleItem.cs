using Abi.DeveloperEvaluation.Domain.Common;

namespace Abi.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; private set; }
        public decimal Total => (UnitPrice * Quantity) - Discount;

        public void ApplyDiscount()
        {
            if (Quantity > 20)
                throw new DomainException("Não é possível vender mais de 20 itens do mesmo produto.");

            if (Quantity >= 10)
                Discount = UnitPrice * 0.20m * Quantity;
            else if (Quantity >= 4)
                Discount = UnitPrice * 0.10m * Quantity;
            else
                Discount = 0;
        }
    }
}
