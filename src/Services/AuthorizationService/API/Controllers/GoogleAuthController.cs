
using CA.Services.AuthorizationService.API.Application.Interfaces;
using CA.Services.AuthorizationService.API.Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthorizationService.API.Controllers
{
    public class GoogleAuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        //private readonly IMessagePublisher _publisher;

        public GoogleAuthController(IAuthService authService/*, IMessagePublisher publisher*/)
        {
            _authService = authService;
          /*  _publisher = publisher;*/
        }

        [Route("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register()
        {
            if (!ModelState.IsValid) return BadRequest();
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var jwt = await _authService.AuthorizeAsync(result);

            return Ok(jwt);
        }
    }
}
