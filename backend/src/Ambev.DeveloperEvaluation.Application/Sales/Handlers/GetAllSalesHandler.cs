using Ambev.DeveloperEvaluation.Application.Dtos;
using Ambev.DeveloperEvaluation.Application.Sales.Queries;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers;

public class GetAllSalesHandler : IRequestHandler<GetAllSalesQuery, IEnumerable<SaleResponse>>
{
    private readonly ISaleRepository _repo;
    private readonly IMapper _mapper;

    public GetAllSalesHandler(ISaleRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SaleResponse>> Handle(GetAllSalesQuery request, CancellationToken ct)
    {
        var sales = await _repo.Query().Include(s => s.Items).ToListAsync(ct);
        return _mapper.Map<IEnumerable<SaleResponse>>(sales);
    }
}
