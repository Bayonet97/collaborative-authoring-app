using CA.Services.RemarkService.Domain.AggregatesModel.BookAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.RemarkService.API.Application.Commands.UpdateRemarkCommand
{
    public class UpdateRemarkCommandHandler : IRequestHandler<UpdateRemarkCommand, CommandResponse>
    {
        private readonly IBookRepository bookRepository;

        public UpdateRemarkCommandHandler(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public async Task<CommandResponse> Handle(UpdateRemarkCommand request, CancellationToken cancellationToken)
        {
            Remark remark = new(guid: request.RemarkId, remarkedText: request.Text, position: (request.StartPosition, request.EndPosition), request.PageId, request.BookId) ;

            Remark updatedRemark = await bookRepository.UpdateRemarkAsync(remark);

            CommandResponse response = new()
            {
                Success = updatedRemark == remark
            };

            return response;    
        }
    }
}
