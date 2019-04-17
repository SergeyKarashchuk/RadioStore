using RadioStore.BusinessAccessLayer.AbstractsDTO;
using RadioStore.BusinessAccessLayer.CocreteDTO;
using RadioStore.BusinessAccessLayer.ModelsDTO;
using RadioStore.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View(CategoryId);
        }

        public PartialViewResult GetChildCategoryList(int? CategoryId)
        {
            
            var childsCategories = GetChilds(CategoryId).ToList();
            if (childsCategories.Count == 0)
            {
                HttpNotFound();
            }
            var viewModelCategories = childsCategories.Select(x =>
                                            new CategoryDTOViewModel
                                            {
                                                Category = x,
                                                IsNoChilds = GetChilds(x.CategoryId).ToArray().Length == 0
                                            });            
            return PartialView(viewModelCategories);
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

        private IQueryable<CategoryDTO> GetChilds(int? CategoryId)
        {
            var childList = uof.Category.GetAll()
                                     .Where(x => x.ParentCategoryId == CategoryId);                                     
            return childList;
        }
    }
}