using CA.Services.AuthoringService.API.Application.Commands;
using CA.Services.AuthoringService.API.Application.Commands.AddCollaboratorCommand;
using CA.Services.AuthoringService.API.Application.Commands.CreateBookCommand;
using CA.Services.AuthoringService.API.Application.Queries;
using CA.Services.AuthoringService.API.Application.Queries.GetBooksQuery;
using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CA.Services.AuthoringService.API.Controllers
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoringController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthoringController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<AuthoringRestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthoringRestController>/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAsync(Guid userId)
        {
            GetBooksQuery query = new(userId);
            QueryResponse<IEnumerable<Book>> queryResponse = await _mediator.Send(query);

            return new OkObjectResult(queryResponse);
        }

        /// <summary>
        /// Create a book asynchronously through the create book command.
        /// </summary>
        /// <param name="command">The create book command.</param>
        /// <returns>Returns the command response.</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(CreateBookCommand command)
        {
            //Guid userId = Guid.Parse(HttpContext.User.Claims.Single(claim => claim.Type == "UserId").Value);
            //if (command.UserId != userId)
             //   return UnauthorizedCommand();

            CommandResponse commandResponse = await _mediator.Send(command);
            return commandResponse.Success
                ? new OkObjectResult(commandResponse)
                : BadRequest(commandResponse);
        }

        /// <summary>
        /// Create a book asynchronously through the create book command.
        /// </summary>
        /// <param name="command">The create book command.</param>
        /// <returns>Returns the command response.</returns>
        [HttpPut("AddCollaborator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCollaboratorAsync(AddCollaboratorCommand command)
        {
            // Guid userId = Guid.Parse(HttpContext.User.Claims.Single(claim => claim.Type == "UserId").Value);
            //if (command.UserId != userId)
            //   return UnauthorizedCommand();

            CommandResponse commandResponse = await _mediator.Send(command);
            return commandResponse.Success
                ? new OkObjectResult(commandResponse)
                : BadRequest(commandResponse);


        }
        // PUT api/<AuthoringRestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [Authorize(Policy = "IsBookOwner")]
        [HttpDelete("{id:guid}")]
        public string Delete(Guid id)
        {
            Console.WriteLine("Hello I am a BOOK OWNER!");
            return "Hello I am a BOOK OWNER, and I DELETED my book.";
        }

        private IActionResult UnauthorizedCommand()
        {
            return Unauthorized(new CommandResponse()
            {
                Errors = new List<string>() { "The user id claim and provided user id are not the same." },
                Success = false
            });
        }
    }
}
