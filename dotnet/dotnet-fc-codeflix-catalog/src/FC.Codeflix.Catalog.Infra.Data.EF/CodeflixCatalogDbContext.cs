﻿using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Infra.Data.EF.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FC.Codeflix.Catalog.Infra.Data.EF;
public class CodeflixCatalogDbContext
    : DbContext
{
    public DbSet<Category> Categories => Set<Category>();

    public CodeflixCatalogDbContext(
        DbContextOptions<CodeflixCatalogDbContext> options
        ) : base( options ) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
    }
}
