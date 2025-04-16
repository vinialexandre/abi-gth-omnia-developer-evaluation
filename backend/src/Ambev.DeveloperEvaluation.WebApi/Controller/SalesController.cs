using Ambev.DeveloperEvaluation.Application.Dtos;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Application.Sales.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Controller;

[ApiController]
[Route("api/sales")]
public class SaleController : BaseController
{
    private readonly IMediator _mediator;

    public SaleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetSaleByIdQuery(id));

        if (result is null)
            return NotFound(ApiResponse.Fail("Venda não encontrada."));

        return Ok(ApiResponse.Ok("Venda encontrada com sucesso.", result));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllSalesQuery());
        return Ok(ApiResponse.Ok("Listagem realizada com sucesso.", new { Sales = result }));
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var result = await _mediator.Send(new GetPaginatedSalesQuery(page, size));
        return Ok(PaginatedResponse<SaleResponse>.Ok(
            result.ToList(),
            result.CurrentPage,
            result.TotalPages,
            result.TotalCount
        ));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaleRequest request)
    {
        var result = await _mediator.Send(new CreateSaleCommand(request));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, ApiResponse.Ok("Venda criada com sucesso.", result));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] SaleRequest request)
    {
        var result = await _mediator.Send(new UpdateSaleCommand(id, request));
        return Ok(ApiResponse.Ok("Venda atualizada com sucesso.", result));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Cancel(Guid id, [FromQuery] string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            return BadRequest(ApiResponse.Fail("O motivo do cancelamento deve ser informado."));

        await _mediator.Send(new CancelSaleCommand(id, reason));
        return Ok(ApiResponse.Ok("Venda cancelada com sucesso."));
    }
}