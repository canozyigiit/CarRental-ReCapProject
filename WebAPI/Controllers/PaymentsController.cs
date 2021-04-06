using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IRentalService rentalService, IPaymentService paymentService)
        {
            _paymentService = paymentService;
           
        }

        [HttpPost("payment")]
        public IActionResult Payment(UserCreditCard card)
        {
            Thread.Sleep(2000);
            var result = _paymentService.Payment(card);
            return Ok(result);
        }
    }
}
