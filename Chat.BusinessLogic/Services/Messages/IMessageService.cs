using Chat.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.BusinessLogic.Services.Messages
{
    public interface IMessageService
    {
        public Task<long> Create(MessageEntity message);
        public List<MessageEntity> GetAll();
        public Task<MessageEntity> GetById(long id);
        public Task<MessageEntity> Update(MessageEntity message);
        public Task Delete(long id);
    }
}
