using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Constants;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCreditCardsController : ControllerBase
    {
        private IUserCreditCardService _userCreditCardService;
        private ICustomerService _customerService;

        public UserCreditCardsController(IUserCreditCardService userCreditCardService,ICustomerService customerService)
        {
            _userCreditCardService = userCreditCardService;
            _customerService = customerService;
        }

        [HttpPost("add")]
        public IActionResult Add(UserCreditCard card)
        {
            var userId = _customerService.GetById(card.UserId);
            card.UserId = userId.Data.UserId;
            var result = _userCreditCardService.Add(card);
            return Ok(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int userId)
        {
            var result = _userCreditCardService.GetByUserId(userId);
            return Ok(result.Data);
        }

        [HttpGet("getallbyid")]
        public IActionResult GetAllById(int customerId)
        {
            var customer = _customerService.GetById(customerId);
            if (customer.Data == null)
            {
                return BadRequest();
            }
            var result = _userCreditCardService.GetAll(customer.Data.UserId);

            if (result.SuccessStatus)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
