using Contracts;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext ctx;
        private ICategoryRepository cat;
        private IProductRepository prod;

        public RepositoryWrapper(RepositoryContext ctx)
        {
            this.ctx = ctx;
        }

        public ICategoryRepository CategoryWrapper
        {
            get
            {
                if (cat == null)
                    cat = new CategoryRepository(ctx);

                return cat;
            }
        }

        public IProductRepository ProductWrapper
        {
            get
            {
                if (prod == null)
                    prod = new ProductRepository(ctx);

                return prod;
            }

        }

        public async Task SaveAsync()
        {
            await ctx.SaveChangesAsync();
        }
    }
}
