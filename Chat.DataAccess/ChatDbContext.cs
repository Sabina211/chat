using Chat.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccess;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
    }

    public DbSet<ChatEntity> Chats { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<MessageEntity> Messages { get; set; } = null!;
}