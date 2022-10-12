using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chat.BusinessLogic.Exceptions
{
    public class EntityNotFoundException : Exception, ISerializable
    {        
        public EntityNotFoundException(string message = "Не удалось найти объект") : base(message)
        {
        }
    }
}
