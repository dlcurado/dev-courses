using FC.Codeflix.Catalog.Application.Interfaces;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.Application.UseCases.Category.Common;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
public class CreateCategory : ICreateCategory
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryModelOutput> Handle(CreateCategoryInput input, CancellationToken cancellationToken)
    {
        // Criando a entity a partir do DTO
        var category = new DomainEntity.Category(
            input.Name,
            input.Description,
            input.IsActive
            );

        // Inserindo a entidade no repositório
        await _categoryRepository.Insert(category, cancellationToken);

        // Persistindo a operação
        await _unitOfWork.Commit(cancellationToken);

        // Retornando um DTO
        return CategoryModelOutput.FromCategory(category);
    }
}
