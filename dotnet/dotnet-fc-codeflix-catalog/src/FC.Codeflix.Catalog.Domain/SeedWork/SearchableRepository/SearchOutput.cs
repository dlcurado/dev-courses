﻿using FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
public class SearchOutput<TAggregate>
    where TAggregate : AggregateRoot
{
    public int CurrentPage { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public IReadOnlyList<TAggregate> Items { get; set; }

    public SearchOutput(int currentPage, 
        int perPage, 
        int total,
        IReadOnlyList<TAggregate> items)
    {
        CurrentPage = currentPage;
        this.PerPage = perPage;
        Items = items;
        this.Total = total;
    }
}
