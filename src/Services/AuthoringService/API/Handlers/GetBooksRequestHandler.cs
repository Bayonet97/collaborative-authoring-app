using CA.Services.AuthoringService.API.Application.Queries;
using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.DomainEventHandlers
{
    public class GetBooksRequestHandler : IRequestHandler<GetBooksQuery, List<Book>>
    {
        private readonly IBookRepository bookRepository;

        public GetBooksRequestHandler(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(bookRepository.FindAllByUserId(request.UserId));
        }
    }
}
