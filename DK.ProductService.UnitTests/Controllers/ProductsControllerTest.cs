using DK.ProductService.API.Queries.Dtos;
using DK.ProductService.UnitTests.TestData;
using DK.ProductService.UnitTests.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static Xunit.Assert;

namespace DK.ProductService.UnitTests.Controllers;

public class ProductsControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;

    public ProductsControllerTest(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task GetAll_ReturnsJsonResult_WithListOfProducts()
    {
        var client = factory.CreateClient();

        var response = await client.DoGetAsync<List<ProductDto>>("/api/Products");

        True(response.Count > 1);
    }


    [Fact]
    public async Task GetByCode_ReturnsJsonResult_WithOneProductOfCorrectType()
    {
        var productTravel = TestProductDtoFactory.Travel();

        var client = factory.CreateClient();

        var response = await client.DoGetAsync<ProductDto>("/api/Products/" + productTravel.Code);

        Equal(productTravel.Code, response.Code);
        Equal(productTravel.Name, response.Name);
        Equal(productTravel.Description, response.Description);
    }
}
