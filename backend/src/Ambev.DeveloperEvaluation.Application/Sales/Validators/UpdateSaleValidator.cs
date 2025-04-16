using Ambev.DeveloperEvaluation.Application.Dtos;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Validators;

public class UpdateSaleValidator : AbstractValidator<SaleRequest>
{
    public UpdateSaleValidator()
    {
        RuleFor(x => x.SaleNumber)
            .NotEmpty().WithMessage("O número da venda é obrigatório.");

        RuleFor(x => x.SaleDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data da venda não pode ser futura.");

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Cliente é obrigatório.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("A venda deve ter pelo menos um item.");

        RuleForEach(x => x.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.ProductId)
                .NotEmpty().WithMessage("Produto é obrigatório.");

            items.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero.")
                .LessThanOrEqualTo(20).WithMessage("Limite máximo é 20 unidades por item.");

            items.RuleFor(i => i.UnitPrice)
                .GreaterThan(0).WithMessage("Preço unitário deve ser maior que zero.");
        });
    }
}
