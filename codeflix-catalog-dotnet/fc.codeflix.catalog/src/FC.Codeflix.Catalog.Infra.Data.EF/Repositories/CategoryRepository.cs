using FC.Codeflix.Catalog.Application.Exceptions;
using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace FC.Codeflix.Catalog.Infra.Data.EF.Repositories;
public class CategoryRepository
    : ICategoryRepository
{
    private readonly CodeflixCatalogDbContext _context;
    private DbSet<Category> _categories => _context.Set<Category>();

    public CategoryRepository(CodeflixCatalogDbContext context)
    {
        _context = context;
    }

    public Task Delete(Category aggregate, CancellationToken cancellationToken)
    {
        return Task.FromResult(_categories.Remove(aggregate));
    }

    public async Task<Category?> Get(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categories.AsNoTracking().FirstOrDefaultAsync(
            cat => cat.Id == id ,
            cancellationToken
        );
        NotFoundException.ThrowIfNull(category, $"Category '{id}' not found.");
        return category;
    }

    public async Task Insert(
        Category aggregate,
        CancellationToken cancellationToken)
    {
        await _context.AddAsync(aggregate, cancellationToken);
    }

    public async Task<SearchOutput<Category>> Search(
        SearchInput input, 
        CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = _categories.AsNoTracking();
        query = AddOrderToQuery(query, input.OrderBy, input.Order);
        if (!String.IsNullOrWhiteSpace(input.Search))
            query = query.Where(cat => cat.Name.Contains(input.Search));

        var total = await query.CountAsync();
        var items = await query.Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync();
        return new(input.Page, input.PerPage, total, items);
    }

    private IQueryable<Category> AddOrderToQuery(
        IQueryable<Category> query, 
        string orderBy, 
        SearchOrder order)
    {
        return (orderBy.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(cat => cat.Name),
            ("name", SearchOrder.Desc) => query.OrderByDescending(cat => cat.Name),
            ("id", SearchOrder.Asc) => query.OrderBy(cat => cat.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(cat => cat.Id),
            ("createdat", SearchOrder.Asc) => query.OrderBy(cat => cat.CreatedAt),
            ("createdat", SearchOrder.Desc) => query.OrderByDescending(cat => cat.CreatedAt),
            _ => query.OrderBy(item => item.Name),
        };
    }

    public Task Update(
        Category aggregate,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(_categories.Update(aggregate));
    }
}
