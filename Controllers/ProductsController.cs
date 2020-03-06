using Microsoft.AspNetCore.Mvc;
using ProdHiFiApi.Models;
using ProdHiFiApi.Models.Interface;

namespace ProdHiFiApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prodDbContext"></param>
        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get a product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var products = _repository.GetAll();
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
            return Ok(_repository.GetProduct(id));
        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            var newProduct = _repository.Add(product);
            return Created("Get", newProduct);
        }

        /// <summary>
        /// Create or update a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]Product product)
        {
            var newProduct = _repository.Add(product);
            return Created("Get", newProduct);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Remove(id);
            return Ok();
        }

    }
}