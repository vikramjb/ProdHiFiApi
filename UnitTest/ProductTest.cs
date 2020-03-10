using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProdHiFiApi.Data;
using ProdHiFiApi.Models;
using Xunit;

namespace ProdHiFiApi.UnitTest
{
    public class ProductTest
    {
        [Fact]
        public void GetAllProducts()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "listProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                var repositoryWrapper = new RepositoryWrapper(_dbContext);
                repositoryWrapper.Product.CreateProduct(new Product { Description = "Fender Bender Guitar", Model = "Acoustic", Brand = "Fender" });
                repositoryWrapper.Product.CreateProduct(new Product { Description = "Samsung Galaxy S20 Ultra 5G 512GB (Cosmic Black)", Model = "Galaxy S20", Brand = "Samsung" });
                repositoryWrapper.Product.CreateProduct(new Product { Description = "JBL Charge 4 Portable Bluetooth Speaker (Black)", Model = "JBL Charge 4", Brand = "JBL" });

                var allProducts = repositoryWrapper.Product.GetAllProducts();
                Assert.True((allProducts.ToList().Count() > 0));
            }
        }
        [Fact]
        public void AddProduct()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "addProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                var repositoryWrapper = new RepositoryWrapper(_dbContext);
                repositoryWrapper.Product.CreateProduct(new Product { Description = "Fender Bender Guitar", Model = "Acoustic", Brand = "Fender" });
                var allProducts = repositoryWrapper.Product.GetAllProducts();
                Assert.Equal(1, allProducts.Count());
            }
        }
    }
}