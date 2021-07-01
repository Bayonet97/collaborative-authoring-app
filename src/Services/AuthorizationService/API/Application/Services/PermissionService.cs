using CA.Services.AuthorizationService.API.Application.Interfaces;
using CA.Services.AuthorizationService.API.Application.Models;
using CA.Services.AuthorizationService.Domain;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CA.Services.AuthorizationService.API.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IAuthorizationContext _authorizationContext;
        private readonly ILogger<PermissionService> _logger;
        private readonly ITokenChecker _tokenChecker;
       // private readonly IMessagePublisher _publisher;
        public PermissionService(IAuthorizationContext authContext, ITokenChecker tokenChecker, ILogger<PermissionService> logger/*, IMessagePublisher publisher*/)
        {
            _authorizationContext = authContext;
            _logger = logger;
            _tokenChecker = tokenChecker;
        }
        public async Task<UserDto> SetUserClaims(string uid)
        {
            var response = new UserDto();

            try
            {
                var claimsDto = await _tokenChecker.VerifyTokenAsync(uid);

                if (claimsDto == null) return response;

                var userExist = _authorizationContext.Users.FirstOrDefault(x =>
                    x.LoginProviderId == claimsDto.Claims["user_id"].ToString());
                if (userExist == null)
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        Name = claimsDto.Claims["name"].ToString(),
                        LoginProviderId = claimsDto.Claims["user_id"].ToString(),
                        Email = claimsDto.Claims["email"].ToString()
                    };

                    _authorizationContext.Users.Add(user);
                    await _authorizationContext.SaveChangesAsync();

                    await NewUserRegistered(user);

                    var claims = new Dictionary<string, object>
                    {
                        {"Id", user.Id},
                        {"user", true},
                        {ClaimTypes.Role,"User" }
                    };
                    await _tokenChecker.AddClaims(claimsDto.Subject, claims);

                    _logger.LogInformation("Set user claims of user: " + user.Id);
                    return user;
                }
            }
            catch (FirebaseAuthException)
            {
                _logger.LogError("Claims could not be added to user!");
                throw;
            }
            catch (ArgumentNullException)
            {
                _logger.LogError("Claims could not be added to user!");
                throw;
            }

            return response;
        }
        private async Task NewUserRegistered(UserDto userDto)
        {
            //await _publisher.PublishMessageAsync<UserDTO>("UserCreatedEvent", userDTO);
        }

        public async Task<bool> AssignElevatedPermissions(Guid userToElevate, string roleToAssign)
        {
            var user =  _authorizationContext.Users.FirstOrDefault(x => x.Id == userToElevate);
            var claims = new Dictionary<string, object>
            {
                {"UserId", user.Id},
                {ClaimTypes.Role, roleToAssign}
            };
            return await _tokenChecker.AddClaims(user.LoginProviderId, claims);
        }
    }
}
