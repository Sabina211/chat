using System.Globalization;
using Chat.BusinessLogic.Services.Chats;
using Chat.BusinessLogic.Services.Messages;
using Chat.BusinessLogic.Services.Users;
using Chat.DataAccess;
using Chat.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using Chat.Web;
using Chat.Web.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Chat.BusinessLogic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ChatDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("ChatDb"), 
        x => x.MigrationsAssembly("Chat.DataAccess")));

builder.Services.AddScoped<UserManager<UserEntity>>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentity<UserEntity, IdentityRole<long>>(options =>
{
    options.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<ChatDbContext>();

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
            ValidAudience = builder.Configuration["Jwt:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                if (string.IsNullOrEmpty(context.Error))
                    context.Error = "invalid_token";
                if (string.IsNullOrEmpty(context.ErrorDescription))
                    context.ErrorDescription = "This request requires a valid JWT access token to be provided";

                if (context.AuthenticateFailure == null ||
                    context.AuthenticateFailure.GetType() != typeof(SecurityTokenExpiredException))
                    return context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        error = context.Error,
                        error_description = context.ErrorDescription
                    }));
                var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                context.Response.Headers.Add("x-token-expired", authenticationException?.Expires.ToString(CultureInfo.CurrentCulture));
                context.ErrorDescription =
                    $"The token expired on {authenticationException?.Expires}";

                return context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    error = context.Error,
                    error_description = context.ErrorDescription
                }));
            }
        };
    });

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(CustomExceptionFilter));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();