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
        public bool IsAdmin { get; set; } = false;
        public int? CategoryId { get; set; } = null;
        public int PageCount { get; set; }
    }
}