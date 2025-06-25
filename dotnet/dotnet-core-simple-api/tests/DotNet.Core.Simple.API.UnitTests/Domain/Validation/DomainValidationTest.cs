using DotNet.Core.Simple.API.Domain.Exceptions;
using DotNet.Core.Simple.API.Domain.Validation;

namespace DotNet.Core.Simple.API.UnitTests.Domain.Validation;
public class DomainValidationTest
{
    [Theory(DisplayName = nameof(ShouldThrowExceptionWhenValueIsNull))]
    [Trait("Domain", "Validation - DomainValidation")]
    [InlineData("TestField", null)]
    public void ShouldThrowExceptionWhenValueIsNull(string fieldName, string? value)
    {
        // Act & Assert
        var action = () => DomainValidation.ValidateNotNull(value, fieldName);
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be null.");
    }

    [Theory(DisplayName = nameof(ShouldThrowExceptionWhenValueIsEmpty))]
    [Trait("Domain", "Validation - DomainValidation")]
    [InlineData("TestField", "")]
    [InlineData("TestField", "    ")]
    public void ShouldThrowExceptionWhenValueIsEmpty(string fieldName, string? value)
    {
        // Act & Assert
        var action = () => DomainValidation.ValidateNotEmpty(value, fieldName);
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be empty.");
    }
}
