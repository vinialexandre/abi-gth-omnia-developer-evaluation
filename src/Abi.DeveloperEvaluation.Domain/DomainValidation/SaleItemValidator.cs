﻿using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.Exceptions;

namespace Abi.DeveloperEvaluation.Domain.DomainValidation

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
