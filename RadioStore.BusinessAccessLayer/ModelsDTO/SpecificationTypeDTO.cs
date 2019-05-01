using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.BusinessAccessLayer.ModelsDTO
{
    public class SpecificationTypeDTO
    {
        public SpecificationTypeDTO()
        {
            Categories = new List<CategoryDTO>();
        }
        public int? SpecificationTypeId { get; set; }

        [Required]
        public string SpecificationName { get; set; }
        public bool IsInTableValue { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}
