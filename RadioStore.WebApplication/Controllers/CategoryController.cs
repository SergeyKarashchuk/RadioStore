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

            var childsCategories = uof.Category.GetAll()
                                    .Where(x => x.ParentCategoryId == CategoryId)
                                    .ToList();
            //if (childsCategories == null)
            //{
            //    RedirectToAction("Index", "Product");
            //}           
            return PartialView(childsCategories);
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
    }
}