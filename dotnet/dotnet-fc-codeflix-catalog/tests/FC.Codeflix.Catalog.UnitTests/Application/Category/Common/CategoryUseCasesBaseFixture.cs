using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using Entity = FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Common;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.Common;
public abstract class CategoryUseCasesBaseFixture
    : BaseFixture
{
    public Mock<ICategoryRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

    public string GetValidCategoryName()
    {
        var categoryName = "";
        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];
        if (categoryName.Length > 255)
            categoryName = categoryName[..255];
        return categoryName;
    }

    public string GetValidCategoryDescription()
    {
        var categoryDescription = "";
        while (categoryDescription.Length < 3)
            categoryDescription = Faker.Commerce.ProductDescription();
        if (categoryDescription.Length > 10000)
            categoryDescription = categoryDescription[..10000];
        return categoryDescription;
    }

    public CreateCategoryInput GetInput()
     => new(
         GetValidCategoryName(),
         GetValidCategoryDescription(),
         getRandomBoolean()
         );


    public Entity.Category GetExampleCategory()
        => new(
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            getRandomBoolean()
            );

    public bool getRandomBoolean()
        => new Random().NextDouble() < 0.5;
}
