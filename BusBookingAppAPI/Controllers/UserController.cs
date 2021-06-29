using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking.Data.Models;
using BusBooking.Data.Context;
using BusBooking.Business.Authenticate;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace BusBookingAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IAccount _account;

        public UserController(ILogger<UserController> logger, IAccount account)
        {
            _logger = logger;
            _account = account;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Credential credentials)
        {
            var valid = _account.ValidateUser(credentials.Contact, credentials.Password);
            if(valid)
            {
                var isAdmin = _account.IsAdmin(credentials.Contact);
                var userId = _account.GetUserIdByContact(credentials.Contact).ToString();
                var fullName = _account.GetFullName(credentials.Contact);
                if (isAdmin)
                {
                    //token will be appended with the response to authenticate.
                    return Ok("admin, " + userId + ", " + fullName);
                }
                else
                {
                    return Ok("NormalUser, " + userId + ", " + fullName);
                }
            }
            else
            {
                return Ok("InValid");
            }
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult SignUp([FromBody] User user)
        {
            var result = _account.AddUser(user);
            return Ok(result);
        }
    }
}
