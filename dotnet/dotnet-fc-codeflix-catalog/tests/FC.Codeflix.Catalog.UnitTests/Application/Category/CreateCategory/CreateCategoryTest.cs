using Moq;
using Entity = FC.Codeflix.Catalog.Domain.Entity;
using UseCases = FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.Application.Interfaces;
using FluentAssertions;
using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.Domain.Exceptions;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _fixture;

    public CreateCategoryTest(CreateCategoryTestFixture fixture) => _fixture = fixture;


    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Application", "Create Category Use Case")]
    public async void CreateCategory()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        var useCase = new UseCases.CreateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            respository => respository.Insert(
                It.IsAny<Entity.Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        unitOfWorkMock.Verify(
            uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        output.CreatedAt.Should().NotBeSameDateAs(default);
    }



    [Theory(DisplayName = nameof(ThrowWhenCantInstantiateAggregate))]
    [Trait("Application", "Create Category Use Case")]
    [MemberData(
        nameof(CreateCategoryTestDataGenerator.GetInvalidInputs),
        parameters: 24,
        MemberType = typeof(CreateCategoryTestDataGenerator)
    )]
    public async void ThrowWhenCantInstantiateAggregate(
        CreateCategoryInput input,
        string exceptionMessage)
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        var useCase = new UseCases.CreateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);
        await task.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(exceptionMessage);
    }

    [Fact(DisplayName = nameof(CreateCategoryWithOnlyName))]
    [Trait("Application", "Create Category Use Case")]
    public async void CreateCategoryWithOnlyName()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        var useCase = new UseCases.CreateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = new CreateCategoryInput(_fixture.GetValidCategoryName());

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            respository => respository.Insert(
                It.IsAny<Entity.Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        unitOfWorkMock.Verify(
            uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be("");
        output.IsActive.Should().BeTrue();
        output.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(CreateCategoryWithOnlyNameAndDescription))]
    [Trait("Application", "Create Category Use Case")]
    public async void CreateCategoryWithOnlyNameAndDescription()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        var useCase = new UseCases.CreateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = new CreateCategoryInput(
            _fixture.GetValidCategoryName(),
            _fixture.GetValidCategoryDescription()
            );

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            respository => respository.Insert(
                It.IsAny<Entity.Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        unitOfWorkMock.Verify(
            uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().BeTrue();
        output.CreatedAt.Should().NotBeSameDateAs(default);
    }
}
