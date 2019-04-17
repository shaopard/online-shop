using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onLineShop.Contracts.Repositories;
using onLineShop.Model;

namespace onLineShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        IRepositoryBase<Customer> customers;
        IRepositoryBase<Product> products;
        IRepositoryBase<ProductCategory> productCategories;


        public AdminController(IRepositoryBase<Customer> customers, IRepositoryBase<Product> products, IRepositoryBase<ProductCategory> productCategories)
        {
            this.customers = customers;
            this.products = products;
            this.productCategories = productCategories;
        }


        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ProductList()
        {
            var model = products.GetAll();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateProduct()
        {
            var model = new Product();
            var list = productCategories.GetAll();
            var selectlist = new List<SelectListItem>();
            foreach (var productCategory in list)
            {
                selectlist.Add(new SelectListItem { Text = productCategory.Description, Value = productCategory.ProductCategoryId.ToString() });
            }
            ViewBag.SelectList = selectlist;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateProduct(Product product, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    file.SaveAs(path);
                    product.ImageUrl = fileName;
                    products.Insert(product);
                    products.Commit();
                }
                ViewBag.Message = "Upload successful";
                
                return RedirectToAction("ProductList");
            }
            catch
            {
                ViewBag.Message = "Upload failed";
                return RedirectToAction("ProductList");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditProduct(int id)
        {
            var product = products.GetById(id);
            var list = productCategories.GetAll();
            var selectlist = new List<SelectListItem>();
            foreach (var productCategory in list)
            {
                selectlist.Add(new SelectListItem { Text = productCategory.Description, Value = productCategory.ProductCategoryId.ToString() });
            }
            ViewBag.SelectList = selectlist;

            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditProduct(Product product)
        {
            products.Update(product);
            products.Commit();

            return RedirectToAction("ProductList");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RemoveProduct(Product product)
        {
            products.Delete(product);
            products.Commit();

            return RedirectToAction("ProductList");
        }

        public class ViewDataUploadFilesResult
        {
            public string Name { get; set; }
            public int Length { get; set; }
        }

    }
}