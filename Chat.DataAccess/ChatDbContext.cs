using Chat.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccess;

public class ChatDbContext : IdentityDbContext<UserEntity, IdentityRole<long>, long>
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<ChatEntity> Chats { get; set; } = null!;
    public DbSet<MessageEntity> Messages { get; set; } = null!;
}