using System.Linq;
using System.Threading.Tasks;
using ProdHiFiApi.Models;
using ProdHiFiApi.Models.Interface;

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
            if (productCount.ToList().Count() <= 0)
            {
                _repositoryWrapper.Product.CreateProduct(new Product { Description = "Product 1 Description", Model = "Product 1 Model", Brand = "Product 1 Brand" });
                _repositoryWrapper.Product.CreateProduct(new Product { Description = "Product 2 Description", Model = "Product 2 Model", Brand = "Product 2 Brand" });
                _repositoryWrapper.Product.CreateProduct(new Product { Description = "Product 3 Description", Model = "Product 3 Model", Brand = "Product 3 Brand" });
            }
        }

    }
}