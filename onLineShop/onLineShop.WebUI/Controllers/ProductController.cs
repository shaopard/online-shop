using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onLineShop.Contracts.Repositories;
using onLineShop.Model;

namespace onLineShop.WebUI.Controllers
{
    public class ProductController : Controller
    {

        IRepositoryBase<Customer> customers;
        IRepositoryBase<Product> products;
        //IRepositoryBase<CosCumparaturi> cosuri;


        public ProductController(IRepositoryBase<Customer> customers, IRepositoryBase<Product> products)
        {
            this.customers = customers;
            this.products = products;
        }

        // GET: Product
        public ActionResult Index()
        {
            ViewBag.Message = "Lista de produse";
            var productsInDb = products.GetAll().ToList();

            return View();
        }


    }
} 