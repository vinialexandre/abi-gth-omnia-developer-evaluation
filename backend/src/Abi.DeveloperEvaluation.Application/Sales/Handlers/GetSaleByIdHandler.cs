using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Application.Sales.Queries;
using Abi.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Abi.DeveloperEvaluation.Application.Sales.Handlers;

public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, SaleResponse?>
{
    private readonly ISaleRepository _repo;
    private readonly IMapper _mapper;

    public GetSaleByIdHandler(ISaleRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<SaleResponse?> Handle(GetSaleByIdQuery request, CancellationToken ct)
    {
        var sale = await _repo.Query()
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == request.Id, ct);

        return sale is null ? null : _mapper.Map<SaleResponse>(sale);
    }
}