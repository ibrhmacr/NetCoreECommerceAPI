using System.Text;
using Application.Abstractions.Services;
using Application.Abstractions.Token;
using Application.DTOs;
using Application.Exceptions;
using Application.Features.Commands.User.Login;
using Application.Helpers;
using Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IUserService _userService;
    private readonly IMailService _mailService;

    public AuthService(IConfiguration configuration, UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService, IMailService mailService)
    {
        _configuration = configuration;
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _signInManager = signInManager;
        _userService = userService;
        _mailService = mailService;
    }
    
    public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifetime)
    {
        AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
        
        if (user == null)
            user = await _userManager.FindByEmailAsync(usernameOrEmail);
        
        if (user==null) 
            throw new NotFoundUserException();
        
        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (result.Succeeded) //Authentication succesfully
        {
            Token token = _tokenHandler.CreateAccessToken(accessTokenLifetime, user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
            return token;
        }
        else
            throw new AuthenticationErrorException();
    }

    public async Task<Token> RefrehTokenLoginAsync(string refreshToken)
    {
        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
        {
            Token token = _tokenHandler.CreateAccessToken(15, user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);
            return token;
        }
        else
        {
            throw new NotFoundUserException();
        }
    }

    public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifetime)
    {
        //Gelen kullanicinin dogrulama degerlerini veriyoruz
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { _configuration["ExternalLoginSettings:Google:ClientId"] }
        };
        
        //Daha sonra gelen kullaniciya ait token degerlerini degerlendirme islemini yapiyoruz Async bir calisma
        //yaptigimiz icin await ile beklememiz gerekir ki info kisminda sorun yasamayalim.
        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
        
        //Identity Library de disaridan gelen kullanicilari ayirt etmemimizi saglayan bir tablo da bulunmaktadir
        //Bu kullanicilari Logins tablosunda ayrica gorebiliriz.
        var infos = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

        AppUser? user = await _userManager.FindByLoginAsync(infos.LoginProvider, infos.ProviderKey);
        //Disaridan gelen kullanici(null gelme durumu) logins tablosunda yoksa haliyle users tablosunda da olmayacaktir.
        //Bundan dolayi gelen kullaniciyi veritabaninda olup olmadigini kontrol edip yoksa eklememiz gerekir.
        bool result = user != null;
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(payload.Email);
            if (user ==null)
            {
                user = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = payload.Email,
                    Email = payload.Email,
                    Name = payload.Name,
                    Surname = payload.FamilyName
                };
                var identiyResult = await _userManager.CreateAsync(user);
                result = identiyResult.Succeeded; 
            }
        }

        if (result)
        {
            await _userManager.AddLoginAsync(user, infos);
            Token token = _tokenHandler.CreateAccessToken(accessTokenLifetime, user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
            return token;
        }
        else
            throw new Exception("Invalid External Authentication");
    }

    public async Task PasswordResetAsync(string email)
    {
        AppUser user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            resetToken = resetToken.UrlEncode();
            
            //byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
            //resetToken = WebEncoders.Base64UrlEncode(tokenBytes); //WebEncoders url de token tasiyabilmemizi saglayan bir siniftir 

            await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);

        }
    }

    public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
    {
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            resetToken = resetToken.UrlDecode();
            //byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
            //resetToken = Encoding.UTF8.GetString(tokenBytes);

            return await _userManager
                .VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword",
                    resetToken);
        }

        return false;
    }
}