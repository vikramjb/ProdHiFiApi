using System;
using System.Threading.Tasks;
using ProdHiFiApi.Data;
using ProdHiFiApi.Models.Interface;

namespace ProdHiFiApi.Models
{
    public class RepositoryWrapper : IRepositoryWrapper, IDisposable
    {
        private IProductRepository _productRepository;
        private ProductDbContext _dbContext;
        public RepositoryWrapper(ProductDbContext prodDbContext)
        {
            _dbContext = prodDbContext;
        }

        public IProductRepository Product
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_dbContext);
                }
                return _productRepository;
            }
        }

        public void Dispose()
        {
            _dbContext = null;
            _productRepository = null;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}