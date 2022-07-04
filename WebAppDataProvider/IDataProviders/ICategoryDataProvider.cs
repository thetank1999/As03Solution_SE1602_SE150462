using System.Collections.Generic;
using WebAppSqlServerDataProvider.Models;

namespace WebAppDataProvider
{
    public interface ICategoryDataProvider
    {
        public List<Category> FilterCategoryList(string name);
        public List<Category> GetCategoryList();
        public Category GetCategoryById(int id);
        public void AddCategory(Category category);
        public void UpdateCategory(Category category);
        public void RemoveCategory(Category category);
    }
}
