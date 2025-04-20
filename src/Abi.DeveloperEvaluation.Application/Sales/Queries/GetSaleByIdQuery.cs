using Abi.DeveloperEvaluation.Application.Dtos;
using MediatR;

namespace Abi.DeveloperEvaluation.Application.Sales.Queries
{
    public record GetSaleByIdQuery(Guid Id) : IRequest<SaleResponse>;
}