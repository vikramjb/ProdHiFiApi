using System.Collections.Generic;

namespace ProdHiFiApi.Models.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetProduct(int id);
        Product Add(Product product);
        void Remove(int id);
        Product Update(Product product);

    }
}