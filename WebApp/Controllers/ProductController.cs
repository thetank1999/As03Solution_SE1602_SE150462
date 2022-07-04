using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAppDataProvider;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {   
        // remember -> INTERFACE
        // dependency injection: 
        private readonly IProductDataProvider _productDataProvider;
        public ProductController(IConfiguration configuration, IProductDataProvider _productDataProvider) {
            this._productDataProvider = _productDataProvider;
        }
        public IActionResult Index() {
            var productList = this._productDataProvider.GetProductList();
            return View(productList);
        }
    }
}
