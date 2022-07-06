using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using WebAppDataProvider;
using WebAppSqlServerDataProvider.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller {
        #region [Fields]
        // remember -> INTERFACE
        // dependency injection: 
        private readonly IProductDataProvider _productDataProvider;
        private readonly IConfiguration _configuration;
        private readonly ICategoryDataProvider _categoryDataProvider;
        #endregion

        #region [ CTor ]
        public ProductController(   IConfiguration configuration, 
                                    IProductDataProvider _productDataProvider, 
                                    ICategoryDataProvider _categoryDataProvider) {

            this._productDataProvider = _productDataProvider;
            this._configuration = configuration;
            this._categoryDataProvider = _categoryDataProvider;
        }
        #endregion
        public IActionResult Index() {
            var productList = this._productDataProvider.GetProductList();
            foreach (var product in productList) {
                product.Category= _categoryDataProvider.GetCategoryById(product.CategoryId.Value);
            }
            return View(productList);
        }

        public IActionResult Details(int id) {
            var product = this._productDataProvider.GetProductById(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            var product = this._productDataProvider.GetProductById(id);
            var categoryList =  this._categoryDataProvider.GetCategoryList();
            var cateSL = new List<SelectListItem>();
            foreach (var item in categoryList) {
                var temp = new SelectListItem(item.CategoryName, item.CategoryId.ToString());
                cateSL.Add(temp);
                if (item.CategoryId == product.CategoryId) {
                    temp.Selected = true;
                }
            }
            ViewBag.CategoryId = cateSL;
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product) {
            try {
                if (ModelState.IsValid) {
                    _productDataProvider.UpdateProduct(product);
                }
                return RedirectToAction("Index");
            } catch (Exception ex) {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            var product = this._productDataProvider.GetProductById(id);
            var categoryList = this._categoryDataProvider.GetCategoryList();
            var cateSL = new List<SelectListItem>();
            foreach (var item in categoryList) {
                var temp = new SelectListItem(item.CategoryName, item.CategoryId.ToString());
                cateSL.Add(temp);
                if (item.CategoryId == product.CategoryId) {
                    temp.Selected = true;
                }
            }
            ViewBag.CategoryId = cateSL;
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product product) {
            _productDataProvider.RemoveProduct(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create() {
            var categoryList = this._categoryDataProvider.GetCategoryList();
            var cateSL = new List<SelectListItem>();
            foreach (var item in categoryList) {
                var temp = new SelectListItem(item.CategoryName, item.CategoryId.ToString());
                cateSL.Add(temp);
            }
            ViewBag.CategoryId = cateSL;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product) {
            try {
                if (ModelState.IsValid) {
                    _productDataProvider.AddProduct(product);
                }
                return RedirectToAction("Index");
            }catch (Exception ex) {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
