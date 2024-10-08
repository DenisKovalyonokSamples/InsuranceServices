﻿using DK.PricingService.API.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DK.PricingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        private readonly IMediator bus;

        public PricingController(IMediator bus)
        {
            this.bus = bus;
        }


        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CalculatePriceCommand cmd)
        {
            var result = await bus.Send(cmd);
            return new JsonResult(result);
        }
    }
}
