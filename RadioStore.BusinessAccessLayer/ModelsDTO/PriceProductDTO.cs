using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.BusinessAccessLayer.ModelsDTO
{
    public class PriceProductDTO
    {
        [Required]
        [Range(1, int.MaxValue)]       
        public int ProductCount { get; set; }

        [Required]
        [Range(0.001, double.MaxValue)]
        public decimal Price { get; set; }
        public bool IsNewPrice { get; set; }
        public bool IsRemove { get; set; }
    }
}
