using Entity = FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.Codeflix.Catalog.IntegrationTests.Base;

namespace FC.Codeflix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository;

[CollectionDefinition(nameof(CategoryRepositoryTestFixture))]
public class CategoryRepositoryTestFixtureCollection :
    ICollectionFixture<CategoryRepositoryTestFixture>
{

}

public class CategoryRepositoryTestFixture : BaseFixture
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
    
    public Category GetExampleCategory()
        => new(
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            getRandomBoolean()
            );

    public List<Category> GetExampleCategoryList(int length = 10)
    {
        return Enumerable.Range(1, length)
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

    public List<Category> CloneCategoriesListOrdered(
        List<Category> categoriesList,
        string orderBy,
        SearchOrder order)
    {
        var listClone = new List<Category>(categoriesList);
        var orderedList = (orderBy.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => listClone.OrderBy(cat => cat.Name),
            ("name", SearchOrder.Desc) => listClone.OrderByDescending(cat => cat.Name),
            ("id", SearchOrder.Asc) => listClone.OrderBy(cat => cat.Id),
            ("id", SearchOrder.Desc) => listClone.OrderByDescending(cat => cat.Id),
            ("createdat", SearchOrder.Asc) => listClone.OrderBy(cat => cat.CreatedAt),
            ("createdat", SearchOrder.Desc) => listClone.OrderByDescending(cat => cat.CreatedAt),
            _ => listClone.OrderBy(item => item.Name),
        };
        return orderedList.ToList();
    }

    
}
