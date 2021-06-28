using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.Commands.AddCollaboratorCommand
{
    public record AddCollaboratorCommand : IRequest<CommandResponse>
    {
        public Guid BookId { get; set; }

        public Guid CollaboratorId { get; set; }
    }
}
