using Chat.DataAccess;
using Chat.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.BusinessLogic.Services.Messages
{
    public class MessageService : IMessageService
    {
        private readonly ChatDbContext _context;

        public MessageService(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<long> Create(MessageEntity message)
        {
            var result = await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task Delete(long id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(x=>x.Id==id);
            if (message == null) throw new Exception("Entity not found exception");
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }

        public List<MessageEntity> GetAll()
        {
            var messages = _context.Messages.ToList();
            return messages;
        }

        public async Task<MessageEntity> GetById(long id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == id);
            if (message == null) throw new Exception("Entity not found exception");
            return message;
        }

        public async Task<MessageEntity> Update(MessageEntity message)
        {
            var result = _context.Messages.Update(message);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
