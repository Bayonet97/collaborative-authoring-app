using CA.Services.AuthoringService.API.Application.Queries;
using CA.Services.AuthoringService.API.Application.Queries.GetBooksQuery;
using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.Queries.GetBooksQuery
{
    public class GetBooksRequestHandler : IRequestHandler<GetBooksQuery, QueryResponse<IEnumerable<Book>>>
    {
        private readonly IBookRepository bookRepository;

        public GetBooksRequestHandler(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<QueryResponse<IEnumerable<Book>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Book> books = await bookRepository.FindAllByUserId(request.UserId);
            QueryResponse<IEnumerable<Book>> queryResponse = new();

            queryResponse.Data = books;
            queryResponse.Success = true;

            return queryResponse;
        }
    }
}
