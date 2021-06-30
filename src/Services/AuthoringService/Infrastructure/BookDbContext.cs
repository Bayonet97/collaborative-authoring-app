using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Infrastructure
{
    public class BookDbContext : DbContext
    {
        private readonly IMediator _mediator;


    }
}
