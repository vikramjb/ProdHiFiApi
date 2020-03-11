using System.Threading.Tasks;

namespace ProdHiFiApi.Models.Interface
{
    public interface IRepositoryWrapper
    {
        IProductRepository Product { get; }
    }
}