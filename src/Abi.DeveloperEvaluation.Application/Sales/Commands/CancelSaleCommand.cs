using MediatR;

namespace Abi.DeveloperEvaluation.Application.Sales.Commands;

public record CancelSaleCommand(Guid SaleId, string Reason) : IRequest<Unit>;
