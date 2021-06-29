using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Application.Commands.ChangePageCommand
{
    public enum PageChangeType
    {
        ADDITION = 0,
        SUBSTRACTION
    }
    public class ChangePageCommand : IRequest<CommandResponse>
    {
        public Guid UserId { get; set; }

        public Guid BookId { get; set; }

        public Guid PageId { get; set; }
        public PageChangeType PageChangeType { get; set; }

        public string Letters { get; set; }

        public int Position { get; set; }

        public int Amount { get; set; }
    }
}
