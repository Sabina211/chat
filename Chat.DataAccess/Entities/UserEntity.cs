using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DataAccess.Entities
{
    public class UserEntity: BaseEntity
    {
        public string? Name { get; set; }
        public string Login { get; set; }
        public string? Description { get; set; }
    }
}
