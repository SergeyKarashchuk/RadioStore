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
    public static class CategoryViewManager
    {
        private static IUnitOfWork uof;
        static CategoryViewManager()
        {
            uof = new UnitOfWork();
        }

        public static IEnumerable<CategoryDTO> GetMenuItems(int? CategoryId = null)
        {
            var list = new List<CategoryDTO>();
            list.AddRange(uof.Categories
                                .GetAll()
                                    .Where(x => x.CategoryParentId == CategoryId)
                                    .Select(x => new CategoryDTO
                                    {
                                        CategoryId = x.CategoryId,
                                        CategoryImage = x.CategoryImage,
                                        CategoryName = x.CategoryName                                        
                                    }));
            return list;
        }
    }
}
