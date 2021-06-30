using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthorizationService.API.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthorizeAsync(AuthenticateResult authResult);
        Task<bool> CheckAccountExistsAsync(string email);
        Task<bool> CreateAccountAsync(string email, string name);
    }
}
