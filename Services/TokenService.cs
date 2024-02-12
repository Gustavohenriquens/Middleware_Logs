using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TesteMiddleware.api.Entities;

namespace TesteMiddleware.api.Services
{
    //public class TokenService : ITokenService
    //{
    //   public string GenerateToken(LoginModel loginModel)
    //    {
    //        var key = Encoding.ASCII.GetBytes(Settings.Secret);

    //        var tokenConfig = new SecurityTokenDescriptor
    //        {
    //            Subject = new ClaimsIdentity(new Claim[]
    //            {
    //                new Claim("usuarioId", loginModel.Id.ToString()),
    //            }),
    //            Expires = DateTime.UtcNow.AddHours(3),
    //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //        };

    //        var tokenHandler = new JwtSecurityTokenHandler();

    //        var token = tokenHandler.CreateToken(tokenConfig);

    //        var tokenString = tokenHandler.WriteToken(token);

    //        return tokenString;

    //    }
    //}
}
