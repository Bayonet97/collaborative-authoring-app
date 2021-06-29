using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.Commands.ChangePageCommand
{
    public class ChangePageCommandHandler : IRequestHandler<ChangePageCommand, CommandResponse>
    {
        private readonly IBookRepository bookRepository;

        public ChangePageCommandHandler(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }


        public async Task<CommandResponse> Handle(ChangePageCommand request, CancellationToken cancellationToken)
        {
            Page page = await bookRepository.FindPageAsync(request.PageId, request.BookId);
            Book book = await bookRepository.FindAsync(request.BookId, cancellationToken);

            if (request.PageChangeType == PageChangeType.ADDITION)
            {
                page.AddText(request.Letters, request.Position);
            }
            else if (request.PageChangeType == PageChangeType.SUBSTRACTION)
            {
                page.RemoveText(request.Amount, request.Position);
            }
            book.UpdatePage(page);
            bool success = await bookRepository.UpdateAsync(book);

            CommandResponse commandResponse = new()
            {
                Success = success
            };
            return commandResponse;
        }
    }
}
