using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.Domain.Exceptions;
using FC.Codeflix.Catalog.Infra.Data.EF;
using FC.Codeflix.Catalog.Infra.Data.EF.Repositories;
using FluentAssertions;
using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

namespace FC.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _fixture;
    public CreateCategoryTest(CreateCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Integration/Application", "Create Category Use Case")]
    public async void CreateCategory()
    {
        var dbContext = _fixture.CreateDbContext();
        var repository = new CategoryRepository(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new UseCase.CreateCategory(
            repository, unitOfWork
        );
        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);
        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        output.CreatedAt.Should().NotBeSameDateAs(default);

        var categoryFromDb = await (_fixture.CreateDbContext(true))
            .Categories.FindAsync(output.Id);
        categoryFromDb.Should().NotBeNull();
        categoryFromDb!.Name.Should().Be(input.Name);
        categoryFromDb.Description.Should().Be(input.Description);
        categoryFromDb.IsActive.Should().Be(input.IsActive);
        categoryFromDb.CreatedAt.Should().Be(output.CreatedAt);
    }

    [Fact(DisplayName = nameof(CreateCategoryOnlyWithName))]
    [Trait("Integration/Application", "Create Category Use Case Only With Name")]
    public async void CreateCategoryOnlyWithName()
    {
        var dbContext = _fixture.CreateDbContext();
        var repository = new CategoryRepository(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new UseCase.CreateCategory(
            repository, unitOfWork
        );
        var input = new UseCase.CreateCategoryInput(
            _fixture.GetInput().Name);

        var output = await useCase.Handle(input, CancellationToken.None);
        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.Name.Should().Be(input.Name);
        output.Description.Should().BeEmpty();
        output.IsActive.Should().BeTrue();
        output.CreatedAt.Should().NotBeSameDateAs(default);

        var categoryFromDb = await (_fixture.CreateDbContext(true))
            .Categories.FindAsync(output.Id);
        categoryFromDb.Should().NotBeNull();
        categoryFromDb!.Name.Should().Be(input.Name);
        categoryFromDb.Description.Should().Be(input.Description);
        categoryFromDb.IsActive.Should().Be(input.IsActive);
        categoryFromDb.CreatedAt.Should().Be(output.CreatedAt);
    }

    [Fact(DisplayName = nameof(CreateCategoryOnlyWithNameAndDescription))]
    [Trait("Integration/Application", "Create Category Use Case Only With Name and Description")]
    public async void CreateCategoryOnlyWithNameAndDescription()
    {
        var dbContext = _fixture.CreateDbContext();
        var repository = new CategoryRepository(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new UseCase.CreateCategory(
            repository, unitOfWork
        );
        var exampleInput = _fixture.GetInput();
        var input = new UseCase.CreateCategoryInput(
            exampleInput.Name,
            exampleInput.Description);

        var output = await useCase.Handle(input, CancellationToken.None);
        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().BeTrue();
        output.CreatedAt.Should().NotBeSameDateAs(default);

        var categoryFromDb = await (_fixture.CreateDbContext(true))
            .Categories.FindAsync(output.Id);
        categoryFromDb.Should().NotBeNull();
        categoryFromDb!.Name.Should().Be(input.Name);
        categoryFromDb.Description.Should().Be(input.Description);
        categoryFromDb.IsActive.Should().Be(input.IsActive);
        categoryFromDb.CreatedAt.Should().Be(output.CreatedAt);
    }


    [Theory(DisplayName = nameof(ThrowWhenCantInstantiateCategory))]
    [Trait("Integration/Application", "Throw Exception When Cant Instantiate")]
    [MemberData(
        nameof(CreateCategoryTestDataGenerator.GetInvalidInputs),
        parameters: 6,
        MemberType = typeof(CreateCategoryTestDataGenerator)
    )]
    public async void ThrowWhenCantInstantiateCategory(
        CreateCategoryInput input,
        string expectedExceptionMessage
        )
    {
        var dbContext = _fixture.CreateDbContext();
        var repository = new CategoryRepository(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new UseCase.CreateCategory(
            repository, unitOfWork
        );
        
        var task = async() 
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<EntityValidationException>()
            .WithMessage(expectedExceptionMessage);
    }
}