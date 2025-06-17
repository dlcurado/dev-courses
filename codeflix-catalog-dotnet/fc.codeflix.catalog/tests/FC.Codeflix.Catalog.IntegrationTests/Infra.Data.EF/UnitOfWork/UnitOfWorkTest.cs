using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UnitOfWorkInfra = FC.Codeflix.Catalog.Infra.Data.EF;
namespace FC.Codeflix.Catalog.IntegrationTests.Infra.Data.EF.UnitOfWork;

[Collection(nameof(UnitOfWorkTestFixture))]
public class UnitOfWorkTest
{
    private readonly UnitOfWorkTestFixture _fixture;

    public UnitOfWorkTest(UnitOfWorkTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Commit")]
    [Trait("Integration/Infra.Data", "UnitOfWork")]
    public async Task Commit()
    {
        // Arrange
        var dbContext = _fixture.CreateDbContext();
        var exampleCategoryList = _fixture.GetExampleCategoriesList();
        await dbContext.Categories.AddRangeAsync(exampleCategoryList);
        var unitOfWork = new UnitOfWorkInfra.UnitOfWork(dbContext);
        

        // Act
        await unitOfWork.Commit(CancellationToken.None);

        // Assert
        //var savedCategory = dbContext.Categories.Find(exampleCategoryList[0].Id);
        var assertDbContext = _fixture.CreateDbContext(true);
        var savedCategory = assertDbContext.Categories
            .AsNoTracking()
            .ToList();

        savedCategory.Should()
            .HaveCount(exampleCategoryList.Count);
    }

    [Fact(DisplayName = "Rollback")]
    [Trait("Integration/Infra.Data", "UnitOfWork")]
    public async Task Rollback()
    {
        // Arrange
        var dbContext = _fixture.CreateDbContext();
        var unitOfWork = new UnitOfWorkInfra.UnitOfWork(dbContext);


        // Act
        var task = async() => await unitOfWork.Rollback(CancellationToken.None);

        // Assert
        await task.Should().NotThrowAsync();
    }
}