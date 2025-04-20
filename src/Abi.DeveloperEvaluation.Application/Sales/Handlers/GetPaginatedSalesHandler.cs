using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Application.Sales.Queries;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Abi.DeveloperEvaluation.Application.Sales.Handlers;

public class GetPaginatedSalesHandler : IRequestHandler<GetPaginatedSalesQuery, PaginatedList<SaleResponse>>
{
    private readonly ISaleRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPaginatedSalesHandler> _logger;

    public GetPaginatedSalesHandler(ISaleRepository repo, IMapper mapper, ILogger<GetPaginatedSalesHandler> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedList<SaleResponse>> Handle(GetPaginatedSalesQuery request, CancellationToken ct)
    {
        _logger.LogInformation("Start - Retrieving paginated sales - Page: {Page}, Size: {Size}", request.Page, request.Size);

        var query = (await _repo.GetQueryableAsync()).Include(s => s.Items);
        var paginated = await PaginatedList<Sale>.CreateAsync(query, request.Page, request.Size);

        _logger.LogInformation("Retrieved {Count} sales from page {Page}", paginated.Count, request.Page);

        var mappedItems = _mapper.Map<List<SaleResponse>>(paginated.ToList());
        return paginated.WithNewItems(mappedItems);
    }
}
