using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProdHiFiApi.Data;
using ProdHiFiApi.Models.Interface;

namespace ProdHiFiApi.Models
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private ProductDbContext _dbContext;
        public ProductRepository(ProductDbContext prodDbContext) : base(prodDbContext)
        {
            _dbContext = prodDbContext;
        }

        public void CreateProduct(Product product)
        {
            Create(product);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await GetAll().OrderBy(mod => mod.Model).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await GetByCustomCondition(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByBrandAsync(string brand)
        {
            return await GetByCustomCondition(x => x.Brand.Contains(brand)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByDescriptionAsync(string description)
        {
            return await GetByCustomCondition(x => x.Description.Contains(description)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByModelAsync(string model)
        {
            return await GetByCustomCondition(x => x.Model.Contains(model)).ToListAsync();
        }

        public void RemoveProduct(Product product)
        {
            Remove(product);
        }

        public void UpdateProduct(Product product)
        {
            Update(product);
        }
    }
}