using Chat.BusinessLogic.Services.Chats;
using Chat.BusinessLogic.Services.Messages;
using Chat.BusinessLogic.Services.Users;
using Chat.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ChatDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("ChatDb"), 
        x => x.MigrationsAssembly("Chat.DataAccess")));

builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMessageService, MessageService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var s = scope.ServiceProvider;
    var c = s.GetRequiredService<ChatDbContext>();
    CreateDB.Create(c);
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();