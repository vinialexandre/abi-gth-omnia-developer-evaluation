
using Ambev.DeveloperEvaluation.Application.Dtos;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands;
public record CreateSaleCommand(SaleRequest Request) : IRequest<SaleResponse>;

