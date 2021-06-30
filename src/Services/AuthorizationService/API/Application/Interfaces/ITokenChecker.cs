using CA.Services.AuthorizationService.API.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthorizationService.API.Application.Interfaces
{
    public interface ITokenChecker
    {
        Task<ClaimsDto> VerifyTokenAsync(string jwt);
        Task<bool> AddClaims(string jwt, Dictionary<string, object> claims);
        Task<bool> CheckValidPermissions(string jwt);
    }

}
