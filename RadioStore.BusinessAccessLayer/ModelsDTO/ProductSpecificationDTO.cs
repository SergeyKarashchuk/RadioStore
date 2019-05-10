using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.BusinessAccessLayer.ModelsDTO
{
    public class ProductSpecificationDTO
    {
        public int SpecificationId { get; set; }
        public int SpecificationTypeId { get; set; }
        public string SpecificationValue { get; set; }
        public string SpecificationType { get; set; }
        public bool IsNewSpec { get; set; }
        public bool IsRemove { get; set; }
    }
}
