using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {
        private ICreditCardService _creditCardService;

        public CreditCardsController(ICreditCardService creditCardService) 
        {
            _creditCardService = creditCardService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _creditCardService.GetAll();
            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Add(CreditCard creditCard)
        {
            var result = _creditCardService.Add(creditCard);
            if (result.SuccessStatus)
                return Ok(result);

            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(CreditCard creditCard)
        {
            var result = _creditCardService.Delete(creditCard);
            if (result.SuccessStatus)
                return Ok(result);

            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(CreditCard creditCard)
        {
            var result = _creditCardService.Update(creditCard);
            if (result.SuccessStatus)
                return Ok(result);

            return BadRequest(result.Message);
        }

        [HttpPost("buy")]
        public IActionResult Buy(BuyDto buyDto)
        {
            var result = _creditCardService.Buy(buyDto);
            if (result.SuccessStatus)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("refund")]
        public IActionResult Refund(BuyDto buyDto)
        {
            var result = _creditCardService.Refund(buyDto);
            if (result.SuccessStatus)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
