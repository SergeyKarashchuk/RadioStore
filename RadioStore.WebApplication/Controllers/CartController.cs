using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RadioStore.WebApplication.Controllers
{
    //[Authorize]
    public class CartController : Controller
    {
        public ActionResult Index(string val)
        {
            return View(model: val);
        }

        [HttpPost]
        public ActionResult AddProductToCart(int? productId, int productCount)
        {
            var str = $"\"productId = {productId};productCount = {productCount};\"";
            return RedirectToAction("Index", new { val = str });
        }
    }
}