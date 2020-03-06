using System.Collections.Generic;
using System.Linq;
using ProdHiFiApi.Data;
using ProdHiFiApi.Models.Interface;

namespace ProdHiFiApi.Models
{
    public class ProductRepository : IProductRepository
    {
        private ProductDbContext _dbContext;
        public ProductRepository(ProductDbContext prodDbContext)
        {
            _dbContext = prodDbContext;
            Add(new Product { Description = "Product 1 Description", Model = "Product 1 Model", Brand = "Product 1 Brand" });
            Add(new Product { Description = "Product 2 Description", Model = "Product 2 Model", Brand = "Product 2 Brand" });
            Add(new Product { Description = "Product 2 Description", Model = "Product 3 Model", Brand = "Product 3 Brand" });
        }
        public Product Add(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            var products = this._dbContext.Products.ToList();
            return products;
        }

        public Product GetProduct(int id)
        {
            return this._dbContext.Products.FirstOrDefault(e => e.Id == id);
        }

        public void Remove(int id)
        {
            var product = this._dbContext.Products.FirstOrDefault(e => e.Id == id);
            this._dbContext.Products.Remove(product);
            this._dbContext.SaveChanges();
        }

        public Product Update(Product product)
        {
            _dbContext.Products.Add(product);
            return product;
        }
    }
}