using CA.Services.AuthorizationService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.AuthorizationService.Infrastructure
{
    public class AuthorizationRepository : IAuthorizationContext
    {
        public List<User> Users { get; set; } = new List<User>();

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(1);
        }
    }
}
