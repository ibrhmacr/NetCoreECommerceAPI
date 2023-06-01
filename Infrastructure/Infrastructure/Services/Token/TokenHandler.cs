using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Abstractions.Token;
using Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Token;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Application.DTOs.Token CreateAccessToken(int second, AppUser user)
    {
        Application.DTOs.Token token = new();
        
        //SecurityKey in simetrigini aliyoruz
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
        
        
        //Sifrelenmis Kimligi olusturuyoruz.
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256); 
        
        //Olusturulacak Token Ayarlarini veriyoruz.
        token.Expiration = DateTime.UtcNow.AddHours(second); //Todo seconds a cevirmeyi unutma
        JwtSecurityToken securityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials,
            claims: new List<Claim>{new (ClaimTypes.Name,user.UserName)}
        );
        
        //Token olusturucu sinifindan ornek aliyoruz
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        token.RefreshToken = CreateRefreshToken();

        return token;


    }
    public string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using RandomNumberGenerator randomNumber = RandomNumberGenerator.Create();
        randomNumber.GetBytes(number);
        return Convert.ToBase64String(number);
        
    }
}