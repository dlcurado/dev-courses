namespace DotNet.Core.Simple.API.Application.Interfaces;
public interface IUnitOfWork
{
    public Task Commit(CancellationToken cancellationToken);
    public Task Rollback(CancellationToken cancellationToken);
}
