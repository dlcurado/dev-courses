using FC.Codeflix.Catalog.Domain.Exceptions;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.UnitTests.Domain.Entity.Category;

[Collection(nameof(CategoryTestFixture))]
public class CategoryTest
{
    private readonly CategoryTestFixture _categoryTestFixture;

    public CategoryTest(CategoryTestFixture categoryTestFixture)
        => _categoryTestFixture = categoryTestFixture;

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Instantiate()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();

        DateTime datetimeBefore = DateTime.Now;
        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description);
        DateTime datetimeAfter = DateTime.Now.AddSeconds(1);

        category.Should().NotBeNull();
        category.Name.Should().Be(validCategory.Name);
        category.Description.Should().Be(validCategory.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        (category.CreatedAt >= datetimeBefore).Should().BeTrue();
        (category.CreatedAt <= datetimeAfter).Should().BeTrue();
        category.IsActive.Should().BeTrue();
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool isActive)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();

        DateTime datetimeBefore = DateTime.Now;
        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, isActive);
        DateTime datetimeAfter = DateTime.Now.AddSeconds(1);

        category.Should().NotBeNull();
        category.Name.Should().Be(validCategory.Name);
        category.Description.Should().Be(validCategory.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        (category.CreatedAt >= datetimeBefore).Should().BeTrue();
        (category.CreatedAt <= datetimeAfter).Should().BeTrue();
        category.IsActive.Should().Be(isActive);
    }

    /* Regras
     * Name com no mínimo 3 caracteres e no máximo 255
     * Description deve ter no máximo 10.000 caracteres
     */
    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("     ")]
    public void InstantiateErrorWhenNameIsEmpty(string? name)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        Action action = () => new DomainEntity.Category(name!, validCategory.Description);
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsNull()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        Action action = () => new DomainEntity.Category(validCategory.Name, null!);
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Description should not be null");
    }


    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("1")]
    [InlineData("12")]
    public void InstantiateErrorWhenNameIsLessThan3Characters(string? name)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        Action action = () => new DomainEntity.Category(name!, validCategory.Description);
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should be at least 3 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreatherThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenNameIsGreatherThan255Characters()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var invalidName = String.Join(
            null, Enumerable.Range(0,256).Select(_ => "a").ToArray()
            );
        Action action = () => new DomainEntity.Category(invalidName, validCategory.Description);
        action.Should()
           .Throw<EntityValidationException>()
           .WithMessage("Name should be less or equal 255 characters");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreatherThan10_000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsGreatherThan10_000Characters()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var invalidDescription = String.Join(
            null, Enumerable.Range(0, 10001).Select(_ => "a").ToArray()
            );
        Action action = () => new DomainEntity.Category(validCategory.Name, invalidDescription);
        action.Should()
           .Throw<EntityValidationException>()
           .WithMessage("Description should be less or equal 10000 characters");
    }

    [Fact(DisplayName = nameof(Activate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Activate()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();

        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, false);
        category.Activate();
        category.IsActive.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(Deactivate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Deactivate()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();

        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);
        category.Deactivate();
        category.IsActive.Should().BeFalse();
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Category - Aggregates")]
    public void Update()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var updatedCategory = _categoryTestFixture.GetValidCategory();

        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);

        category.Update(updatedCategory.Name, updatedCategory.Description);

        category.Name.Should().Be(updatedCategory.Name);
        category.Description.Should().Be(updatedCategory.Description);
    }

    [Fact(DisplayName = nameof(UpdateNameOnly))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateNameOnly()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var updatedCategory = _categoryTestFixture.GetValidCategory();

        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);

        category.Update(updatedCategory.Name);

        category.Name.Should().Be(updatedCategory.Name);
        category.Description.Should().Be(validCategory.Description);
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("     ")]
    public void UpdateErrorWhenNameIsEmpty(string? name)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        
        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);
        Action action = () => category.Update(name!);
        action.Should()
           .Throw<EntityValidationException>()
           .WithMessage("Name should not be empty or null");
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [MemberData(nameof(GetNamesWithLessThan3Characters), parameters: 10)]
    public void UpdateErrorWhenNameIsLessThan3Characters(string? name)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        
        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);
        Action action = () => category.Update(name!);
        action.Should()
           .Throw<EntityValidationException>()
           .WithMessage("Name should be at least 3 characters long");
    }

    public static IEnumerable<object[]> GetNamesWithLessThan3Characters(int? numberOfTests = 6)
    {
        var fixture = new CategoryTestFixture();
        for (int i = 0; i < numberOfTests; i++)
        {
            var isOdd = i % 2 == 1;
            yield return new object[] {
                fixture.GetValidCategoryName()[..(isOdd ? 1 : 2)]
            };
        }
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreatherThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenNameIsGreatherThan255Characters()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var invalidName = _categoryTestFixture.Faker.Lorem.Letter(256);

        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);
        Action action = () => category.Update(invalidName);
        action.Should()
           .Throw<EntityValidationException>()
           .WithMessage("Name should be less or equal 255 characters");
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreatherThan10_000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenDescriptionIsGreatherThan10_000Characters()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var invalidDescription = _categoryTestFixture.Faker.Lorem.Letter(10_001);

        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);
        Action action = () => category.Update(validCategory.Name, invalidDescription);
        action.Should()
           .Throw<EntityValidationException>()
           .WithMessage("Description should be less or equal 10000 characters");
    }
}
