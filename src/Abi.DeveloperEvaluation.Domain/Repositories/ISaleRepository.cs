using Abi.DeveloperEvaluation.Domain.Entities;

namespace Abi.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale?> GetByIdAsync(Guid id);
        Task<IEnumerable<Sale>> GetAllAsync();
        Task AddAsync(Sale sale);
        Task UpdateAsync(Sale sale);
        Task<bool> ExistsAsync(Guid id);
        Task<IQueryable<Sale>> GetQueryableAsync();
        IQueryable<Sale> Query();
        Task SaveChangesAsync();
        void RemoveAllItems(Sale sale);

    }
}
