using RadioStore.BusinessAccessLayer.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.BusinessAccessLayer.AbstractsDTO
{
    public interface IUnitOfWorkDTO
    {
        IService<CategoryDTO> Category { get; }
        IService<ProductDTO> Products { get; }
        IService<SpecificationTypeDTO> SpecificationTypes { get; }
    }
}
