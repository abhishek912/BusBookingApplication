using BusBooking.Business.Authenticate;
using BusBooking.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BusBooking.Shared.DTO;

namespace BusBookingAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class HomeController : Controller
    {
        private readonly IHome homeObj;
        public HomeController(IHome homeObj)
        {
            this.homeObj = homeObj;
        }

        [HttpPost]
        [Route("Search")]
        public IActionResult Search([FromBody] SearchDTO data)
        {
            var searchResult = homeObj.GetSearchResults(data.Source, data.Destination, data.Date, data.Time, data.TimeType);

            return Ok(searchResult);
        }
    }
}
