using RadioStore.BusinessAccessLayer.AbstractsDTO;
using RadioStore.BusinessAccessLayer.CocreteDTO;
using RadioStore.BusinessAccessLayer.ModelsDTO;
using RadioStore.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RadioStore.WebApplication.Controllers
{
    public class CategoryController : Controller
    {
        private IUnitOfWorkDTO uof;
        public CategoryController(IUnitOfWorkDTO uof)
        {
            this.uof = uof;
        }

        public ActionResult Index(int? CategoryId)
        {
            //CategoryId = CategoryId == 0 ? null : CategoryId;
            return View(CategoryId);
        }

        public PartialViewResult GetChildCategoryList(int? CategoryId)
        {            
            return PartialView(GetCategoryViewModel(CategoryId));
        }

        public PartialViewResult GetParrentsCategoryLinks(int? CategoryId)
        {
            CategoryDTO cur = uof.Category.GetAll().FirstOrDefault(x => x.CategoryId == CategoryId);
            var parrentsCategories = new List<CategoryDTO>();
            while (cur != null)
            {
                parrentsCategories.Add(cur);
                cur = uof.Category.Get(cur.ParentCategoryId);
            }
            return PartialView(parrentsCategories);
        }

        public PartialViewResult GetCategoryNavItems(int? CategoryId = null)
        {
            return PartialView(GetCategoryViewModel(CategoryId));
        }

        private IQueryable<CategoryDTO> GetChilds(int? CategoryId)
        {
            var childList = uof.Category.GetAll()
                                     .Where(x => x.ParentCategoryId == CategoryId);                                     
            return childList;
        }

        private IEnumerable<CategoryDTOViewModel> GetCategoryViewModel(int? CategoryId)
        {
            var childsCategories = GetChilds(CategoryId).ToList();
            var viewModelCategories = childsCategories.Select(x =>
                                          new CategoryDTOViewModel
                                          {
                                              Category = x,
                                              Childs = GetChilds(x.CategoryId).ToList()                                              
                                          });
            return viewModelCategories;
        }

        public ActionResult EditCategory(int? CategoryId = null)
        {
            var categoryForEdit = uof.Category.Get(CategoryId);
            return View(categoryForEdit);
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryDTO newCategory)
        {
            return RedirectToAction("Index");
        }

    }
}