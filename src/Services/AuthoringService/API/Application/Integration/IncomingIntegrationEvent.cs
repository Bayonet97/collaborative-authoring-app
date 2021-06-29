using CA.Services.AuthoringService.API.Application.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.Integration
{
    /// <summary>
    /// Represents the <see cref="IncomingIntegrationEvent"/> class.
    /// Used as marker class for integration events to enable the event to go through the validation, exception, and transaction pipeline.
    /// </summary>
    public abstract class IncomingIntegrationEvent : IntegrationEvent, IRequest<CommandResponse>
    {

    }
}
