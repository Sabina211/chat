using Chat.DataAccess;
using Chat.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.BusinessLogic.Services.Chats;

public class ChatService : IChatService
{
    private readonly ChatDbContext _context;

    public ChatService(ChatDbContext context)
    {
        _context = context;
    }

    public async Task<long> Create(ChatEntity chat)
    {
        var result = await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();
        
        return result.Entity.Id;
    }

    public List<ChatEntity> GetAll()
    {
        var result = _context.Chats.ToList();

        return result;
    }

    public async Task<ChatEntity> GetById(long id)
    {
        var result = await _context.Chats.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new Exception("Entity not found exception");
        }
        
        return result;
    }

    public async Task<ChatEntity> Update(ChatEntity chat)
    {
        var result = _context.Chats.Update(chat);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task Delete(long id)
    {
        var chat = await _context.Chats.FirstOrDefaultAsync(x => x.Id == id);
        
        if (chat == null)
        {
            throw new Exception("Entity not found exception");
        }
        
        _context.Chats.Remove(chat);
        await _context.SaveChangesAsync();
    }
}