using BusBooking.Business.Authenticate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking.Shared.DTO;

namespace BusBookingAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class BookingController : Controller
    {
        private readonly IBusBooking bookObj;
        public BookingController(IBusBooking bookObj)
        {
            this.bookObj = bookObj;
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index(int id)
        {
            var bookings = bookObj.GetBookings(id);
            return Ok(bookings);
        }

        [HttpGet]
        [Route("Details")]
        public IActionResult Details(int BookingId)
        {
            var bookings = bookObj.GetBookingDetails(BookingId);
            return Ok(bookings);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] BookingTicketDTO ticket)
        {
            var bookings = bookObj.CreateBooking(ticket);
            return Ok(bookings);
        }

        [HttpGet]
        [Route("Cancel")]
        public IActionResult Cancel(int BookingId)
        {
            var bookings = bookObj.CancelBooking(BookingId);
            return Ok(bookings);
        }

        [HttpPost]
        [Route("PassengerWise/Cancel")]
        public IActionResult Cancel(int BookingId, List<int> PsngrId, bool refund)
        {
            var result = bookObj.CancelPassengersBooking(BookingId, PsngrId, refund);
            return Ok(result);
        }
    }
}
