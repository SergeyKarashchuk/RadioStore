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
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private IUnitOfWorkDTO uof;
        private ItemSelector<ProductDTO> selector;
        public ProductController(IUnitOfWorkDTO uof)
        {
            this.uof = uof;
            selector = new ItemSelector<ProductDTO>(uof.Products);
        }

        [AllowAnonymous]
        public ActionResult Index(int? CategoryId = null)
        {
            var viewModel = GetViewModel(CategoryId);                       
            return View(viewModel);
        }

        [AllowAnonymous]
        public PartialViewResult PartialProductList(int? CategoryId = null, int pageId = 0)
        {
            Session["LastProductPage"] = pageId;
            var viewModel = GetViewModel(CategoryId);                                          
            return PartialView(viewModel);
        }

        [AllowAnonymous]
        public  PartialViewResult PartialProductListWithPagination(int? CategoryId = null, int itemsPerPageCount = 5)
        {
            Session["ProductItemsPerPage"] = itemsPerPageCount;
            var viewModel = GetViewModel(CategoryId);
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

        private ProductDTOViewModel GetViewModel(int? CategoryId = null)
        {
            int pageId = GetLastPageId();
            int itemsPerPage = GetItemsPerPage();

            /////filters
            var IsAdmin = Request.IsAuthenticated && User.IsInRole("admin");
            
            var predicate = PredicateBuilder.New<ProductDTO>(true);
            predicate.And(x => x.CategoryId == CategoryId);
            if (!IsAdmin)
            {
                predicate.And(x => x.IsPublished);
            }
            /////

            int pageCount = selector.GetPageCount(itemsPerPage, predicate);
            var list = selector.GetItemList(predicate)
                                .OrderBy(x => x.ProductName)
                                .Skip((pageId - 1) * itemsPerPage)
                                .Take(itemsPerPage);
            var model = new ProductDTOViewModel()
            {
                CategoryId = CategoryId,
                Products = list.ToList(),
                PageCount = pageCount,
                Specifications = uof.Category.Get(CategoryId)?.SpecificationTypes.ToArray()
            };
            return model;
        }

        public ActionResult EditProduct(int? productId = null)
        {
            var productForEdit = uof.Products.Get(productId);
            var categoryList = uof.Category.GetAll().ToList();
            var rootCategoryListForProducts = categoryList.Where(x => !uof.Category.GetAll().Any(y => y.ParentCategoryId == x.CategoryId));
            ViewBag.CategoryList = rootCategoryListForProducts;
            return View(productForEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(ProductDTO newProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(newProduct);
            }
            if (newProduct.ProductId == null)
            {
                uof.Products.Add(newProduct);
            }
            else
            {
                uof.Products.Update(newProduct);
            }
            return RedirectToAction("Index", new { newProduct.CategoryId });
        }

        private List<PriceProductDTO> productPriceList;
        private List<PriceProductDTO> ProductPriceList
        {
            get
            {
                if (productPriceList == null)
                    productPriceList = Session["ProductPriceList"] as List<PriceProductDTO>;
                return productPriceList;
            }
            set
            {
                productPriceList = value;
                Session["ProductPriceList"] = productPriceList;
            }
        }

        public ActionResult EditProductPrices(int? productId)
        {
            var productForEdit = uof.Products.Get(productId);
            if (productForEdit == null)
            {
                return HttpNotFound("Product not found");
            }
            ViewBag.ProductId = productForEdit.ProductId;
            ViewBag.ProductName = productForEdit.ProductName;

            if (ProductPriceList == null)
            {
                ProductPriceList = productForEdit.Prices;
            }
            return View(ProductPriceList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductPrices(List<PriceProductDTO> priceProductList, int? productId, string command)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProductId = productId;
                return View("EditProductPrices", new { productId, priceProductList });
            }            
            switch (command)
            {
                case "Add item":
                    if (priceProductList == null)
                    {
                        priceProductList = new List<PriceProductDTO>();
                    }
                    priceProductList.Add(new PriceProductDTO { IsRemove = false , IsNewPrice = true});
                    break;                

                default:
                    var itemForEdit = uof.Products.Get(productId);
                    if (itemForEdit == null)
                    {
                        ProductPriceList = null;
                        return HttpNotFound("Product not found");
                    }
                    itemForEdit.Prices = priceProductList;
                    ProductPriceList = null;
                    uof.Products.Update(itemForEdit);
                    return RedirectToAction("Index");
            }
            ProductPriceList = priceProductList;
            return RedirectToAction("EditProductPrices", new { productId });
        }

        public ActionResult EditProductSpecifications(int? productId)
        {
            var productForEdit = uof.Products.Get(productId);
            if (productForEdit == null)
            {
                return HttpNotFound("Product not found");
            }
            ViewBag.ProductId = productForEdit.ProductId;
            ViewBag.ProductName = productForEdit.ProductName;

            var specList = productForEdit.Specifications;
            return View(specList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductSpecifications(List<ProductSpecificationDTO> specList, int? productId, string command)
        {
            var productForEdit = uof.Products.Get(productId);           
            if (command == "Update types from category")
            {
                var categoryOfProduct = uof.Category.Get(productForEdit?.CategoryId);
                if (categoryOfProduct != null)
                {
                    productForEdit.Specifications.ForEach(x => x.IsRemove = true);
                    foreach (var item in categoryOfProduct.SpecificationTypes)
                    {
                        var specItem = productForEdit.Specifications.Find(x => x.SpecificationTypeId == item.SpecificationTypeId);
                        if (specItem != null)
                        {
                            specItem.IsRemove = false;
                        }
                        else
                        {
                            productForEdit.Specifications.Add(new ProductSpecificationDTO
                            {
                                IsNewSpec = true,
                                IsRemove = false,
                                SpecificationTypeId = item.SpecificationTypeId.Value
                            });
                        }
                    }
                }
                uof.Products.Update(productForEdit);
                return RedirectToAction("EditProductSpecifications", new { productId = productForEdit.ProductId });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ProductId = productId;
                return View(productForEdit.Specifications);
            }
            productForEdit.Specifications = specList;
            uof.Products.Update(productForEdit);
            return RedirectToAction("Index", new { productForEdit.CategoryId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePublishProductAttribute(int? productId)
        {
            var productForEdit = uof.Products.Get(productId);
            if (productId == null)
            {
                return HttpNotFound();
            }
            productForEdit.IsPublished = !productForEdit.IsPublished;
            uof.Products.Update(productForEdit);
            return RedirectToAction("Index", new { productForEdit.CategoryId });
        }
    }
}