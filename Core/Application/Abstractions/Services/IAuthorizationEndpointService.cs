namespace Application.Abstractions.Services;

public interface IAuthorizationEndpointService
{
    public Task AssignRoleEndpointAsync(string[] roles,string menu, string endpointCode, Type type);

    public Task<List<string>> GetRolesToEndpointAsync(string code, string menu);
}