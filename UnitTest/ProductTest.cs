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
        public async void GetAllProducts()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "ProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                var repositoryWrapper = new RepositoryWrapper(_dbContext);
                repositoryWrapper.Product.CreateProduct(new Product { Id = 1, Description = "Fender Bender Guitar", Model = "Acoustic", Brand = "Fender" });
                repositoryWrapper.Product.CreateProduct(new Product { Id = 2, Description = "Samsung Galaxy S20 Ultra 5G 512GB (Cosmic Black)", Model = "Galaxy S20", Brand = "Samsung" });
                repositoryWrapper.Product.CreateProduct(new Product { Id = 3, Description = "JBL Charge 4 Portable Bluetooth Speaker (Black)", Model = "JBL Charge 4", Brand = "JBL" });
                await repositoryWrapper.SaveAsync();
                var allProducts = await repositoryWrapper.Product.GetAllProductsAsync();
                Assert.True((allProducts.ToList().Count() > 0));
            }
        }
        [Fact]
        public async void AddProduct()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "ProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                var repositoryWrapper = new RepositoryWrapper(_dbContext);
                var allProducts = await repositoryWrapper.Product.GetAllProductsAsync();
                Assert.Equal(3, allProducts.Count());
            }
        }

        [Fact]
        public async void FindProductById()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "ProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                var repositoryWrapper = new RepositoryWrapper(_dbContext);
                var product = await repositoryWrapper.Product.GetProductByIdAsync(1);
                Assert.True(product != null);
            }
        }
        [Fact]
        public async void FindProductByBrand()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "ProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                var repositoryWrapper = new RepositoryWrapper(_dbContext);
                var productList = await repositoryWrapper.Product.GetProductByBrandAsync("Fender");
                Assert.True((productList.ToList().Count() > 0));
            }
        }

        [Fact]
        public async void FindProductByModel()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "ProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                var repositoryWrapper = new RepositoryWrapper(_dbContext);
                var productList = await repositoryWrapper.Product.GetProductByModelAsync("Acoustic");
                Assert.True((productList.ToList().Count() > 0));
            }
        }

        [Fact]
        public async void FindProductByDescription()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "ProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                var repositoryWrapper = new RepositoryWrapper(_dbContext);
                var productList = await repositoryWrapper.Product.GetProductByDescriptionAsync("Guitar");
                Assert.True((productList.ToList().Count() > 0));
            }
        }
    }
}