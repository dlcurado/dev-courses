namespace DotNet.Core.Simple.API.Domain.SeedWork;
public abstract class AggregateRoot : Entity
{
    protected AggregateRoot() : base() { }
    /*
    protected AggregateRoot(Guid id) : base(id) { }
    public override bool IsValid() => true;
    public override void Validate() { }
    public override void ClearNotifications() { }
    public override string ToString() => $"{GetType().Name} - Id: {Id}";
    */
}
