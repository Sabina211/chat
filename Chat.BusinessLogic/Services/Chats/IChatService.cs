using Chat.DataAccess.Entities;

namespace Chat.BusinessLogic.Services.Chats;

public interface IChatService
{
    public Task<long> Create(ChatEntity chat);
    public List<ChatEntity> GetAll();
    public Task<ChatEntity> GetById(long id);
    public Task<ChatEntity> Update(ChatEntity chat);
    public Task Delete(long id);
}