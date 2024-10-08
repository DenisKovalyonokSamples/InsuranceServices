﻿using DK.ProductService.Domain.Contracts;
using DK.ProductService.Domain.Types;
using DK.ProductService.Domain;
using Microsoft.EntityFrameworkCore;

namespace DK.ProductService.DataAccess.EF;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext productDbContext;

    public ProductRepository(ProductDbContext productDbContext)
    {
        this.productDbContext = productDbContext ?? throw new ArgumentNullException(nameof(productDbContext));
    }

    public async Task<Product> Add(Product product)
    {
        await productDbContext.Products.AddAsync(product);
        return product;
    }

    public async Task<List<Product>> FindAllActive()
    {
        return await productDbContext
            .Products
            .Include(c => c.Covers)
            .Include("Questions.Choices")
            .Where(p => p.Status == ProductStatus.Active)
            .ToListAsync();
    }

    public async Task<Product> FindOne(string productCode)
    {
        return await productDbContext
            .Products
            .Include(c => c.Covers)
            .Include("Questions.Choices")
            .FirstOrDefaultAsync(p => p.Code.Equals(productCode, StringComparison.InvariantCultureIgnoreCase));
    }

    public async Task<Product> FindById(Guid id)
    {
        return await productDbContext.Products.Include(c => c.Covers).Include("Questions.Choices")
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
