using Bogus;
using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.IntegrationTests.Base;

namespace FC.Codeflix.Catalog.IntegrationTests.Infra.Data.EF.UnitOfWork;

[CollectionDefinition(nameof(UnitOfWorkTestFixture))]
public class UnitOfWorkTestFixtureCollection : 
    ICollectionFixture<UnitOfWorkTestFixture>
{
    

}


public class UnitOfWorkTestFixture : BaseFixture
{
    public string GetValidCategoryName()
    {
        var categoryName = "";
        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];
        if (categoryName.Length > 255)
            categoryName = categoryName[..255];
        return categoryName;
    }

    public Category GetExampleCategory()
        => new(
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            getRandomBoolean()
            );

    public string GetValidCategoryDescription()
    {
        var categoryDescription = "";
        while (categoryDescription.Length < 3)
            categoryDescription = Faker.Commerce.ProductDescription();
        if (categoryDescription.Length > 10000)
            categoryDescription = categoryDescription[..10000];
        return categoryDescription;
    }
    public bool getRandomBoolean()
        => new Random().NextDouble() < 0.5;

    public List<Category> GetExampleCategoriesList(int length = 10)
    {
        return Enumerable.Range(0, length)
            .Select(_ => GetExampleCategory()).ToList();
    }

    public List<Category> GetExampleCategoryListWithNames(List<string> names)
    {
        return names
            .Select(name => {
                var category = GetExampleCategory();
                category.Update(name);
                return category;
            }).ToList();
    }
}