﻿using DK.ProductService.API.Queries;
using DK.ProductService.Domain.Contracts;
using DK.ProductService.Domain;
using DK.ProductService.Queries;
using DK.ProductService.UnitTests.TestData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DK.ProductService.UnitTests.Handlers;

public class FindProductsHandlersTest
{
    private readonly Mock<IProductRepository> productRepository;

    private readonly List<Product> products = new()
    {
        TestProductFactory.Travel(),
        TestProductFactory.House()
    };

    public FindProductsHandlersTest()
    {
        productRepository = new Mock<IProductRepository>();


        productRepository.Setup(x => x.FindAllActive()).Returns(Task.FromResult(products));
        productRepository.Setup(x => x.FindOne(It.Is<string>(s => products.Select(p => p.Code).Contains(s))))
            .Returns(Task.FromResult(products.First()));
        productRepository.Setup(x => x.FindOne(It.Is<string>(s => !products.Select(p => p.Code).Contains(s))))
            .Returns(Task.FromResult<Product>(null));
    }

    [Fact]
    public async Task FindAllProductsHandler_ReturnsListOfAllProducts()
    {
        var findAllProductsHandler = new FindAllProductsHandler(productRepository.Object);

        var result = await findAllProductsHandler.Handle(new FindAllProductsQuery(), new CancellationToken());

        Assert.NotNull(result);
        Assert.Equal(products.Count, result.Count());
    }

    [Fact]
    public async Task FindProductByCodeHandler_ReturnsOneProduct()
    {
        var findProductByCodeHandler = new FindProductByCodeHandler(productRepository.Object);

        var result = await findProductByCodeHandler.Handle(
            new FindProductByCodeQuery { ProductCode = TestProductFactory.Travel().Code }, new CancellationToken());

        Assert.NotNull(result);
    }

    [Fact]
    public async Task FindProductByCodeHandler_ReturnsNullIfCodeNotExists()
    {
        var findProductByCodeHandler = new FindProductByCodeHandler(productRepository.Object);

        var result = await findProductByCodeHandler.Handle(new FindProductByCodeQuery { ProductCode = "ASDASD" },
            new CancellationToken());

        Assert.Null(result);
    }
}
