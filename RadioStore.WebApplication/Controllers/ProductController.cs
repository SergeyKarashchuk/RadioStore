using RadioStore.BusinessAccessLayer.AbstractsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RadioStore.WebApplication.Controllers
{
    public class ProductController : Controller
    {
        private IUnitOfWorkDTO uof;
        public ProductController(IUnitOfWorkDTO uof)
        {
            this.uof = uof;
        }

        // GET: Product
        public ActionResult Index(int? CategoryId = null)
        {
            var list = uof.Products.GetAll()
                            .Where(x => x.CategoryId == CategoryId)
                            .ToList();
            return View(list);
        }
    }
}