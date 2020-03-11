using System;
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

        public async Task CreateProductAsync(Product product)
        {
            Create(product);
            await _dbContext.SaveChangesAsync();
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
            return await GetByCustomCondition(x => x.Description.Contains(description, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByModelAsync(string model)
        {
            return await GetByCustomCondition(x => x.Model.Contains(model)).ToListAsync();
        }

        public async Task RemoveProductAsync(Product product)
        {
            Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}