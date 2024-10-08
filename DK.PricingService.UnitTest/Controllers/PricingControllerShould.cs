using DK.PricingService.API.Commands.Results;
using DK.PricingService.API.Commands;
using DK.PricingService.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DK.PricingService.UnitTests.Controllers;

public class PricingControllerShould
{
    [Fact]
    public async Task CallMediatorWithCorrectParameters()
    {
        var mediator = new Mock<IMediator>();
        var command = new CalculatePriceCommand();
        var controller = new PricingController(mediator.Object);

        await controller.Post(command);

        mediator.Verify(x => x.Send(command, default), Times.Once());
    }

    [Fact]
    public async Task ReturnJsonResult()
    {
        var mediator = new Mock<IMediator>();
        var command = new CalculatePriceCommand();
        var calculatePriceResult = new CalculatePriceResult { TotalPrice = 12 };
        mediator.Setup(x => x.Send(command, default)).ReturnsAsync(calculatePriceResult);
        var controller = new PricingController(mediator.Object);

        var result = await controller.Post(command);

        Assert.IsAssignableFrom<JsonResult>(result);
        Assert.Equal(calculatePriceResult, ((JsonResult)result).Value);
    }
}
