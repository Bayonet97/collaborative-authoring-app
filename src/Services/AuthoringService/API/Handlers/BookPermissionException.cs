using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Handlers
{
    public class BookPermissionException : Exception
    {
        public BookPermissionException() { }

        public BookPermissionException(string message)
            : base(message) { }

        public BookPermissionException(string message, Exception inner)
            : base(message, inner) { }

    }

}
