using onLineShop.Contracts.Repositories;
using onLineShop.Model;
using onLineShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace onLineShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepositoryBase<Customer> customers;
        IRepositoryBase<Product> products;
        IRepositoryBase<ShoppingCart> shoppingCarts;
        ShoppingCartService _shoppingCartService;
       
        public HomeController(IRepositoryBase<Customer> customers, IRepositoryBase<Product> products, IRepositoryBase<ShoppingCart> shoppingCarts)
        {
            this.customers = customers;
            this.products = products;
            this.shoppingCarts = shoppingCarts;
            _shoppingCartService = new ShoppingCartService(this.shoppingCarts);//aici e problema zice ca e never assigned to
        }

        public ActionResult ShoppingCartSummary()
        {
            var model = _shoppingCartService.GetShoppingCart(HttpContext);

            return View(model.ShoppingCartItems);
        }

        public ActionResult AddToShoppingCart(int id)
        {
            _shoppingCartService.AddToShoppingCart(HttpContext, id, 1);

            return RedirectToAction("ShoppingCartSummary");
        }

        public ActionResult DeleteEntryFromShoppingCart(int id)
        {
            _shoppingCartService.RemoveFromShoppingCart(HttpContext, id);
            return RedirectToAction("ShoppingCartSummary");
        }


        public ActionResult Index(string productCategory = null)
        {
            var productList = products.GetAll();
            if (productCategory != null)
            {
                productList = productList.Where(p => productCategory.Equals(p.ProductCategory.Description));
            }
            return View(productList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details(int id)
        {
            var product = products.GetById(id);

            return View(product);
        }
    }
}