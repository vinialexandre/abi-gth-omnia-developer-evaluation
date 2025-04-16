using Ambev.DeveloperEvaluation.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult BadRequest(string message) =>
        base.BadRequest(new ApiResponse { Message = message, Success = false });

    protected IActionResult NotFound(string message = "Resource not found") =>
        base.NotFound(new ApiResponse { Message = message, Success = false });

    protected IActionResult OkPaginated<T>(PaginatedList<T> pagedList) =>
        base.Ok(PaginatedResponse<T>.Ok(
            pagedList.ToList(),
            pagedList.CurrentPage,
            pagedList.TotalPages,
            pagedList.TotalCount
        ));

    protected IActionResult OkResponse(string message, object? data = null) =>
        Ok(ApiResponse.Ok(message, data));

    protected IActionResult CreatedResponse(string action, object routeValues, object? data = null) =>
        CreatedAtAction(action, routeValues, ApiResponse.Ok("Criado com sucesso", data));
}
