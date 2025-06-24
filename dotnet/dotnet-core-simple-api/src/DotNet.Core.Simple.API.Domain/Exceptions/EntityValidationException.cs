namespace DotNet.Core.Simple.API.Domain.Exceptions;
public class EntityValidationException: Exception
{
    public EntityValidationException(string? message) : base(message)
    {
    }
}
