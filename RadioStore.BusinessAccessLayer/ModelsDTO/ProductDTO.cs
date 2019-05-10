using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.BusinessAccessLayer.ModelsDTO
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            Prices = new List<PriceProductDTO>();
            Specifications = new List<ProductSpecificationDTO>();
        }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int? CategoryId { get; set; }
        public bool IsPublished { get; set; }
        public List<PriceProductDTO> Prices { get; set; }
        public List<ProductSpecificationDTO> Specifications { get; set; }
    }
}
