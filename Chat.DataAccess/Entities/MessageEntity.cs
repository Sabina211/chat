using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DataAccess.Entities
{
    public class MessageEntity: BaseEntity
    {
        public UserEntity Sender { get; set; } = null!;
        public ChatEntity Chat { get; set; }
        public string Text { get; set; }
        public bool IsRead { get; set; } = false;
    }
}