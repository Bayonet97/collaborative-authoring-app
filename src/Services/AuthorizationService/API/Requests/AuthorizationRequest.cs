using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthorizationService.API.Requests
{
    public class AuthorizationRequest
    {
        [Required]
        public string Code { get; set; }
    }
}
