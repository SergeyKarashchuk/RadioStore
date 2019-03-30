using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.BusinessAccessLayer.ModelsDTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
