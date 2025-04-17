
using Abi.DeveloperEvaluation.Application.Dtos;
using MediatR;

namespace Abi.DeveloperEvaluation.Application.Sales.Commands;
public record CreateSaleCommand(SaleRequest Request) : IRequest<SaleResponse>;

