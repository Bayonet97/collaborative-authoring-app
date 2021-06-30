using CA.Services.AuthorizationService.API.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthorizationService.API.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<UserDto> SetUserClaims(string uid);
        Task<bool> AssignElevatedPermissions(Guid userToElevate, string roleToAssign);
    }
}
