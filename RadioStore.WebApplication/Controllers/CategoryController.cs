﻿using RadioStore.BusinessAccessLayer.AbstractsDTO;
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

        public  ActionResult Index(int? CategoryId = null)
        {
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
                                              Childs = GetChilds(x.CategoryId).ToList(),                                              
                                          });
            return viewModelCategories;
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditCategory(int? CategoryId = null)
        {
            var categoryForEdit = uof.Category.Get(CategoryId);
            
            var parentCategoryList = uof.Category
                                        .GetAll()
                                        .Where(x => x.CategoryId != CategoryId)
                                        .ToList();

            ViewBag.ParrentCategorySelectList = parentCategoryList
                                                    .Where(x => !uof.Products.GetAll()
                                                    .Any(y => y.CategoryId == x.CategoryId));
            return View(categoryForEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult EditCategory(CategoryDTO newCategory)
        {     
            
            if (!ModelState.IsValid)
            {                
                ViewBag.ParrentCategorySelectList = uof.Category
                                       .GetAll()
                                       .Where(x => x.CategoryId != newCategory.CategoryId)
                                       .ToList();
                return View(newCategory);
            }

            if (newCategory.ParentCategoryId == 0)
            {
                newCategory.ParentCategoryId = null;
            }

            if (newCategory.CategoryId == null)
            {
                uof.Category.Add(newCategory);
            }
            else
            {
                uof.Category.Update(newCategory);
            }            
            return RedirectToAction("Index");
        }



    }
}