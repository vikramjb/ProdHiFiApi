using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProdHiFiApi.Data;
using ProdHiFiApi.Models;
using ProdHiFiApi.Models.Interface;
using Xunit;

namespace ProdHiFiApi.UnitTest
{
    public class ProductTest
    {

        public ProductTest()
        {

        }

        [Fact]
        public async void AddProduct()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "AddProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                using (var repositoryWrapper = new RepositoryWrapper(_dbContext))
                {
                    InsertProducts(repositoryWrapper);
                    var allProducts = await repositoryWrapper.Product.GetAllProductsAsync();
                    Assert.True(allProducts.Any());
                }
            }
        }

        [Fact]
        public async void GetAllProducts()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "GetProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                using (var repositoryWrapper = new RepositoryWrapper(_dbContext))
                {
                    InsertProducts(repositoryWrapper);
                    var allProducts = await repositoryWrapper.Product.GetAllProductsAsync();
                    Assert.True(allProducts.Any());
                }
            }
        }

        [Fact]
        public async void FindProductById()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "FindProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                using (var repositoryWrapper = new RepositoryWrapper(_dbContext))
                {
                    InsertProducts(repositoryWrapper);
                    var product = await repositoryWrapper.Product.GetProductByIdAsync(1);
                    Assert.True(product != null);
                }
            }
        }

        [Fact]
        public async void FindProductByBrand()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "BrandProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                using (var repositoryWrapper = new RepositoryWrapper(_dbContext))
                {
                    InsertProducts(repositoryWrapper);
                    var productList = await repositoryWrapper.Product.GetProductByBrandAsync("Fender");
                    Assert.True((productList.ToList().Count() > 0));
                }
            }
        }

        [Fact]
        public async void FindProductByModel()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "ModelProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                using (var repositoryWrapper = new RepositoryWrapper(_dbContext))
                {
                    InsertProducts(repositoryWrapper);
                    var productList = await repositoryWrapper.Product.GetProductByModelAsync("Acoustic");
                    Assert.True((productList.ToList().Count() > 0));
                }
            }
        }

        [Fact]
        public async void FindProductByDescription()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "DescriptionProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                using (var repositoryWrapper = new RepositoryWrapper(_dbContext))
                {
                    InsertProducts(repositoryWrapper);
                    var productList = await repositoryWrapper.Product.GetProductByDescriptionAsync("Ultra");
                    Assert.True((productList.Any()));
                }
            }
        }


        private async void InsertProducts(IRepositoryWrapper repositoryWrapper)
        {
            await repositoryWrapper.Product.CreateProductAsync(new Product { Id = 1, Description = "Fender Bender Guitar", Model = "Acoustic", Brand = "Fender" });
            await repositoryWrapper.Product.CreateProductAsync(new Product { Id = 2, Description = "Samsung Galaxy S20 Ultra 5G 512GB (Cosmic Black)", Model = "Galaxy S20", Brand = "Samsung" });
            await repositoryWrapper.Product.CreateProductAsync(new Product { Id = 3, Description = "JBL Charge 4 Portable Bluetooth Speaker (Black)", Model = "JBL Charge 4", Brand = "JBL" });
        }

    }
}