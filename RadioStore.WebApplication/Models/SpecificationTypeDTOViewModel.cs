using RadioStore.BusinessAccessLayer.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RadioStore.WebApplication.Models
{
    public class SpecificationTypeDTOViewModel
    {
        public IEnumerable<SpecificationTypeDTO> Specifications { get; set; }
        public int PageCount { get; set; }
    }
}