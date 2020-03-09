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
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext ctx)
            :base(ctx)
        {

        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await FindAll()
               .OrderBy(c => c.ProductId)
               .ToListAsync();
        }
        public IEnumerable<Product> GetProdInCat(int catId)
        {
            return FindByCondition(p => p.Category.CatId.Equals(catId))
                .ToList();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await FindByCondition(p => p.ProductId.Equals(id))
                .FirstOrDefaultAsync();
        }

        //public async Task<Product> GetProductWithProductAsync(int id)
        //{
        //    return await FindByCondition(cat => cat.CatId.Equals(id))
        //        .Include(prod => prod.Products)
        //        .FirstOrDefaultAsync();
        //}

        public void CreateProduct(Product prod)
        {
            Create(prod);
        }

        public void UpdateProduct(Product prod)
        {
            Update(prod);
        }

        public void DeleteProduct(Product prod)
        {
            Delete(prod);
        }

    }
}