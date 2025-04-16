using Ambev.DeveloperEvaluation.Application.Dtos;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers;

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

        existing.Modify(request.Request.SaleNumber, updatedItems);
        await _repo.UpdateAsync(existing);

        return _mapper.Map<SaleResponse>(existing);
    }
}
