using Abi.DeveloperEvaluation.Application.Dtos;
using MediatR;

namespace Abi.DeveloperEvaluation.Application.Sales.Commands;

public record UpdateSaleCommand(Guid Id, SaleRequest Request) : IRequest<SaleResponse>;
