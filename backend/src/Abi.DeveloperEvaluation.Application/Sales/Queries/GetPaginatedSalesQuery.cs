using Abi.DeveloperEvaluation.Application.Dtos;
using MediatR;

namespace Abi.DeveloperEvaluation.Application.Sales.Queries;

public record GetPaginatedSalesQuery(int Page, int Size) : IRequest<PaginatedList<SaleResponse>>;
