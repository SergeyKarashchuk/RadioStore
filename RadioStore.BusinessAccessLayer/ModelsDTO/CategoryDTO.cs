using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.BusinessAccessLayer.ModelsDTO
{
    public class CategoryDTO
    {
        public CategoryDTO()
        {
            SpecificationTypes = new List<SpecificationTypeDTO>();
        }
        public int? CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }
        public int? ParentCategoryId { get; set; }
        public List<SpecificationTypeDTO> SpecificationTypes { get; set; }
    }
}
