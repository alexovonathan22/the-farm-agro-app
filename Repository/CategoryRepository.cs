using Contracts;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext ctx)
            : base(ctx)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await FindAll()
               .OrderBy(c => c.CatId)
               .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await FindByCondition(cat => cat.CatId.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<Category> GetCategoryWithProductAsync(int id)
        {
            return await FindByCondition(cat => cat.CatId.Equals(id))
                .Include(prod => prod.Products)
                .FirstOrDefaultAsync();
        }

        public void CreateCategory(Category cat)
        {
            Create(cat);
        }

        public void UpdateCategory(Category cat)
        {
            Update(cat);
        }

        public void DeleteCategory(Category cat)
        {
            Delete(cat);
        }



    }
}
