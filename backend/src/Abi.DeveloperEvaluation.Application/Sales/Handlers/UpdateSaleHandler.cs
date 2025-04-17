using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Application.Sales.Commands;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.Enums;
using Abi.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Abi.DeveloperEvaluation.Application.Sales.Handlers;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, SaleResponse>
{
    private readonly ISaleRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSaleHandler> _logger;

    public UpdateSaleHandler(ISaleRepository repo, IMapper mapper, ILogger<UpdateSaleHandler> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SaleResponse> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start - Updating sale {SaleId}", request.Id);

        var existing = await _repo.GetByIdAsync(request.Id);
        if (existing is null)
        {
            _logger.LogWarning("Sale {SaleId} not found", request.Id);
            throw new Exception("Venda não encontrada.");
        }

        var updatedItems = _mapper.Map<List<SaleItem>>(request.Request.Items);

        if (!Enum.TryParse<SaleStatus>(request.Request.Status, true, out var parsedStatus))
        {
            _logger.LogWarning("Invalid sale status '{Status}' for sale {SaleId}", request.Request.Status, request.Id);
            throw new Exception("Status inválido.");
        }

        _repo.RemoveAllItems(existing);
        _logger.LogDebug("Removed all items from sale {SaleId}", request.Id);

        existing.Modify(request.Request.SaleNumber, updatedItems, parsedStatus);
        _logger.LogDebug("Modified sale {SaleId} with new data", request.Id);

        await _repo.UpdateAsync(existing);
        await _repo.SaveChangesAsync();

        _logger.LogInformation("Sale {SaleId} updated successfully", request.Id);

        return _mapper.Map<SaleResponse>(existing);
    }
}
