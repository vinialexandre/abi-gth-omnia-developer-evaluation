using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Application.Sales.Queries;
using Abi.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Abi.DeveloperEvaluation.Application.Sales.Handlers;

public class GetAllSalesHandler : IRequestHandler<GetAllSalesQuery, IEnumerable<SaleResponse>>
{
    private readonly ISaleRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSalesHandler> _logger;

    public GetAllSalesHandler(ISaleRepository repo, IMapper mapper, ILogger<GetAllSalesHandler> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<SaleResponse>> Handle(GetAllSalesQuery request, CancellationToken ct)
    {
        _logger.LogInformation("Start - Retrieving all sales");

        var sales = await _repo.Query().Include(s => s.Items).ToListAsync(ct);

        _logger.LogInformation("Retrieved {Count} sales", sales.Count);

        return _mapper.Map<IEnumerable<SaleResponse>>(sales);
    }
}
