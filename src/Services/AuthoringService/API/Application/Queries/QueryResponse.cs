using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.Queries
{
    public sealed record QueryResponse<T> : Response where T : class
    {
        public T Data { get; set; }
    }
}
