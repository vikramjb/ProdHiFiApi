using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdHiFiApi.Models
{
    public class ProductViewModel
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
    }
}