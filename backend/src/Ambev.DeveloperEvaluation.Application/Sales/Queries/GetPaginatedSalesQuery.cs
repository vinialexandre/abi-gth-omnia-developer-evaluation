using Ambev.DeveloperEvaluation.Application.Dtos;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries;

public record GetPaginatedSalesQuery(int Page, int Size) : IRequest<PaginatedList<SaleResponse>>;
