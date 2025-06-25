using DotNet.Core.Simple.API.Application.Interfaces;
using DotNet.Core.Simple.API.Domain.Repository;
using DomainEntity = DotNet.Core.Simple.API.Domain.Entity;

namespace DotNet.Core.Simple.API.Application.UseCases.Product.CreateProduct;
public class CreateProduct : ICreateProduct
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProduct(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateProductOutput> Handle(CreateProductInput input, 
        CancellationToken cancellationToken)
    {
        var product = new DomainEntity.Product(
            input.Name,
            input.SalePrice
            );

        await _productRepository.Insert(product, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return CreateProductOutput.FromProduct(product);
    }
}
