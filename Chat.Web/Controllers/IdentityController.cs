using Chat.BusinessLogic.Exceptions;
using Chat.DataAccess;
using Chat.DataAccess.Entities;
using Chat.Web.Extensions;
using Chat.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Chat.Web.Controllers;

[ApiController]
[Route("identity")]
public class IdentityController : ApiControllerBase
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly JwtOptions _jwtOptions;
    private readonly ChatDbContext _context;

    public IdentityController(UserManager<UserEntity> userManager, IOptions<JwtOptions> jwtOptions, ChatDbContext context)
    {
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);

        if (user == null)
        {
            return BadRequest();
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password)) return Unauthorized();

        return Ok(new LoginResponse
        {
            AccessToken = user.GenerateAccessToken(_jwtOptions)
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (request.RetypePassword != request.Password)
        {
            return BadRequest();
        }
        var user = new UserEntity
        {
            Email = request.Email,
            Name = request.Name,
            Description = request.Description,
            BirthDate = request.BirthDate,
            UserName = request.UserName
        };
        
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new UserIdentityException(result.Errors);


        return await Login(new LoginRequest
        {
            UserName = request.UserName,
            Password = request.Password
        });
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == CurrentUser.Id);
        
        return Ok(user);
    }
}