using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthorizationService.API.Application.Interfaces
{
    public interface IAuthHttpRequest
    {
        Task<bool> SendAuthRequest(string code);
    }
}
