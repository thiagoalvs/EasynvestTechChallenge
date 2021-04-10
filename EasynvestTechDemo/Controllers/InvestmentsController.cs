using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasynvestTechDemo.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasynvestTechDemo.Controllers
{
    [Route("api/investments")]
    [ApiController]
    public class InvestmentsController : ControllerBase
    {

        IInvestmentsService _investmentsService;

        public InvestmentsController(IInvestmentsService investmentsService)
        {
            _investmentsService = investmentsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _investmentsService.Get());
        }

    }
}