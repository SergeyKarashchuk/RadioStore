using RadioStore.BusinessAccessLayer.AbstractsDTO;
using RadioStore.BusinessAccessLayer.ModelsDTO;
using RadioStore.DataAccessLayer.Abstracts;
using RadioStore.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.BusinessAccessLayer.CocreteDTO
{
    public class CategoryService : IService<CategoryDTO>
    {
        private IUnitOfWork uof;
        public CategoryService(IUnitOfWork uof)
        {
            this.uof = uof;
        }

        public void Add(CategoryDTO obj)
        {
            throw new NotImplementedException();
        }

        public CategoryDTO Get(int? id)
        {
            var item = this.GetAll().FirstOrDefault(x => x.CategoryId == id);
            return item;
        }

        public IQueryable<CategoryDTO> GetAll()
        {
            var list = uof.Categories
                            .GetAll()
                                .Select(x => new CategoryDTO
                                {
                                    CategoryId = x.CategoryId,
                                    CategoryImage = x.CategoryImage,
                                    CategoryName = x.CategoryName,
                                    ParentCategoryId = x.CategoryParentId
                                });
            return list;
        }        
    }
}
