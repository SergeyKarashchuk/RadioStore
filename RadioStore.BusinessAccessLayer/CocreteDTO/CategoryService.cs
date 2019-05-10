using RadioStore.BusinessAccessLayer.AbstractsDTO;
using RadioStore.BusinessAccessLayer.ModelsDTO;
using RadioStore.DataAccessLayer.Abstracts;
using RadioStore.DataAccessLayer.Concrete;
using RadioStore.DataAccessLayer.Entities;
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

        public CategoryDTO Get(int? id)
        {
            var item =  this.GetAll().FirstOrDefault(x => x.CategoryId == id);
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
                                ParentCategoryId = x.CategoryParentId,
                                SpecificationTypes = x.SpecificationsToCategories.Select(s => new SpecificationTypeDTO
                                {
                                    IsInTableValue = s.SpecificationType.IsInTableValue,
                                    SpecificationName = s.SpecificationType.SpecificationName,
                                    SpecificationTypeId = s.SpecificationType.SpecificationTypeId
                                }).ToList()
                            });           
            return list;
        }

        public void Add(CategoryDTO obj)
        {
            var newCategory = new Category
            {
                CategoryImage = obj.CategoryImage,
                CategoryName = obj.CategoryName,
                CategoryParentId = obj.ParentCategoryId
            };
            uof.Categories.Create(newCategory);
            uof.SaveChanges();
        }

        public void Remove(CategoryDTO obj)
        {
            throw new NotImplementedException();
        }

        public void Update(CategoryDTO obj)
        {
            var editCategory = uof.Categories.Get(obj.CategoryId.Value);
            if (editCategory != null)
            {
                editCategory.CategoryImage = obj.CategoryImage;
                editCategory.CategoryName = obj.CategoryName;
                editCategory.CategoryParentId = obj.ParentCategoryId;
                uof.Categories.Update(editCategory);
                uof.SaveChanges();
            }
        }
    }
}
