using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RadioStore.WebApplication.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int CategoryId)
        {
            return View();
        }
    }
}