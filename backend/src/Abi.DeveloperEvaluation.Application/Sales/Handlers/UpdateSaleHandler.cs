using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Application.Sales.Commands;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.Enums;
using Abi.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Abi.DeveloperEvaluation.Application.Sales.Handlers;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, SaleResponse>
{
    private readonly ISaleRepository _repo;
    private readonly IMapper _mapper;

    public UpdateSaleHandler(ISaleRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<SaleResponse> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repo.GetByIdAsync(request.Id);
        if (existing is null)
            throw new Exception("Venda não encontrada.");

        var updatedItems = _mapper.Map<List<SaleItem>>(request.Request.Items);

        if (!Enum.TryParse<SaleStatus>(request.Request.Status, true, out var parsedStatus))
            throw new Exception("Status inválido.");

        _repo.RemoveAllItems(existing);

        existing.Modify(request.Request.SaleNumber, updatedItems, parsedStatus);

        await _repo.UpdateAsync(existing);
        await _repo.SaveChangesAsync();

        return _mapper.Map<SaleResponse>(existing);
    }
}
