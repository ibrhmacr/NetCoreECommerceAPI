  using System.Text;
using Application.Abstractions.Services;
using Application.DTOs.User;
using Application.Exceptions;
using Application.Features.Commands.User.Create;
using Application.Helpers;
  using Application.Repositories.Endpoint;
  using Domain.Entities;
  using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEndpointReadRepostiory _endpointReadRepostiory;

    public UserService(UserManager<AppUser> userManager, IEndpointReadRepostiory endpointReadRepostiory)
    {
        _userManager = userManager;
        _endpointReadRepostiory = endpointReadRepostiory;
    }
    public async Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model)
    {
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = model.Username,
            Name = model.Name,
            Surname = model.Surname,
            Email = model.Email,
        }, model.Password);
        
        CreateUserResponseDTO response = new() { Succeded = result.Succeeded };
        if (result.Succeeded)
            response.Message = "User has been created Succesfully";
        else
            foreach (IdentityError error in result.Errors)
                response.Message += $"{error.Code} - {error.Description}";
        return response;

    }

    public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, 
        int addOnAccessTokenDate)
    {
        if (user != null)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
            await _userManager.UpdateAsync(user);
        }
        else
        {
            throw new NotFoundUserException();
        }

        
    }

    public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
    {
        AppUser user =  await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            resetToken = resetToken.UrlDecode();
            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }
            else
            {
                throw new PasswordChangeFailedException();
            }
        }
    }

    public async Task<List<ListUserDTO>> GetAllUsersAsync(int page, int size)
    {
        var users = await _userManager.Users.Skip(page * size).Take(size).ToListAsync();

        return users.Select(user => new ListUserDTO()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Surname = user.Surname,
            TwoFactorEnabled = user.TwoFactorEnabled,
            UserName = user.UserName
        }).ToList();
    }
    public int TotalUsersCount => _userManager.Users.Count();
    
    
    public async Task AssignRoleToUserAsync(string userId, string[] roles)
    {
        AppUser? user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, roles);
        }
    }

    public async Task<string[]> GetRolesUserAsync(string userIdOrName)
    {
        AppUser? user = await _userManager.FindByIdAsync(userIdOrName);
        if (user == null)
        {
            user = await _userManager.FindByNameAsync(userIdOrName);
        }
        
        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles.ToArray();
        }
        else
        {
            return new string[]{};
        }
    }

    public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
    {
        //Userin sahip oldugu yetkileri aliyoruz
        var userRoles = await GetRolesUserAsync(name);
        if (!userRoles.Any())
        {
            return false;
        }
        
        //Daha sonra Istek gelen endpointin rollerini aliyoruz
        Endpoint? endpoint = await _endpointReadRepostiory.Table
            .Include(e => e.Roles)
            .FirstOrDefaultAsync(e => e.Code == code);

        if (endpoint == null)
            return false;

        
        //Kontrolu saglayip eslesen roller var ise true yok ise false donucez.
        var endpointRoles = endpoint.Roles.Select(r => r.Name);

        foreach (var userRole in userRoles)
        {
            foreach (var endpointRole in endpointRoles)
                if (userRole == endpointRole)
                    return true;
        }
        return false;

    }
}