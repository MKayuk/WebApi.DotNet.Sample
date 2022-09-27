using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces.Repositories;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.API.Controllers
{
    [Route("api/PriceList")]
    [ApiController]
    public class PriceListController : ControllerBase
    {
        private readonly ILogger _logger;

        public PriceListController(ILogger logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetPriceList([FromServices] IPriceRepository priceRepository)
        {
            List<PriceList> pricelist;

            try
            {
                pricelist = priceRepository.GetCustomPriceList();
                var price = priceRepository.Get(1);
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message);
            }

            if (pricelist.Any())
                return Ok(pricelist);
            else
                return NotFound(pricelist);
        }

    }
}