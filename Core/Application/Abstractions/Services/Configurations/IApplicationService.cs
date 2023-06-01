using Application.DTOs.Configuration;

namespace Application.Abstractions.Services.Configurations;

public interface IApplicationService
{
    List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
}