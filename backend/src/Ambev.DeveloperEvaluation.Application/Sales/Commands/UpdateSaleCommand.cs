using Ambev.DeveloperEvaluation.Application.Dtos;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands;

public record UpdateSaleCommand(Guid Id, SaleRequest Request) : IRequest<SaleResponse>;
