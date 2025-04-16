﻿using Ambev.DeveloperEvaluation.Application.Dtos;
using Ambev.DeveloperEvaluation.Application.Sales.Queries;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers;

public class GetPaginatedSalesHandler : IRequestHandler<GetPaginatedSalesQuery, PaginatedList<SaleResponse>>
{
    private readonly ISaleRepository _repo;
    private readonly IMapper _mapper;

    public GetPaginatedSalesHandler(ISaleRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<PaginatedList<SaleResponse>> Handle(GetPaginatedSalesQuery request, CancellationToken ct)
    {
        var query = (await _repo.GetQueryableAsync()).Include(s => s.Items);
        var paginated = await PaginatedList<Sale>.CreateAsync(query, request.Page, request.Size);
        var mappedItems = _mapper.Map<List<SaleResponse>>(paginated.ToList());
        return paginated.WithNewItems(mappedItems);
    }
}