using FC.Codeflix.Catalog.Application.Exceptions;
using FC.Codeflix.Catalog.Infra.Data.EF.Repositories;
using FluentAssertions;
using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory;

namespace FC.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.GetCategory;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryTest
{
    private readonly GetCategoryTestFixture _fixture;
    public GetCategoryTest(GetCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(GetCategory))]
    [Trait("Integration/Application", "Get Category")]
    public async Task GetCategory()
    {
        var exampleCategory = _fixture.GetExampleCategory();
        var dbContext = _fixture.CreateDbContext();
        dbContext.Categories.Add(exampleCategory);
        dbContext.SaveChanges();
        var repository = new CategoryRepository(dbContext);
        
        var input = new UseCase.GetCategoryInput(exampleCategory.Id);
        var useCase = new UseCase.GetCategory(repository);
        
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.Id.Should().Be(exampleCategory.Id);
        output.Name.Should().Be(exampleCategory.Name);
        output.Description.Should().Be(exampleCategory.Description);
        output.IsActive.Should().Be(exampleCategory.IsActive);
        output.CreatedAt.Should().Be(exampleCategory.CreatedAt);
    }

    [Fact(DisplayName = nameof(NotFoundExceptionWhenCategoryDoesntExist))]
    [Trait("Integration/Application", "Get Category")]
    public async Task NotFoundExceptionWhenCategoryDoesntExist()
    {
        var exampleCategory = _fixture.GetExampleCategory();
        var dbContext = _fixture.CreateDbContext();
        dbContext.Categories.Add(exampleCategory);
        dbContext.SaveChanges();
        var repository = new CategoryRepository(dbContext);

        var useCase = new UseCase.GetCategory(repository);
        var input = new UseCase.GetCategoryInput(Guid.NewGuid());

        var task = async() 
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Category '{input.Id}' not found.");
    }
}
