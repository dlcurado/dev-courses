using Bogus;
using FC.Codeflix.Catalog.Domain.Exceptions;
using FC.Codeflix.Catalog.Domain.Validation;
using FluentAssertions;

namespace FC.Codeflix.Catalog.UnitTests.Domain.Validation;
public class DomainValidationTest
{
    private Faker Faker {  get; set; } = new Faker();
    private string GetFieldName()
    {
        return Faker.Commerce.ProductName().Replace(" ", "");
    }

    [Fact(DisplayName = nameof(NotNullOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullOk()
    {
        var value = Faker.Commerce.ProductName();
        Action action = () => DomainValidation.NotNull(value, GetFieldName());
        action.Should()
           .NotThrow();
    }

    

    [Fact(DisplayName = nameof(NotNullThrowWhenNull))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullThrowWhenNull()
    {
        string? target = null;
        var fieldName = GetFieldName();
        Action action = () => DomainValidation.NotNull(target, fieldName);
        action.Should()
           .Throw<EntityValidationException>()
           .WithMessage($"{fieldName} should not be null");
    }

    [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
    [Trait("Domain", "DomainValidation - Validation")]
    [InlineData(null)]
    [InlineData("")]
    public void NotNullOrEmptyThrowWhenEmpty(string? target)
    {
        var fieldName = GetFieldName();
        Action action = () => DomainValidation.NotNullOrEmpty(target, fieldName);
        action.Should()
           .Throw<EntityValidationException>()
           .WithMessage($"{fieldName} should not be empty or null");
    }

    [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullOrEmptyOk()
    {
        var fieldName = GetFieldName();
        string target = Faker.Commerce.ProductName();
        Action action = () => DomainValidation.NotNull(target, fieldName);
        action.Should()
           .NotThrow();
    }


    [Theory(DisplayName = nameof(MinLenghtThrowWhenLess))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesForMinLenght), parameters: 9)]
    public void MinLenghtThrowWhenLess(string target, int minLenght)
    {
        var fieldName = GetFieldName();
        Action action = () => DomainValidation.MinLenght(target, minLenght, fieldName);
        action.Should()
           .Throw<EntityValidationException>()
           .WithMessage($"{ fieldName } should be at least { minLenght } characters long");
    }

    public static IEnumerable<object[]> GetValuesForMinLenght(int numberOfTests = 5) 
    {
        yield return new object[] { "123456", 10 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var target = faker.Commerce.ProductName();
            var minLenght = target.Length + (new Random()).Next(1, 20);
            yield return new object[] { target, minLenght };
        }
    }


    [Theory(DisplayName = nameof(MinLenghtOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesGratherThanMinLenght), parameters: 9)]
    public void MinLenghtOk(string target, int minLenght)
    {
        var fieldName = GetFieldName();
        Action action = () => DomainValidation.MinLenght(target, minLenght, fieldName);
        action.Should()
           .NotThrow();
    }

    public static IEnumerable<object[]> GetValuesGratherThanMinLenght(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 6 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var target = faker.Commerce.ProductName();
            var minLenght = target.Length - (new Random()).Next(1, target.Length);
            yield return new object[] { target, minLenght };
        }
    }

    [Theory(DisplayName = nameof(MaxLenghtThrowWhenGrather))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesGreatherThanMaxLenght), parameters: 9)]
    public void MaxLenghtThrowWhenGrather(string target, int maxLenght)
    {
        var fieldName = GetFieldName();
        Action action = () => DomainValidation.MaxLenght(target, maxLenght, fieldName);
        action.Should()
           .Throw<EntityValidationException>()
           .WithMessage($"{fieldName} should be less or equal { maxLenght } characters");
    }

    public static IEnumerable<object[]> GetValuesGreatherThanMaxLenght(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 5 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var target = faker.Commerce.ProductName();
            var maxLenght = target.Length - (new Random()).Next(1, target.Length);
            yield return new object[] { target, maxLenght };
        }
    }

    [Theory(DisplayName = nameof(MaxLenghtOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesLessThanMaxLenght), parameters: 9)]
    public void MaxLenghtOk(string target, int maxLenght)
    {
        var fieldName = GetFieldName();
        Action action = () => DomainValidation.MaxLenght(target, maxLenght, fieldName);
        action.Should()
           .NotThrow();
    }

    public static IEnumerable<object[]> GetValuesLessThanMaxLenght(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 10 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var target = faker.Commerce.ProductName();
            var maxLenght = target.Length + (new Random()).Next(1, 5);
            yield return new object[] { target, maxLenght };
        }
    }
}
