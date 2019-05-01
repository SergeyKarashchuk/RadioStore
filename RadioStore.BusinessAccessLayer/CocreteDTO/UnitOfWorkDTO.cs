using RadioStore.BusinessAccessLayer.AbstractsDTO;
using RadioStore.BusinessAccessLayer.ModelsDTO;
using RadioStore.DataAccessLayer.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.BusinessAccessLayer.CocreteDTO
{
    public class UnitOfWorkDTO : IUnitOfWorkDTO
    {
        private IUnitOfWork uof;
        public UnitOfWorkDTO(IUnitOfWork uof)
        {
            this.uof = uof;
            Category = new CategoryService(uof);
            Products = new ProductService(uof);
            SpecificationTypes = new SpecificationTypeService(uof);
        }
        
        public IService<CategoryDTO> Category { get; }
        public IService<ProductDTO> Products { get; }
        public IService<SpecificationTypeDTO> SpecificationTypes { get; }
    }
}
