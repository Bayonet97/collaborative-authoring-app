using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.RemarkService.API.Application.Commands.UpdateRemarkCommand
{
    public sealed class UpdateRemarkCommand : IRequest<CommandResponse>
    {
        public Guid RemarkId { get; init; }

        public Guid BookId { get; init; }

        public Guid PageId { get; init; }

        public string Text { get; init; }
        public int StartPosition { get; init; }
        public int EndPosition { get; init; }
    }
}
