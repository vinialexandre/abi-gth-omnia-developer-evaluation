using Abi.DeveloperEvaluation.Application.Sales.Commands;
using Abi.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Abi.DeveloperEvaluation.Application.Sales.Handlers;

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
        await _repo.SaveChangesAsync();

        return Unit.Value;
    }
}
