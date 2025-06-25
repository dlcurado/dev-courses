using DotNet.Core.Simple.API.Domain.Repository;
using DotNet.Core.Simple.API.Application.Interfaces;
using UseCases = DotNet.Core.Simple.API.Application.UseCases.Product.CreateProduct;
using Entity = DotNet.Core.Simple.API.Domain.Entity;
using Moq;

namespace DotNet.Core.Simple.API.UnitTests.Application.Product.CreateProduct;
public class CreateProductTest
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CreateProductTest()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact(DisplayName = nameof(CreateProduct))]
    [Trait("Application", "Unit")]
    public async void CreateProduct()
    {
        // Arrange
        var useCase = new UseCases.CreateProduct(
            _productRepositoryMock.Object,
            _unitOfWorkMock.Object
            );

        var input = new UseCases.CreateProductInput(
            "Test Product",
            100.00m
        );

        // Act
        var output = await useCase.Handle(input, CancellationToken.None);

        // Assert
        // Verify if Insert method in UseCase ProdutRepository was called only once
        _productRepositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<Entity.Product>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
            );
    }
}
