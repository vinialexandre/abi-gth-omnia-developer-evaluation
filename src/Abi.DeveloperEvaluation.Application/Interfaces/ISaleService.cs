using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Domain.Entities;

namespace Abi.DeveloperEvaluation.Application.Services;

public interface ISaleService
{
    Task<Sale?> GetByIdAsync(Guid id);
    Task<IEnumerable<Sale>> GetAllAsync();
    Task<Sale> CreateAsync(Sale sale);
    Task<Sale> UpdateAsync(Sale sale);
    Task CancelAsync(Guid id, string reason);
    Task<PaginatedList<Sale>> GetPaginatedAsync(int pageNumber, int pageSize);

}
