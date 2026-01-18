using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
    }
}
