using DotNet.Core.Simple.API.Domain.Entity;

namespace DotNet.Core.Simple.API.Domain.SeedWork;
public interface IGenericRepository<TAggregate> : IRepository
    where TAggregate : AggregateRoot
{
    public Task Insert(TAggregate aggregate, CancellationToken cancellationToken);
}
