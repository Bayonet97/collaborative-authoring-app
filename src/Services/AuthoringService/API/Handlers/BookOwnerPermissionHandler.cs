using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Buffers;
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
    /// Represents the <see cref="BookOwnerRequirement"/> class.
    /// This class acts as a "marker class" to create an authorization requirement.
    /// </summary>
    public class BookOwnerRequirement : IAuthorizationRequirement
    {
        
    }

    /// <summary>
    /// Represents the <see cref="BookOwnerPermissionHandler"/> class.
    /// This class is used to authorize the highest level of modification of a book.
    /// </summary>
    public class BookOwnerPermissionHandler : AuthorizationHandler<BookOwnerRequirement>
    {
        private readonly IBookRepository _bookRepository;

        public BookOwnerPermissionHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, BookOwnerRequirement requirement)
        {
            if (await IsOwnerAsync(context.User, context.Resource as HttpContext))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }

        private async Task<bool> IsOwnerAsync(ClaimsPrincipal user, HttpContext context)
        {
            try
            {
                Guid userId = Guid.Parse(user.Claims.Single(claim => claim.Type == "Id").Value);
                BookRequest bookRequest = await GetBookRequest(context);
                return await _bookRepository.CheckBookOwner(userId, bookRequest.BookId);
            }
            catch(Exception e)
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

