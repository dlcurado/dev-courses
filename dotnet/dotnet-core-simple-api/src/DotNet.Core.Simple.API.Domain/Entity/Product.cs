using DotNet.Core.Simple.API.Domain.SeedWork;
using DotNet.Core.Simple.API.Domain.Validation;

namespace DotNet.Core.Simple.API.Domain.Entity;
public class Product : AggregateRoot
{
    public Product(string name, decimal salePrice)
    {
        Name = name;
        SalePrice = salePrice;

        Validate();
    }

    public string Name { get; private set; }
    public decimal SalePrice { get; private set; }

    public void Update(string? name, decimal? salePrice)
    {
        Name = name ?? Name;
        SalePrice = salePrice ?? SalePrice;
        Validate();
    }

    private void Validate()
    {
        DomainValidation.ValidateNotNull(Name, nameof(Name));

        DomainValidation.ValidateNotEmpty(Name, nameof(Name));

        DomainValidation.ValidateGreaterThanZero(SalePrice, nameof(SalePrice));
    }
}