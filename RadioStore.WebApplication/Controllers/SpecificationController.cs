using LinqKit;
using RadioStore.BusinessAccessLayer.AbstractsDTO;
using RadioStore.BusinessAccessLayer.ModelsDTO;
using RadioStore.WebApplication.Infrastructure;
using RadioStore.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RadioStore.WebApplication.Controllers
{
    [Authorize(Roles ="admin")]
    public class SpecificationController : Controller
    {
        private IUnitOfWorkDTO uof;
        private ItemSelector<SpecificationTypeDTO> selector;
        private int itemsPerPage = 20;
        public SpecificationController(IUnitOfWorkDTO uof)
        {
            this.uof = uof;
            selector = new ItemSelector<SpecificationTypeDTO>(uof.SpecificationTypes);
        }

        public async Task<ActionResult> Index()
        {
            var model = await GetViewModelAsync();
            return View(model);
        }

        public async Task<PartialViewResult> PartialSpecificationTypesList(int pageId = 0)
        {
            Session["LastSpecificationTypesPage"] = pageId;
            var viewModel = await GetViewModelAsync();
            return PartialView(viewModel);
        }

        private int GetLastPageId()
        {
            object obj = Session["LastSpecificationTypesPage"];
            int lastpage = obj != null ? (int)obj : 1;
            return lastpage;
        }

        private async Task<SpecificationTypeDTOViewModel> GetViewModelAsync()
        {
            int pageId = GetLastPageId();

            //change for filters
            /////
            var predicate = PredicateBuilder.New<SpecificationTypeDTO>(true);
            /////

            int pageCount = await Task.Factory.StartNew(() => selector.GetPageCount(itemsPerPage, predicate));
            var list = await Task.Factory.StartNew(() => selector.GetItemList(predicate)
                                .OrderBy(x => x.SpecificationName)
                                .Skip((pageId - 1) * itemsPerPage)
                                .Take(itemsPerPage));
            var model = new SpecificationTypeDTOViewModel()
            {
                Specifications = list.ToList(),
                PageCount = pageCount
            };
            return model;
        }

        public ActionResult EditSpecificationType(int? SpecificationTypeId = null)
        {
            var specificationTypeForEdit = uof.SpecificationTypes.Get(SpecificationTypeId);           
            return View(specificationTypeForEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSpecificationType(SpecificationTypeDTO newSpecificationType)
        {
            if (!ModelState.IsValid)
            {
                return View(newSpecificationType);
            }

            if (newSpecificationType.SpecificationTypeId == null)
            {
                uof.SpecificationTypes.Add(newSpecificationType);
            }
            else
            {
                uof.SpecificationTypes.Update(newSpecificationType);
            }

            return RedirectToAction("Index");
        }
        public ActionResult CategoriesForSpecificationType(int? SpecificationTypeId)
        {
            var specificationType = uof.SpecificationTypes.Get(SpecificationTypeId);
            ViewBag.CategoryList = uof.Category.GetAll();
            return View(specificationType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveCategoryFromSpecification(int? SpecificationTypeId, int CategoryId)
        {
            var specification = uof.SpecificationTypes.Get(SpecificationTypeId);
            var categoryForRemove = specification.Categories.FirstOrDefault(x => x.CategoryId == CategoryId);
            if (specification != null && categoryForRemove != null)
            {
                specification.Categories.Remove(categoryForRemove);
                uof.SpecificationTypes.Update(specification);
            }
            return RedirectToAction("CategoriesForSpecificationType", new { SpecificationTypeId =  specification.SpecificationTypeId });
        }      
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSpecificationToCategory(int? SpecificationTypeId, int CategoryId)
        {
            var specification = uof.SpecificationTypes.Get(SpecificationTypeId);
            var categoryForAdd = uof.Category.Get(CategoryId);
            if (specification != null)
            {
                specification.Categories.Add(categoryForAdd);
                uof.SpecificationTypes.Update(specification);
            }
            return RedirectToAction("CategoriesForSpecificationType", new { specification.SpecificationTypeId });
        }

     
    }
}