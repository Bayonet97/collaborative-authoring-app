﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.Commands.CreateBookCommand
{
    public record CreateBookCommand : IRequest<CommandResponse>
    {
        public Guid UserId { get; init; }

        public string Title { get; init; }


    }
}
