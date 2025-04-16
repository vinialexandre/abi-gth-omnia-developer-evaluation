using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaleController : BaseController
{
    private readonly ISaleService _saleService;

    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var sale = await _saleService.GetByIdAsync(id);
        if (sale == null)
            return NotFound(ApiResponse.Fail("Venda não encontrada."));

        return Ok(ApiResponseWithData<Sale>.Ok(sale));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sales = await _saleService.GetAllAsync();
        return Ok(ApiResponseWithData<IEnumerable<Sale>>.Ok(sales));
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var result = await _saleService.GetPaginatedAsync(page, size);
        return Ok(PaginatedResponse<Sale>.Ok(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Sale sale)
    {
        var created = await _saleService.CreateAsync(sale);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, ApiResponseWithData<Sale>.Ok(created));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Sale sale)
    {
        if (id != sale.Id)
            return BadRequest(ApiResponse.Fail("O ID da rota deve coincidir com o ID da venda."));

        var updated = await _saleService.UpdateAsync(sale);
        return Ok(ApiResponseWithData<Sale>.Ok(updated));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Cancel(Guid id, [FromQuery] string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            return BadRequest(ApiResponse.Fail("O motivo do cancelamento deve ser informado."));

        await _saleService.CancelAsync(id, reason);
        return Ok(ApiResponse.Ok("Venda cancelada com sucesso."));
    }
}
