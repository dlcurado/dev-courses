﻿using FC.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.Common;

namespace FC.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.GetCategory;

[CollectionDefinition(nameof(GetCategoryTestFixture))]
public class GetCategoryTestFixtureCollection 
    : ICollectionFixture<GetCategoryTestFixture>
{
    
}

public class GetCategoryTestFixture: CategoryUseCasesBaseFixture
{

}
