using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory;
public interface IGetCategory : 
    IRequestHandler<GetCategoryInput, CategoryModelOutput>
{
    public Task<CategoryModelOutput> Handle(GetCategoryInput input, 
        CancellationToken cancellationToken);
}
