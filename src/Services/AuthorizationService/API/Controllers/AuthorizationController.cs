using CA.Services.AuthorizationService.API.Application.Interfaces;
using CA.Services.AuthorizationService.API.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthorizationService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthorizationController : ControllerBase
    {
        private readonly IPermissionService _permissionServiceService;
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(IPermissionService permissionService, ILogger<AuthorizationController> logger)
        {
            _permissionServiceService = permissionService;
            _logger = logger;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AddClaimsRequest createAuthRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _permissionServiceService.SetUserClaims(createAuthRequest.Jwt);
                return new OkObjectResult(response);
            }

            return StatusCode(400);
        }

        [HttpPost("assignRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Moderator")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest assignRoleRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _permissionServiceService.AssignElevatedPermissions(assignRoleRequest.targetId, assignRoleRequest.roleToAssign);
                return new OkObjectResult(response);
            }

            return StatusCode(400);
        }
    }
}
