using Abi.DeveloperEvaluation.Application.Dtos;
using MediatR;

namespace Abi.DeveloperEvaluation.Application.Sales.Queries
{
    public record GetAllSalesQuery() : IRequest<IEnumerable<SaleResponse>>;
}
