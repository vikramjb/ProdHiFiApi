using System.Linq;
using System.Threading.Tasks;
using ProdHiFiApi.Models;
using ProdHiFiApi.Repository.Interface;

namespace ProdHiFiApi.Data
{
    public class BaseDataSeeder
    {

        private readonly IRepositoryWrapper _repositoryWrapper;

        public BaseDataSeeder(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task SeedAsync()
        {
            var productCount = await _repositoryWrapper.Product.GetAllProductsAsync();
            if (!productCount.ToList().Any())
            {
                await _repositoryWrapper.Product.CreateProductAsync(new Product { Description = "Guitar", Model = "Acoustic", Brand = "Fender" });
                await _repositoryWrapper.Product.CreateProductAsync(new Product { Description = "Samsung Galaxy S20 Ultra 5G 512GB (Cosmic Black)", Model = "Galaxy S20", Brand = "Samsung" });
                await _repositoryWrapper.Product.CreateProductAsync(new Product { Description = "JBL Charge 4 Portable Bluetooth Speaker (Black)", Model = "JBL Charge 4", Brand = "JBL" });
            }
        }

    }
}