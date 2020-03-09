using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        ICategoryRepository CategoryWrapper { get; }
        IProductRepository ProductWrapper { get; }
        Task SaveAsync();

    }
}
