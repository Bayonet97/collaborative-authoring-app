using CA.Services.AuthorizationService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Services.AuthorizationService.API.Application.Models
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public static implicit operator UserDto(User u)
        {
            UserDto user = new UserDto()
            {
                Name = u.Name,
                Email = u.Email
            };
            return user;
        }
    }
}
