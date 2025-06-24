using DotNet.Core.Simple.API.Domain.Exceptions;

namespace DotNet.Core.Simple.API.Domain.Validation;
public class DomainValidation
{
    public static void ValidateNotNull(object? value, string fieldName)
    {
        if (value is null)
            throw new EntityValidationException($"{fieldName} should not be null.");
    }
    public static void ValidateNotEmpty(string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EntityValidationException($"{fieldName} should not be empty.");
    }
    public static void ValidateGreaterThanZero(decimal value, string fieldName)
    {
        if (value <= 0)
            throw new EntityValidationException($"{fieldName} should greather than zero.");
    }
}
