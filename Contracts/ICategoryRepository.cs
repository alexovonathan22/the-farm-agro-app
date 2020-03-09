using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICategoryRepository 
    {
        Task<IEnumerable<Category>> GetAllCategoryAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> GetCategoryWithProductAsync(int id);
        void CreateCategory(Category cat);
        void UpdateCategory(Category cat);
        void DeleteCategory(Category cat);
    }
}
