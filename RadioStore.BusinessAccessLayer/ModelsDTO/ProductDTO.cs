﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.BusinessAccessLayer.ModelsDTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<PriceProductDTO> Prices { get; set; }
    }
}