using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Application.Category.Common;
using FC.Codeflix.Catalog.UnitTests.Common;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.CreateCategory;

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection
    : ICollectionFixture<CreateCategoryTestFixture>
{

}

public class CreateCategoryTestFixture : CategoryUseCasesBaseFixture
{
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

