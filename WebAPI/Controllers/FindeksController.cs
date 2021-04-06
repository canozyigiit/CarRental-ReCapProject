using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FindeksController : ControllerBase
    {
        private IFindeksService _findeksService;

        public FindeksController(IFindeksService findeksService)
        {
            _findeksService = findeksService;
        }
        
        [HttpGet("check")]
        public IActionResult Check(int carId,int customerId)
        {
            var result = _findeksService.Check(carId,customerId);
            if (result.SuccessStatus)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
