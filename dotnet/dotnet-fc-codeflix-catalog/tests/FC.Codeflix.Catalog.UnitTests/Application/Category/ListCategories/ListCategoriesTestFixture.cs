﻿using FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using Entity = FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.Codeflix.Catalog.UnitTests.Application.Category.Common;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.ListCategories;


[CollectionDefinition(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture>
{
}

public class ListCategoriesTestFixture : CategoryUseCasesBaseFixture
{
    public List<Entity.Category> GetExampleCategoriesList(int length)
    {
        var list = new List<Entity.Category>();
        for (int i = 0; i < length; i++)
        {
            list.Add(GetExampleCategory());
        }
        return list;
    }

    public List<Entity.Category> GetExampleCategoriesList()
    {
        return GetExampleCategoriesList(1);
    }

    public ListCategoriesInput GetExampleInput()
    {
        var random = new Random();
        return new ListCategoriesInput(
            page: random.Next(0, 10),
            perPage: random.Next(15, 100),
            search: Faker.Commerce.ProductName(),
            sort: Faker.Commerce.ProductName(),
            dir: random.Next(0, 10) > 5 ?
                SearchOrder.Asc : SearchOrder.Desc
        );
    }
}
