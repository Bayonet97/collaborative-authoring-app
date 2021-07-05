using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Handlers
{

    /// <summary>
    /// Represents the <see cref="BookEditorRequirement"/> class.
    /// This class acts as a "marker class" to create an authorization requirement.
    /// </summary>
    public class BookEditorRequirement : IAuthorizationRequirement
    {

    }

    public class BookEditorPermissionHandler : AuthorizationHandler<BookEditorRequirement>
    {
        private readonly IBookRepository _bookRepository;

        public BookEditorPermissionHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, BookEditorRequirement requirement)
        {
            if (await IsEditorAsync(context.User, context.Resource as HttpContext))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }

        private async Task<bool> IsEditorAsync(ClaimsPrincipal user, HttpContext context)
        {
            try
            {
                Guid userId = Guid.Parse(user.Claims.Single(claim => claim.Type == "Id").Value);
                BookRequest bookRequest = await GetBookRequest(context);
                bool canEdit = await _bookRepository.CheckBookOwner(userId, bookRequest.BookId) || await _bookRepository.CheckCollaborator(userId, bookRequest.BookId);
                return canEdit;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        private async static Task<BookRequest> GetBookRequest(HttpContext context)
        {

            string read = await new StreamReader(context.Request.Body).ReadToEndAsync();
            // TODO: error checking

            // NOTE: Each request for adding, deleting, or updating a book has the property "BookId".
            BookRequest bookRequest = JsonSerializer.Deserialize<BookRequest>(Encoding.ASCII.GetBytes(read));

            return bookRequest;
        }
    }
}
