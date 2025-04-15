using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleItemValidator : IValidator<SaleItem>
    {
        public void Validate(SaleItem item)
        {
            if (item.Quantity <= 0)
                throw new DomainException("A quantidade do item deve ser maior que zero.");

            if (item.UnitPrice <= 0)
                throw new DomainException("O preço unitário deve ser maior que zero.");
        }
    }
}
