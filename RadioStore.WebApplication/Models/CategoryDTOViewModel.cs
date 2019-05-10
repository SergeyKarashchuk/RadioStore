using RadioStore.BusinessAccessLayer.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RadioStore.WebApplication.Models
{
    public class CategoryDTOViewModel
    {
        public CategoryDTOViewModel()
        {
            Childs = new List<CategoryDTO>();
        }
        public CategoryDTO Category { get; set; }
        public List<CategoryDTO> Childs { get; set; }        
    }
}