using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.Queries
{
    public record GetBooksQuery : IRequest<List<Book>>
    {
        public Guid UserId { get; set; }

        public GetBooksQuery(Guid guid)
        {
            UserId = guid;
        }
    }
}
