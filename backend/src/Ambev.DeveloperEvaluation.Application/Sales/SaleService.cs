using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Validation;
using Ambev.DeveloperEvaluation.Application.Dtos;
using Ambev.DeveloperEvaluation.Application.Services;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        private readonly IValidator<Sale> _saleValidator;
        private readonly IValidator<SaleItem> _itemValidator;

        public SaleService(
            ISaleRepository repository,
            IValidator<Sale> saleValidator,
            IValidator<SaleItem> itemValidator)
        {
            _repository = repository;
            _saleValidator = saleValidator;
            _itemValidator = itemValidator;
        }

        public async Task<Sale> CreateAsync(Sale sale)
        {
            ValidateSale(sale);
            sale.MarkAsCompleted();
            await _repository.AddAsync(sale);
            PublishEvents(sale);
            return sale;
        }

        public async Task<Sale> UpdateAsync(Sale sale)
        {
            ValidateSale(sale);
            sale.Modify(sale.SaleNumber, sale.Items.ToList());
            await _repository.UpdateAsync(sale);
            PublishEvents(sale);
            return sale;
        }

        public async Task CancelAsync(Guid saleId, string reason)
        {
            var sale = await _repository.GetByIdAsync(saleId)
                ?? throw new DomainException("Venda não encontrada.");

            sale.Cancel(reason);

            await _repository.UpdateAsync(sale);

            PublishEvents(sale);
        }

        private void ValidateSale(Sale sale)
        {
            _saleValidator.Validate(sale);

            foreach (var item in sale.Items)
                _itemValidator.Validate(item);
        }

        private void PublishEvents(Sale sale)
        {
            foreach (var e in sale.DomainEvents)
                Console.WriteLine($"[EVENTO] {e.GetType().Name} publicado");

            sale.ClearEvents();
        }

        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PaginatedList<Sale>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = await _repository.GetQueryableAsync();
            return await PaginatedList<Sale>.CreateAsync(query, pageNumber, pageSize);
        }


    }
}
