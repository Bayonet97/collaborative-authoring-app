using CA.Services.AuthorizationService.API.Application.Interfaces;
using CA.Services.AuthorizationService.API.Application.Models;
using CA.Services.AuthorizationService.Domain;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthorizationService.API.Application.Services
{
    public class CaAuthorizationService : IAuthService
    {
    
        private readonly IAuthorizationContext authorizationContext;

        public async Task<string> AuthorizeAsync(AuthenticateResult authResult)
        {
            //  AuthResponseDto response = await _authHttpRequest.SendAuthRequest(code);
            var claims = authResult.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            var email = claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            var name = claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            //Check if Account already exists, and register one if this is not the case. Afterwards generate a JWT
            if (!CheckAccountExistsAsync(email).Result)
            {
                await CreateAccountAsync(email, name);
            }

            return null;//TokenGenerator.GenerateToken(JWTSettings.SecretKey, email);
        }

        public async Task<bool> CheckAccountExistsAsync(string email)
        {
            return authorizationContext.Users.FirstOrDefault(x => x.Email == email) != null;
        }

        public async Task<bool> CreateAccountAsync(string email, string name)
        {
            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email
            };
            await authorizationContext.Users.AddAsync(newUser);
            bool success = await authorizationContext.SaveChangesAsync() > 0;

            UserDto userDTO = newUser;

            //await _publisher.PublishMessageAsync<UserDTO>("UserCreatedEvent", userDTO);
            if (!success)
            {
                throw new InvalidOperationException("Something went wrong trying to create the account");
            }
            return true;
        }
    }
}
