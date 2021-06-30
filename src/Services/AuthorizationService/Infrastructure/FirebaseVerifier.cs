using CA.Services.AuthorizationService.API.Application.Interfaces;
using CA.Services.AuthorizationService.API.Application.Models;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.AuthorizationService.Infrastructure
{
    public class FirebaseVerifier : ITokenChecker
    {
        private readonly FirebaseAuth _firebaseApp;

        public FirebaseVerifier(FirebaseApp firebaseApp)
        {
            _firebaseApp = _firebaseApp = FirebaseAuth.GetAuth(firebaseApp);
        }
        public async Task<bool> AddClaims(string jwt, Dictionary<string, object> claims)
        {
            await _firebaseApp.SetCustomUserClaimsAsync(jwt, claims);
            return true;
        }

        public async Task<ClaimsDto> VerifyTokenAsync(string jwt)
        {
            try
            {
                var token = await _firebaseApp.VerifyIdTokenAsync(jwt);

                var claimsDto = new ClaimsDto
                {
                    Subject = token.Subject,
                    Audience = token.Audience,
                    Issuer = token.Issuer,
                    ExpirationTimeSeconds = token.ExpirationTimeSeconds,
                    IssuedAtTimeSeconds = token.IssuedAtTimeSeconds,
                    Claims = token.Claims
                };

                return claimsDto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task<bool> CheckValidPermissions(string jwt)
        {
            try
            {
                var token = await _firebaseApp.VerifyIdTokenAsync(jwt);

                UserRecord user = await FirebaseAuth.DefaultInstance.GetUserAsync(token.Uid);
                if (user.CustomClaims["Role"] is "Admin" or "Moderator")
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
