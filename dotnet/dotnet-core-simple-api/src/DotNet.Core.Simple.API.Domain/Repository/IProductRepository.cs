using DotNet.Core.Simple.API.Domain.Entity;
using DotNet.Core.Simple.API.Domain.SeedWork;

namespace DotNet.Core.Simple.API.Domain.Repository;
public interface IProductRepository :
    IGenericRepository<Product>
{
}
