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
    public class ProductController : Controller
    {
        private IUnitOfWorkDTO uof;
        private ItemSelector<ProductDTO> selector;
        public ProductController(IUnitOfWorkDTO uof)
        {
            this.uof = uof;
            selector = new ItemSelector<ProductDTO>(uof.Products);
        }

        public async Task<ActionResult> Index(int? CategoryId = null)
        {
            var viewModel = await GetViewModelAsync(CategoryId);                       
            return View(viewModel);
        }

        public async Task<PartialViewResult> PartialProductList(int? CategoryId = null, int pageId = 0)
        {
            Session["LastProductPage"] = pageId;
            var viewModel = await GetViewModelAsync(CategoryId);                                          
            return PartialView(viewModel);
        }

        public async Task<PartialViewResult> PartialProductListWithPagination(int? CategoryId = null, int itemsPerPageCount = 5)
        {
            Session["ProductItemsPerPage"] = itemsPerPageCount;
            var viewModel = await GetViewModelAsync(CategoryId);
            return PartialView(viewModel);
        }       

        private int GetItemsPerPage()
        {
            object obj = Session["ProductItemsPerPage"];
            int items = obj != null ? (int)obj : 5;
            return items > 0 ? items : 5;
        }

        private int GetLastPageId()
        {
            object obj = Session["LastProductPage"];
            int lastpage = obj != null ? (int)obj : 1;
            return lastpage;
        }

        private async Task<ProductDTOViewModel> GetViewModelAsync(int? CategoryId = null)
        {
            int pageId = GetLastPageId();
            int itemsPerPage = GetItemsPerPage();

            //change for filters
            /////
            var predicate = PredicateBuilder.New<ProductDTO>(true);
            predicate.And(x => x.CategoryId == CategoryId);
            /////

            int pageCount = await Task.Factory.StartNew(() => selector.GetPageCount(itemsPerPage, predicate));
            var list = await Task.Factory.StartNew(() => selector.GetItemList(predicate)
                                .OrderBy(x => x.ProductName)
                                .Skip((pageId - 1) * itemsPerPage)
                                .Take(itemsPerPage));
            var model = new ProductDTOViewModel()
            {
                CategoryId = CategoryId,
                Products = list.ToList(),
                PageCount = pageCount,
                IsAdmin = true  //there will be is aush && admin              
            };
            return model;
        }
    }
}