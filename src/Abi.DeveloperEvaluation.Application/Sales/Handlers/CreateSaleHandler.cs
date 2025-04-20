using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Application.Sales.Commands;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Abi.DeveloperEvaluation.Application.Sales.Handlers;
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, SaleResponse>
{
    private readonly ISaleRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;

    public CreateSaleHandler(ISaleRepository repo, IUnitOfWork uow, IMapper mapper, ILogger<CreateSaleHandler> logger)
    {
        _repo = repo;
        _uow = uow;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SaleResponse> Handle(CreateSaleCommand request, CancellationToken ct)
    {
        _logger.LogInformation("Start - Creating sale for customer {CustomerId}", request.Request.CustomerId);

        var sale = _mapper.Map<Sale>(request.Request);
        _logger.LogDebug("Mapped sale: {@Sale}", sale);

        sale.CalculateDiscounts();
        _logger.LogDebug("Discounts calculated for sale {SaleNumber}", sale.SaleNumber);

        await _repo.AddAsync(sale);
        await _uow.CommitAsync();

        _logger.LogInformation("Sale {SaleNumber} persisted successfully", sale.SaleNumber);

        return _mapper.Map<SaleResponse>(sale);
    }

}

