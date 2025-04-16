using Ambev.DeveloperEvaluation.Application.Dtos;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries
{
    public record GetSaleByIdQuery(Guid Id) : IRequest<SaleResponse>;
}