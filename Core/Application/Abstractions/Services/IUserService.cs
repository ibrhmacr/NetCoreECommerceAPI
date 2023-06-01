using Application.DTOs.User;
using Domain.Entities.Identity;

namespace Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model);

    Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);

    Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);

    Task<List<ListUserDTO>> GetAllUsersAsync(int page, int size);
    
    int TotalUsersCount { get; }

    Task AssignRoleToUserAsync(string userId, string[] roles);

    Task<string[]> GetRolesUserAsync(string userIdOrName);

    Task<bool> HasRolePermissionToEndpointAsync(string name, string code);


}