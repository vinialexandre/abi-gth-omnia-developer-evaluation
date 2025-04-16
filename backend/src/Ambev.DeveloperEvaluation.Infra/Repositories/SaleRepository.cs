using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Infra.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DbContext _context;

        public SaleRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Sale?> GetByIdAsync(Guid id) => await _context.Set<Sale>().FindAsync(id);
        public IQueryable<Sale> Query()
        {
            return _context.Set<Sale>().AsNoTracking();
        }

        public Task<IQueryable<Sale>> GetQueryableAsync()
        {
            return Task.FromResult(_context.Set<Sale>().Include(s => s.Items).AsNoTracking().AsQueryable());
        }

        public async Task<IEnumerable<Sale>> GetAllAsync() => await _context.Set<Sale>().ToListAsync();
        public async Task AddAsync(Sale sale)
        {
            await _context.Set<Sale>().AddAsync(sale);
        }

        public async Task UpdateAsync(Sale sale)
        {
            _context.Set<Sale>().Update(sale);
        }

        public async Task DeleteAsync(Guid id)
        {
            var sale = await GetByIdAsync(id);
            if (sale != null)
                _context.Set<Sale>().Remove(sale);
        }

        public async Task<bool> ExistsAsync(Guid id) => await _context.Set<Sale>().AnyAsync(x => x.Id == id);
    }
}
