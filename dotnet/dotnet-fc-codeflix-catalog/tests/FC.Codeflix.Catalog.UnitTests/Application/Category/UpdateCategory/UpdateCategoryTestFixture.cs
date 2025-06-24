using FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FC.Codeflix.Catalog.UnitTests.Application.Category.Common;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

[CollectionDefinition(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTestFixtureCollection
    : ICollectionFixture<UpdateCategoryTestFixture>
{ }


public class UpdateCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public UpdateCategoryInput GetValidInput(Guid? id = null)
    {
        return new UpdateCategoryInput(
            id ?? Guid.NewGuid(),
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            getRandomBoolean()
        );
    }

    public UpdateCategoryInput GetInvalidInputShortName()
    {
        var invalidInputShortName = GetValidInput();
        invalidInputShortName.Name = Faker.Commerce.Categories(1)[0][..2];
        return invalidInputShortName;
    }

    public UpdateCategoryInput GetInvalidInputTooLongName()
    {
        var invalidInputTooLongName = GetValidInput();
        invalidInputTooLongName.Name = Faker.Commerce.Categories(1)[0];
        while (invalidInputTooLongName.Name.Length <= 255)
            invalidInputTooLongName.Name = $"{invalidInputTooLongName.Name} {Faker.Commerce.Categories(1)[0]}";
        return invalidInputTooLongName;
    }

    public UpdateCategoryInput GetInvalidInputToLongDescription()
    {
        var invalidInputTooLongDescription = GetValidInput();
        invalidInputTooLongDescription.Description = "";
        while (invalidInputTooLongDescription.Description.Length < 10001)
            invalidInputTooLongDescription.Description += Faker.Commerce.ProductDescription();
        return invalidInputTooLongDescription;
    }
}
