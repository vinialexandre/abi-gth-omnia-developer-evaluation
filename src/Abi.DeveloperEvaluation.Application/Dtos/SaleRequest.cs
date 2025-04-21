using System.ComponentModel.DataAnnotations;

namespace Abi.DeveloperEvaluation.Application.Dtos;

public class SaleRequest
{
    [Required]
    public string SaleNumber { get; set; } = string.Empty;

    [Required]
    public DateTime SaleDate { get; set; }

    [Required]
    public Guid CustomerId { get; set; }

    [Required]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    public Guid BranchId { get; set; }

    public string BranchName { get; set; } = string.Empty;

    public string? Status { get; set; }

    public List<SaleItemRequest> Items { get; set; } = new();
}
