using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application
{
    public abstract record Response
    {
        /// <summary>
        /// Gets or sets the execution result success.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the error messages in case of an execution failure.
        /// </summary>
        public List<string> Errors { get; set; } = new();
    }
}
