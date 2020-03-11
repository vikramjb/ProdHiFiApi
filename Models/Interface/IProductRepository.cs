using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProdHiFiApi.Models.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);

        Task<IEnumerable<Product>> GetProductByDescriptionAsync(string description);
        Task<IEnumerable<Product>> GetProductByModelAsync(string model);
        Task<IEnumerable<Product>> GetProductByBrandAsync(string brand);

        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task RemoveProductAsync(Product product);

    }
}