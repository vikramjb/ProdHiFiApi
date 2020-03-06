using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdHiFiApi.Data;
using ProdHiFiApi.Models;

namespace ProdHiFiApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductDbContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prodDbContext"></param>
        public ProductsController(ProductDbContext prodDbContext)
        {
            _dbContext = prodDbContext;
        }

        /// <summary>
        /// Get a product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var products = this._dbContext.Products.ToListAsync();
            return Ok(products);
        }

        /// <summary>
        /// Get a product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(this._dbContext.Products.FirstOrDefaultAsync(e => e.Id == id));
        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            this._dbContext.Products.AddAsync(product);
            this._dbContext.SaveChangesAsync();
            return Created("Get", product);
        }

        /// <summary>
        /// Create or update a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]Product product)
        {
            this._dbContext.Products.AddAsync(product);
            this._dbContext.SaveChangesAsync();
            return Created("Get", product);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = this._dbContext.Products.FirstOrDefault(e => e.Id == id);
            this._dbContext.Products.Remove(product);
            this._dbContext.SaveChangesAsync();
            var products = this._dbContext.Products.ToListAsync();
            return Ok(products);
        }

    }
}