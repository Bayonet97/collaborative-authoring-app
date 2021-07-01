using CA.Services.AuthorizationService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthorizationService.Domain
{
    public interface IAuthorizationContext
    {
        List<User> Users { get; set; }
        Task<int> SaveChangesAsync();
    }
}
