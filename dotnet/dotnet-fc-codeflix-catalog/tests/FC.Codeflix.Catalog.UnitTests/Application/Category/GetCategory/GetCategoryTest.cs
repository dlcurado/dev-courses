using FluentAssertions;
using Moq;
using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory;
using FC.Codeflix.Catalog.Application.Exceptions;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.GetCategory;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryTest
{
    private GetCategoryTestFixture _fixture;

    public GetCategoryTest(GetCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(GetCategory))]
    [Trait("Application", "Get Category")]
    public async Task GetCategory()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var categoryMock = _fixture.GetExampleCategory();
        repositoryMock.Setup(repo => repo.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(categoryMock);

        var input = new UseCase.GetCategoryInput(categoryMock.Id);
        var useCase = new UseCase.GetCategory(repositoryMock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(repo => repo.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
            ), Times.Once);

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.Id.Should().Be(categoryMock.Id);
        output.Name.Should().Be(categoryMock.Name);
        output.Description.Should().Be(categoryMock.Description);
        output.IsActive.Should().Be(categoryMock.IsActive);
        output.CreatedAt.Should().Be(categoryMock.CreatedAt);
    }

    [Fact(DisplayName = nameof(NotFoundExceptionWhenCategoryDoesntExist))]
    [Trait("Application", "Get Category")]
    public async Task NotFoundExceptionWhenCategoryDoesntExist()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var exampleGuid = Guid.NewGuid();
        repositoryMock.Setup(repo => repo.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
            )
        ).ThrowsAsync(
            new NotFoundException($"cateogry '{exampleGuid}' not found")
        );

        var input = new UseCase.GetCategoryInput(exampleGuid);
        var useCase = new UseCase.GetCategory(repositoryMock.Object);

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>();

        repositoryMock.Verify(repo => repo.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
            ), Times.Once);
    }
}
