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

namespace Abi.DeveloperEvaluation.Application.Sales.Handlers;
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, SaleResponse>
{
    private readonly ISaleRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CreateSaleHandler(ISaleRepository repo, IUnitOfWork uow, IMapper mapper)
    {
        _repo = repo;
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<SaleResponse> Handle(CreateSaleCommand request, CancellationToken ct)
    {
        var sale = _mapper.Map<Sale>(request.Request);
        sale.CalculateDiscounts();
        await _repo.AddAsync(sale);
        await _uow.CommitAsync();
        return _mapper.Map<SaleResponse>(sale);
    }
}

