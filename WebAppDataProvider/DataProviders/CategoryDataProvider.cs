using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppSqlServerDataProvider.Data;
using WebAppSqlServerDataProvider.Models;

namespace WebAppDataProvider
{
    public class CategoryDataProvider : ICategoryDataProvider
    {
        #region [ Fields ]
        private readonly IDbContextFactory<FStoreDBContext> _dbContextFactory;
        #endregion

        #region [ CTor ]
        public CategoryDataProvider(IDbContextFactory<FStoreDBContext> dbContextFactory) {
            _dbContextFactory = dbContextFactory;
        }
        #endregion

        #region [ CRUD ]
        public Category GetCategoryById(int id) {
            Category tempCategory = null;
            try {
                using var context = _dbContextFactory.CreateDbContext();
                tempCategory = context.Categories.FirstOrDefault(x => x.CategoryId == id);
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return tempCategory;
        }
        public void AddCategory(Category category) {
            try {
                var tempCategory = this.GetCategoryById(category.CategoryId);
                if (tempCategory == null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.Categories.Add(category);
                    context.SaveChanges();
                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }
        public void RemoveCategory(Category category) {
            try {
                Category tempCategory = this.GetCategoryById(category.CategoryId);
                if (tempCategory != null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.Categories.Remove(tempCategory);
                    context.SaveChanges();
                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }

        public void UpdateCategory(Category category) {
            try {
                Category tempCategory = this.GetCategoryById(category.CategoryId);
                if (tempCategory != null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.Categories.Update(category);
                    context.SaveChanges();

                } else {
                    throw new Exception();

                }
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }

        #endregion

        #region [ Get list ]
        public List<Category> FilterCategoryList(string name) {
            var categoryList = new List<Category>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                categoryList = context.Categories.Where(x => x.CategoryName.Contains(name)).ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return categoryList;
        }


        public List<Category> GetCategoryList() {
            var categoryList = new List<Category>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                categoryList = context.Categories.ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return categoryList;
        }
        #endregion

    }
}
