using Microsoft.EntityFrameworkCore;
using ProdHiFiApi.Models;

namespace ProdHiFiApi.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}