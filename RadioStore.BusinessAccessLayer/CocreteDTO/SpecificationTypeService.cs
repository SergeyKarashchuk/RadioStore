using RadioStore.BusinessAccessLayer.AbstractsDTO;
using RadioStore.BusinessAccessLayer.ModelsDTO;
using RadioStore.DataAccessLayer.Abstracts;
using RadioStore.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.BusinessAccessLayer.CocreteDTO
{
    public class SpecificationTypeService : IService<SpecificationTypeDTO>
    {
        private IUnitOfWork uof;
        public SpecificationTypeService(IUnitOfWork uof)
        {
            this.uof = uof;
        }

        public void Add(SpecificationTypeDTO obj)
        {
            SpecificationType newSpecType = new SpecificationType
            {
                IsInTableValue = obj.IsInTableValue,
                SpecificationName = obj.SpecificationName
            };
            uof.SpecificationTypes.Create(newSpecType);
            uof.SaveChanges();
        }

        public SpecificationTypeDTO Get(int? id)
        {
            var item = this.GetAll().FirstOrDefault(x => x.SpecificationTypeId == id);
            return item;
        }

        public IQueryable<SpecificationTypeDTO> GetAll()
        {
            var list = uof.SpecificationTypes
                            .GetAll()
                            .Select(x => new SpecificationTypeDTO
                            {
                                SpecificationTypeId = x.SpecificationTypeId,
                                IsInTableValue = x.IsInTableValue,
                                SpecificationName = x.SpecificationName,
                                Categories = x.SpecificationsToCategories.Select(y => new CategoryDTO
                                {
                                    CategoryId = y.Category.CategoryId,
                                    CategoryName = y.Category.CategoryName
                                }).ToList()
                            }                                
            );
            return list;
        }

        public void Remove(SpecificationTypeDTO obj)
        {
            var itemForRemove = uof.SpecificationTypes.Get(obj.SpecificationTypeId.Value);
            if (itemForRemove != null)
            {
                uof.SpecificationTypes.Remove(itemForRemove);
                uof.SaveChanges();
            }
        }

        public void Update(SpecificationTypeDTO obj)
        {
            var itemForUpdate = uof.SpecificationTypes.Get(obj.SpecificationTypeId.Value);
            if (itemForUpdate != null)
            {
                itemForUpdate.IsInTableValue = obj.IsInTableValue;
                itemForUpdate.SpecificationName = obj.SpecificationName;

                var newList = obj.Categories;
                var oldList = itemForUpdate.SpecificationsToCategories;
                if (newList.Count > oldList.Count)
                {
                    var newCategoryDTO = newList.FirstOrDefault(x => !oldList.Any(y => y.CategoryId == x.CategoryId));
                    var newCategory = uof.Categories.Get(newCategoryDTO.CategoryId.Value);
                    var newCategoryToSpecification = new SpecificationsToCategory()
                    {
                        SpecificationTypeId = itemForUpdate.SpecificationTypeId,
                        CategoryId = newCategory.CategoryId
                    };
                    uof.SpecificationsToCategories.Create(newCategoryToSpecification);
                }
                else if (newList.Count < oldList.Count)
                {
                    var categoryForRemove = oldList.FirstOrDefault(x => !newList.Any(y => x.CategoryId == y.CategoryId));
                    if (categoryForRemove != null)
                    {
                        uof.SpecificationsToCategories.Remove(categoryForRemove);
                    }
                }
                uof.SpecificationTypes.Update(itemForUpdate);
                uof.SaveChanges();
            }
        }
    }
}
