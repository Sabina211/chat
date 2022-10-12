using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chat.BusinessLogic.Exceptions
{
    public class UserIdentityException : Exception, ISerializable
    {
        public override string? Message { get; }
        public UserIdentityException(string message = "Не удалось выполнить действие") : base(message)
        {
        }

        public UserIdentityException(IEnumerable<IdentityError> errorsList)
        {
            Message = string.Join(';', errorsList.Select(x => x.Description));//вынести в хелпер
        }
    }
}
