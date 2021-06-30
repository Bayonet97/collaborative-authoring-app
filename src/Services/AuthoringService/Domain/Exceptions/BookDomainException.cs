using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Domain.Exceptions
{
    internal sealed class BookDomainException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KweetDomainException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        internal BookDomainException(string message) : base(message)
        {
        }
    }
}
