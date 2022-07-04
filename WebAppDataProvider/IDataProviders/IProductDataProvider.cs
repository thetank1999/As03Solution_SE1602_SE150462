using System.Collections.Generic;
using WebAppSqlServerDataProvider.Models;

namespace WebAppDataProvider
{
    public interface IProductDataProvider
    {
        public List<Product> FilterProductListByName(string name);
        public List<Product> GetProductList();
        public Product GetProductById(int id);
        public void AddProduct(Product product);
        public void UpdateProduct(Product product);
        public void RemoveProduct(Product product);
        public List<Product> FilterProductListById(string id);
        public List<Product> FilterProductListByPrice(string price);
        public List<Product> FilterProductListByNoInstock(string stock);
    }
}
