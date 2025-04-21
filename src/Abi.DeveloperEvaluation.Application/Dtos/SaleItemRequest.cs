using System.ComponentModel.DataAnnotations;

namespace Abi.DeveloperEvaluation.Application.Dtos;

public class SaleItemRequest
{
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    public string ProductName { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Preço unitário deve ser maior que zero.")]
    public decimal UnitPrice { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser no mínimo 1.")]
    public int Quantity { get; set; }
}
