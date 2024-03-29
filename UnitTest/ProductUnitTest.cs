using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProdHiFiApi.Data;
using ProdHiFiApi.Models;
using ProdHiFiApi.Repository;
using ProdHiFiApi.Repository.Interface;
using Xunit;

namespace ProdHiFiApi.UnitTest
{
    public class ProductUnitTest
    {
        public ProductUnitTest()
        {
        }

        [Fact]
        public async void AddProduct()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "AddProductsDb").Options;
            var listProducts = GetTestProducts();
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                using (var repositoryWrapper = new RepositoryWrapper(_dbContext))
                {
                    InsertProducts(repositoryWrapper);
                    var allProducts = await repositoryWrapper.Product.GetAllProductsAsync();
                    Assert.True(allProducts.Any());
                    Assert.True(allProducts.Count() == listProducts.Count());
                }
            }
        }

        [Theory]
        [InlineData(1, "New Fender Brand 1")]
        [InlineData(2, "New Samsung Brand 1")]
        [InlineData(3, "New JBL Brand 1")]
        public async void UpdateProduct(int id, string newBrandInformation)
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "UpdateProductsDb").Options;
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                using (var repositoryWrapper = new RepositoryWrapper(_dbContext))
                {
                    InsertProducts(repositoryWrapper);
                    var productFromRepo = await repositoryWrapper.Product.GetProductByIdAsyncForEditing(id);
                    productFromRepo.Brand = newBrandInformation;
                    await repositoryWrapper.Product.UpdateProductAsync(productFromRepo);
                    productFromRepo = await repositoryWrapper.Product.GetProductByIdAsync(id);
                    Assert.True(productFromRepo.Brand == newBrandInformation);
                }
            }
        }

        [Fact]
        public async void GetAllProducts()
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "GetProductsDb").Options;
            var testProducts = GetTestProducts();
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                using (var repositoryWrapper = new RepositoryWrapper(_dbContext))
                {
                    InsertProducts(repositoryWrapper);
                    var allProducts = await repositoryWrapper.Product.GetAllProductsAsync();
                    Assert.True(allProducts.Count() == testProducts.Count());
                }
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void FindProductById(int id)
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "FindProductsDb").Options;
            var testProducts = GetTestProducts();
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                using (var repositoryWrapper = new RepositoryWrapper(_dbContext))
                {
                    InsertProducts(repositoryWrapper);
                    var product = await repositoryWrapper.Product.GetProductByIdAsync(id);
                    var testProduct = testProducts.FirstOrDefault(x => x.Id == id);
                    Assert.NotNull(product);
                    Assert.True(product.Brand == testProduct.Brand);
                }
            }
        }

        [Theory]
        [InlineData("Fender")]
        [InlineData("Samsung")]
        [InlineData("JBL")]
        public async void FindProductByBrand(string brandSearchText)
        {
            var dbOptions = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "BrandProductsDb").Options;
            var testProducts = GetTestProducts();
            using (var _dbContext = new ProductDbContext(dbOptions))
            {
                using (var repositoryWrapper = new RepositoryWrapper(_dbContext))
                {
                    InsertProducts(repositoryWrapper);
                    var productList = await repositoryWrapper.Product.GetProductByBrandAsync(brandSearchText);
                    var testProductList = testProducts.Where(x => x.Brand.Contains(brandSearchText));
                    Assert.True((productList.ToList().Any()));
                    Assert.True((productList.ToList().Count() == testProductList.Count()));
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
                    Assert.True((productList.ToList().Any()));
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
            if (repositoryWrapper.Product.GetAll().Any())
            {
                return;
            }

            await repositoryWrapper.Product.CreateProductAsync(new Product { Id = 1, Description = "Fender Bender Guitar", Model = "Acoustic", Brand = "Fender" });
            await repositoryWrapper.Product.CreateProductAsync(new Product { Id = 2, Description = "Samsung Galaxy S20 Ultra 5G 512GB (Cosmic Black)", Model = "Galaxy S20", Brand = "Samsung" });
            await repositoryWrapper.Product.CreateProductAsync(new Product { Id = 3, Description = "JBL Charge 4 Portable Blue-tooth Speaker (Black)", Model = "JBL Charge 4", Brand = "JBL" });
        }

        private IEnumerable<Product> GetTestProducts()
        {
            var testProducts = new List<Product>
            {
                new Product {Id = 1, Description = "Fender Bender Guitar", Model = "Acoustic", Brand = "Fender"},
                new Product
                {
                    Id = 2, Description = "Samsung Galaxy S20 Ultra 5G 512GB (Cosmic Black)", Model = "Galaxy S20",
                    Brand = "Samsung"
                },
                new Product { Id = 3, Description = "JBL Charge 4 Portable Blue-tooth Speaker (Black)", Model = "JBL Charge 4", Brand = "JBL" }
            };
            return testProducts;
        }
    }
}