namespace Abi.DeveloperEvaluation.Application.Dtos;
public class SaleRequest
{
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string? Status { get; set; }
    public List<SaleItemRequest> Items { get; set; } = new();
}
