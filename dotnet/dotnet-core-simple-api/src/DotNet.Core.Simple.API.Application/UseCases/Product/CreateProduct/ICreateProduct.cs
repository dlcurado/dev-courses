using MediatR;

namespace DotNet.Core.Simple.API.Application.UseCases.Product.CreateProduct;
public interface ICreateProduct :
    IRequestHandler<CreateProductInput, CreateProductOutput>
{
    public Task<CreateProductOutput> Handle(CreateProductInput request, 
        CancellationToken cancellationToken);
}
