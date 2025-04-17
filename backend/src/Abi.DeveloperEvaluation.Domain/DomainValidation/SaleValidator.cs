using Abi.DeveloperEvaluation.Domain.Entities;

namespace Abi.DeveloperEvaluation.Domain.DomainValidation

{
    public class SaleValidator : IValidator<Sale>
    {
        public void Validate(Sale sale)
        {
            if (string.IsNullOrWhiteSpace(sale.CustomerName))
                throw new DomainException("O nome do cliente é obrigatório.");

            if (!sale.Items.Any())
                throw new DomainException("A venda deve conter pelo menos um item.");

            if (sale.TotalAmount <= 0)
                throw new DomainException("O valor total da venda deve ser maior que zero.");
        }
    }

}
