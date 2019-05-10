using RadioStore.BusinessAccessLayer.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RadioStore.WebApplication.Models
{
    public class ProductDTOViewModel
    {
        public IEnumerable<ProductDTO> Products { get; set; }
        public SpecificationTypeDTO[] Specifications { get; set; }
        public int? CategoryId { get; set; } = null;
        public int PageCount { get; set; }
    }
}