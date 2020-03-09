using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProdHiFiApi.Models;
using ProdHiFiApi.Models.CustomModels;
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
        private IRepositoryWrapper _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prodDbContext"></param>
        public ProductsController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.EmailAddress))
            {
                return BadRequest("Unable to create token");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,loginModel.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0123456789ABCDEF"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("http://programmingfundas.com", "http://programmingfundas.com", claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        /// <summary>
        /// Get a product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _repository.Product.GetAllProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Get a product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _repository.Product.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductViewModel productViewModel)
        {
            var newProduct = new Product
            {
                Description = productViewModel.Description,
                Model = productViewModel.Model,
                Brand = productViewModel.Brand
            };
            _repository.Product.CreateProduct(newProduct);
            await _repository.SaveAsync();
            return Ok();
        }

        /// <summary>
        /// Create or update a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ProductViewModel productViewModel)
        {
            if (!productViewModel.Id.HasValue)
            {
                return BadRequest();
            }

            var existingProduct = await _repository.Product.GetProductByIdAsync(productViewModel.Id.Value);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct = new Product
            {
                Id = productViewModel.Id.Value,
                Description = productViewModel.Description,
                Model = productViewModel.Model,
                Brand = productViewModel.Brand
            };
            _repository.Product.UpdateProduct(existingProduct);
            await _repository.SaveAsync();
            return Ok();
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _repository.Product.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _repository.Product.RemoveProduct(product);
            await _repository.SaveAsync();
            return Ok();
        }

    }
}