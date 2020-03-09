using Contracts;
using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
       
    {
        private readonly RepositoryContext ctx;

        //protected RepositoryContext ctx { get; set; }
        public RepositoryBase(RepositoryContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(T entity)
        {
            ctx.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            ctx.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return ctx.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return ctx.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            ctx.Set<T>().Update(entity);

        }
    }
}
