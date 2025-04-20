using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Application.Sales.Commands;
using FluentValidation;

namespace Abi.DeveloperEvaluation.Application.Sales.Validators;
public class CancelSaleValidator : AbstractValidator<CancelSaleCommand>
{
    public CancelSaleValidator()
    {
        RuleFor(x => x.SaleId)
            .NotEmpty().WithMessage("O ID da venda é obrigatório.");

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("O motivo do cancelamento é obrigatório.")
            .MinimumLength(5).WithMessage("Descreva melhor o motivo.");
    }
}
