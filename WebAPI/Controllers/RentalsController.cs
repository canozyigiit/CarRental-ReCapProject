using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("getalldetails")]
        public IActionResult GetAllDetails()
        {
            var result = _rentalService.GetDetailsAll();
            if (result.SuccessStatus)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _rentalService.GetAll();
            if (result.SuccessStatus)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcarcontrol")]
        public IActionResult GetCarControl(int carId)
        {
            var result = _rentalService.RentalCarControl(carId);
            if (result.SuccessStatus)
            {
                  return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("addrental")]
        public IActionResult AddRental(Rental rental)
        {
            var result = _rentalService.Add(rental);
            return Ok(result);
        }
        [HttpPost("isrentable")]
        public IActionResult IsRentable(Rental rental)
        {
            var result = _rentalService.IsRentable(rental);
            if (result.SuccessStatus) return Ok(result);

            return BadRequest(result);
        }
    }
}