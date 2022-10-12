using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chat.DataAccess.Entities;
using Chat.Web.Models;
using Microsoft.IdentityModel.Tokens;

namespace Chat.Web.Extensions;

public static class JwtExtensions
{
    public static string GenerateAccessToken(this UserEntity user, JwtOptions jwtOptions)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            issuer: jwtOptions.ValidIssuer,
            audience: jwtOptions.ValidAudience,
            claims: new List<Claim>
            {
                new (JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new (JwtRegisteredClaimNames.Name, user.UserName),
                new (JwtRegisteredClaimNames.Email, user.Email),
                new (JwtRegisteredClaimNames.Birthdate, user.BirthDate?.ToString("dd.MM.yyyy") ?? string.Empty)
            },
            expires: DateTime.Now.AddMinutes(jwtOptions.Expire),
            signingCredentials: signinCredentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
    }
}