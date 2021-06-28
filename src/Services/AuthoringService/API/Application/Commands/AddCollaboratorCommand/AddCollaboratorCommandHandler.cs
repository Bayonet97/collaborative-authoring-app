using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.Commands.AddCollaboratorCommand
{
    public class AddCollaboratorCommandHandler : IRequestHandler<AddCollaboratorCommand, CommandResponse>
    {
        private readonly IBookRepository bookRepository;

        public AddCollaboratorCommandHandler(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public async Task<CommandResponse> Handle(AddCollaboratorCommand request, CancellationToken cancellationToken)
        {

            Book book = await bookRepository.FindAsync(request.BookId, cancellationToken);
            book.AddCollaborator(request.CollaboratorId);
            bool success = await bookRepository.UpdateAsync(book);

            CommandResponse commandResponse = new()
            {
                Success = success
            };
            return commandResponse;
        }
    }
}
