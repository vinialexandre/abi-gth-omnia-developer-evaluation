using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, Unit>
{
    private readonly ISaleRepository _repo;

    public CancelSaleHandler(ISaleRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(CancelSaleCommand request, CancellationToken ct)
    {
        var sale = await _repo.GetByIdAsync(request.SaleId);
        if (sale is null)
            throw new Exception("Venda não encontrada.");

        sale.Cancel(request.Reason);

        await _repo.UpdateAsync(sale);
        return Unit.Value;
    }
}
