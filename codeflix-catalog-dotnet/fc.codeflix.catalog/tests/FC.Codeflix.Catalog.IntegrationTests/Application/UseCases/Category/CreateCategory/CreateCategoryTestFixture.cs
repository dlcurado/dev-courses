using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.Common;

namespace FC.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.CreateCategory;

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection
    : ICollectionFixture<CreateCategoryTestFixture>
{ }

public class CreateCategoryTestFixture
    : CategoryUseCasesBaseFixture
{
    public CreateCategoryInput GetInput()
    {
        var categoryExample = GetExampleCategory();
        return new CreateCategoryInput(
            categoryExample.Name,
            categoryExample.Description,
            categoryExample.IsActive
        );
    }

    public CreateCategoryInput GetInvalidInputShortName()
    {
        var invalidInputShortName = GetInput();
        invalidInputShortName.Name = Faker.Commerce.Categories(1)[0][..2];
        return invalidInputShortName;
    }

    public CreateCategoryInput GetInvalidInputTooLongName()
    {
        var invalidInputTooLongName = GetInput();
        invalidInputTooLongName.Name = Faker.Commerce.Categories(1)[0];
        while (invalidInputTooLongName.Name.Length <= 255)
            invalidInputTooLongName.Name = $"{invalidInputTooLongName.Name} {Faker.Commerce.Categories(1)[0]}";
        return invalidInputTooLongName;
    }

    public CreateCategoryInput GetInvalidInputToLongDescription()
    {
        var invalidInputTooLongDescription = GetInput();
        invalidInputTooLongDescription.Description = "";
        while (invalidInputTooLongDescription.Description.Length < 10001)
            invalidInputTooLongDescription.Description += Faker.Commerce.ProductDescription();
        return invalidInputTooLongDescription;
    }

    public CreateCategoryInput GetInvalidInputDescriptionNull()
    {
        var invalidInputDescriptionNull = GetInput();
        invalidInputDescriptionNull.Description = null!;
        return invalidInputDescriptionNull;
    }
}
