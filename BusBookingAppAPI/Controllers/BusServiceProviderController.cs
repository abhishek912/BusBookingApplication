using BusBooking.Business.Authenticate;
using BusBooking.Data.Models;
using BusBooking.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    //[Authorize]
    public class BusServiceProviderController : Controller
    {
        private readonly IBusOperator op;
        public BusServiceProviderController(IBusOperator op)
        {
            this.op = op;
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index(int id)
        {
            var buses = op.GetBusByOperator(id);

            return Ok(buses);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] BusDTO bus)
        {
            var result = op.CreateBus(bus);
            return Ok(result);
        }

        [HttpGet]
        [Route("Details")]
        public IActionResult Details(int BusId)
        {
            var busDetails = op.GetBusDetailsByBusId(BusId);
            return Ok(busDetails);
        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit([FromBody] BusDTO bus)
        {
            var result = op.EditBusDetails(bus);
            return Ok(result);
        }

        [HttpGet]
        [Route("Remove")]
        public IActionResult Remove(int BusId)
        {
            bool result = op.RemoveBus(BusId);
            return Ok(result);
        }

        [HttpGet]
        [Route("BusBookings")]
        public IActionResult BusBookings(int busId)
        {
            var bookingData = op.GetBookingsOfBus(busId);
            return Ok(bookingData);
        }
    }
}
