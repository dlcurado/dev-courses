using DomainEntity = DotNet.Core.Simple.API.Domain.Entity;
namespace DotNet.Core.Simple.API.Application.UseCases.Product.CreateProduct;

public class CreateProductOutput
{
    public CreateProductOutput(Guid id, string name, decimal salePrice)
    {
        Id = id;
        Name = name;
        SalePrice = salePrice;
    }

    public Guid Id { get; }
    public string Name { get; }
    public decimal SalePrice { get; }

    public static CreateProductOutput FromProduct(DomainEntity.Product product)
    {
        return new (
            product.Id,
            product.Name,
            product.SalePrice
            );
    }
}