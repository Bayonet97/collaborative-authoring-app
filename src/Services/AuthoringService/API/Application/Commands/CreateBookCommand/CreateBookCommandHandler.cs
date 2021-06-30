using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.Commands.CreateBookCommand
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CommandResponse>
    {
        private readonly IBookRepository _bookRepository;

        public CreateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<CommandResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            Book book = new(Guid.NewGuid(), request.UserId, request.Title, Guid.NewGuid());

            bool success = await _bookRepository.CreateAsync(book);

            CommandResponse commandResponse = new()
            {
                Success = success
            };

            return commandResponse;
        }
    }
}
