using CA.Services.AuthoringService.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate
{
    public class User
    {
        public Guid Id { get; protected set; }

        private string userName;

        /// <summary>
        /// Gets and sets the user name.
        /// </summary>
        public string UserName
        {
            get => userName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new BookDomainException("The user name is null, empty or contains only whitespaces.");
                if (value.Length > 32)
                    throw new BookDomainException("The user name length exceeded 32 characters.");
                if (!value.All(char.IsLetterOrDigit))
                    throw new BookDomainException("The user name is not alphanumeric.");
                userName = value;
            }
        }

        public User()
        {

        }

        public User(Guid guid, string _userName)
        {
            Id = guid;
            UserName = _userName;
        }



    }

}
