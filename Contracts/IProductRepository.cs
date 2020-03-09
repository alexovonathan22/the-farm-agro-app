using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProductRepository 
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        IEnumerable<Product> GetProdInCat(int catId);
        Task<Product> GetProductByIdAsync(int id);
        //Task<Product> GetCatWithProductAsync(int id);
        void CreateProduct(Product prod);
        void UpdateProduct(Product prod);
        void DeleteProduct(Product prod);
    }
}
