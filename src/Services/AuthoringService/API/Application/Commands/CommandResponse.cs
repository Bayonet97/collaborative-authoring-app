using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.Commands
{
    /// <summary>
    /// Represents the <see cref="CommandResponse"/> record.
    /// </summary>
    public sealed record CommandResponse : Response
    {
        public static explicit operator CommandResponse(Task<object> v)
        {
            throw new NotImplementedException();
        }
    }
}
