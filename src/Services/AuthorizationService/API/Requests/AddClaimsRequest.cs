using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthorizationService.API.Requests
{
    public class AddClaimsRequest
    {
        public string Jwt { get; set; }
    }
}
