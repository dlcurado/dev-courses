using DotNet.Core.Simple.API.Domain.Exceptions;
using DomainEntity = DotNet.Core.Simple.API.Domain.Entity;

namespace DotNet.Core.Simple.API.UnitTests.Domain.Entity.Product
{
    [Collection(nameof(ProductTestFixture))]
    public class ProductTest
    {
        private readonly ProductTestFixture _fixture;
        public ProductTest(ProductTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(InstantiateProductWithNameAndPrice))]
        [Trait("Domain", "Entity - Product")]
        public void InstantiateProductWithNameAndPrice()
        {
            // Arrange
            var validProduct = _fixture.GetValidProduct();
            // Act
            var product = new DomainEntity.Product(validProduct.Name, validProduct.SalePrice);
            // Assert
            Assert.NotNull(product);
            Assert.Equal(validProduct.Name, product.Name);
            Assert.Equal(validProduct.SalePrice, product.SalePrice);
        }

        [Fact(DisplayName = nameof(UpdateProductName))]
        [Trait("Domain", "Entity - Product")]
        public void UpdateProductName()
        {
            // Arrange
            var validProduct = _fixture.GetValidProduct();
            var newName = _fixture.GetValidProductName();

            // Act
            var product = new DomainEntity.Product(validProduct.Name, validProduct.SalePrice);
            product.Update(newName,null);

            // Assert
            Assert.NotNull(product);
            Assert.NotEqual(validProduct.Name, product.Name);
            Assert.Equal(newName, product.Name);
            Assert.Equal(validProduct.SalePrice, product.SalePrice);
        }

        [Fact(DisplayName = nameof(UpdateProductName))]
        [Trait("Domain", "Entity - Product")]
        public void UpdateProductPrice()
        {
            // Arrange
            var validProduct = _fixture.GetValidProduct();
            var newSalePrice = _fixture.GetValidProductSalePrice();

            // Act
            var product = new DomainEntity.Product(validProduct.Name, validProduct.SalePrice);
            product.Update(null, newSalePrice);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(validProduct.Name, product.Name);
            Assert.NotEqual(validProduct.SalePrice, product.SalePrice);
            Assert.Equal(newSalePrice, product.SalePrice);
        }

        [Fact(DisplayName = nameof(UpdateProductNameAndPrice))]
        [Trait("Domain", "Entity - Product")]
        public void UpdateProductNameAndPrice()
        {
            // Arrange
            var validProduct = _fixture.GetValidProduct();
            var newValidProduct = _fixture.GetValidProduct();
            // Act
            var product = new DomainEntity.Product(validProduct.Name, validProduct.SalePrice);
            product.Update(newValidProduct.Name, newValidProduct.SalePrice);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(newValidProduct.Name, product.Name);
            Assert.Equal(newValidProduct.SalePrice, product.SalePrice);
        }

        


        [Theory(DisplayName = nameof(InstantiateProductWithoutNameThrowException))]
        [Trait("Domain", "Entity - Product")]
        [InlineData("", "Name should not be empty.")]
        [InlineData(null, "Name should not be null.")]
        [InlineData("   ", "Name should not be empty.")]
        public void InstantiateProductWithoutNameThrowException(string? name, string message)
        {
            // Arrange
            var salePrice = _fixture.GetValidProductSalePrice();
            // Act
            Action action = () => new DomainEntity.Product(name!, salePrice);
            // Assert
            action.Should().Throw<EntityValidationException>()
                .WithMessage(message);
        }

        [Theory(DisplayName = nameof(InstantiateProductWithoutGreatherThanZeroPriceThrowException))]
        [Trait("Domain", "Entity - Product")]
        [InlineData(0.0, "SalePrice should greather than zero.")]
        [InlineData(-1.10, "SalePrice should greather than zero.")]
        public void InstantiateProductWithoutGreatherThanZeroPriceThrowException(decimal salePrice, string message)
        {
            // Arrange
            var name = _fixture.GetValidProductName();
            // Act
            Action action = () => new DomainEntity.Product(name, salePrice);
            // Assert
            action.Should().Throw<EntityValidationException>()
                .WithMessage(message);
        }

    }
}