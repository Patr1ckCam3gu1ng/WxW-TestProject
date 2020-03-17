namespace WuxiaWorld.Controllers {

    using System;

    using BLL.Services.Interfaces;

    using DAL.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    [Route("api/login")]
    public class LoginController : Controller {
        private readonly IAuthenticate _authenticate;

        public LoginController(IAuthenticate authenticate) {

            _authenticate = authenticate ?? throw new ArgumentNullException(nameof(authenticate));
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login) {

            if (_authenticate.Validate(login)) {

                var jwtToken = _authenticate.GenerateToken(login.UserName);

                return Ok(jwtToken);
            }

            return Unauthorized();
        }
    }

}