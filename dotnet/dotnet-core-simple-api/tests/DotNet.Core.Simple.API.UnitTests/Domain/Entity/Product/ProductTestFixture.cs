using DotNet.Core.Simple.API.UnitTests.Commom;
using DomainEntity = DotNet.Core.Simple.API.Domain.Entity;

namespace DotNet.Core.Simple.API.UnitTests.Domain.Entity.Product
{
    public class ProductTestFixture
        : BaseFixture
    {
        public ProductTestFixture() : base() { }

        public string GetValidProductName()
        {
            return Faker.Commerce.ProductName();
        }

        public string GetValidProductDescription()
        {
            return Faker.Commerce.ProductDescription();
        }

        public decimal GetValidProductSalePrice()
        {
            return Faker.Finance.Amount(20.00m, 480.00m, 2);
        }

        public DomainEntity.Product GetValidProduct()
            => new (
                GetValidProductName(),
                GetValidProductSalePrice()
            );
    }

    [CollectionDefinition(nameof(ProductTestFixture))]
    public class ProductTestFixtureCollection : ICollectionFixture<ProductTestFixture>
    {
    }
}