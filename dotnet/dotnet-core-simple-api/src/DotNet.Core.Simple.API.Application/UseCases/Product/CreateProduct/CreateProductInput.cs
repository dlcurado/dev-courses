using MediatR;

namespace DotNet.Core.Simple.API.Application.UseCases.Product.CreateProduct;

public class CreateProductInput : IRequest<CreateProductOutput>
{
    public CreateProductInput(string name, decimal salePrice)
    {
        Name = name;
        SalePrice = salePrice;
    }

    public string Name { get; set; }
    public decimal SalePrice { get; set; }
}