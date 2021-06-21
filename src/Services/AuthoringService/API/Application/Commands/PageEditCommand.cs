using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CA.Services.AuthoringService.API.Application.Commands
{
    public class PageEditCommand : IRequest<int>
    {
        public int ListId { get; set; }

        public string Title { get; set; }


    }
}