using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppSqlServerDataProvider.Data;
using WebAppSqlServerDataProvider.Models;

namespace WebAppDataProvider
{
    public class ProductDataProvider : IProductDataProvider
    {
        #region [ Fields ]
        private readonly IDbContextFactory<FStoreDBContext> _dbContextFactory;
        #endregion

        #region [ CTor ]
        public ProductDataProvider(IDbContextFactory<FStoreDBContext> dbContextFactory) {
            _dbContextFactory = dbContextFactory;
        }
        #endregion

        #region [ CRUD ]
        public Product GetProductById(int id) {
            Product tempProduct = null;
            try {
                using var context = _dbContextFactory.CreateDbContext();
                tempProduct = context.Products.FirstOrDefault(x => x.ProductId == id);
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return tempProduct;
        }
        public void AddProduct(Product product) {
            try {
                var tempProduct = GetProductById(product.ProductId);
                if (tempProduct == null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.Products.Add(product);
                    context.SaveChanges();
                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }
        public void RemoveProduct(Product product) {
            try {
                Product tempProduct = GetProductById(product.ProductId);
                if (tempProduct != null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.Products.Remove(tempProduct);
                    context.SaveChanges();
                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }

        public void UpdateProduct(Product product) {
            try {
                Product tempProduct = GetProductById(product.ProductId);
                if (tempProduct != null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.Products.Update(product);
                    context.SaveChanges();

                } else {
                    throw new Exception();

                }
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }
        #endregion

        #region [ Filter by ]
        public List<Product> FilterProductListById(string id) {
            var productList = new List<Product>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                productList = context.Products.Where(x => x.ProductId.ToString().Contains(id)).ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return productList;
        }

        public List<Product> FilterProductListByName(string name) {
            var productList = new List<Product>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                productList = context.Products.Where(x => x.ProductName.Contains(name)).ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return productList;
    }

        public List<Product> FilterProductListByNoInstock(string stock) {
            var productList = new List<Product>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                productList = context.Products.Where(x => x.UnitsInStock.ToString().Contains(stock)).ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return productList;
        }

        public List<Product> FilterProductListByPrice(string price) {
            var productList = new List<Product>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                productList = context.Products.Where(x => x.UnitPrice.ToString().Contains(price)).ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return productList;
        }
        #endregion

        #region [ GetList ]
        public List<Product> GetProductList() {
            var productList = new List<Product>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                productList = context.Products.ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return productList;
        }
        #endregion
    }
}
