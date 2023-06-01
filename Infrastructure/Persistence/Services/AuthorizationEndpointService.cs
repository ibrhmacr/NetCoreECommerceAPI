using Application.Abstractions.Services;
using Application.Abstractions.Services.Configurations;
using Application.Repositories.Endpoint;
using Application.Repositories.Menu;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Persistence.Services;

public class AuthorizationEndpointService : IAuthorizationEndpointService
{
    private readonly IApplicationService _applicationService;
    private readonly IEndpointReadRepostiory _endpointReadRepostiory;
    private readonly IEndpointWriteRepository _endpointWriteRepository;
    private readonly IMenuReadRepository _menuReadRepository;
    private readonly IMenuWriteRepository _menuWriteRepository;
    private readonly RoleManager<AppRole> _roleManager;

    public AuthorizationEndpointService(IApplicationService applicationService, IEndpointReadRepostiory endpointReadRepostiory, IEndpointWriteRepository endpointWriteRepository, IMenuReadRepository menuReadRepository, IMenuWriteRepository menuWriteRepository, RoleManager<AppRole> roleManager)
    {
        _applicationService = applicationService;
        _endpointReadRepostiory = endpointReadRepostiory;
        _endpointWriteRepository = endpointWriteRepository;
        _menuReadRepository = menuReadRepository;
        _menuWriteRepository = menuWriteRepository;
        _roleManager = roleManager;
    }


    //Repository kismini any veya exist mantiginda bir ekleme veya gelistirme yapilabilir
    
    public async Task AssignRoleEndpointAsync(string[] roles,string menu, string endpointCode, Type type) //Buradaki type API kendisi vericek sekilde duzenlenicek
    {
        Menu _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
        if (_menu == null)
        {
            _menu = new()
            {
                Id = Guid.NewGuid(),
                Name = menu
            };
            await _menuWriteRepository.AddAsync(_menu);
            await _endpointWriteRepository.SaveAsync();
        }

        
        //Buradaki menuyu oncelikli olarak veritabanina eklememiz gerekir cunku asagidaki kodda bunu kullaniyoruz

        Endpoint? endpoint = await _endpointReadRepostiory.Table.Include(e => e.Menu)
            .Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == endpointCode && e.Menu.Name == menu);

        if (endpoint == null)
        {
            var action = _applicationService.GetAuthorizeDefinitionEndpoints(type).FirstOrDefault(m => m.Name == menu)
                ?.Actions.FirstOrDefault(e => e.Code == endpointCode);

            endpoint = new()
            {
                Id = Guid.NewGuid(),
                Actiontype = action.ActionType,
                Code = action.Code,
                Definition = action.Definition,
                HttpType = action.HttpType,
                Menu = _menu
            };

            await _endpointWriteRepository.AddAsync(endpoint);
            await _endpointWriteRepository.SaveAsync();
        }

        foreach (var role in endpoint.Roles)
        {
            endpoint.Roles.Remove(role);
        }
        
        var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();
        
        foreach (var role in appRoles)
        {
            endpoint.Roles.Add(role);
        }
        
        await _endpointWriteRepository.SaveAsync();
        
        //Ilk olarak controllere gittik, kontrolunu tamamlandiktan sonra actionlarina gittik daha sonra gondermis oldugumuz
        //endpoint ile esleseni aliyoruz daha sonra rol atamasini gerceklestiricez daha sonra bunu veritabanina kaydediyoruz
    }

    //Gelen endpointCode a karsilik bir endpointimiz var mi yok mu  onu kontrol ediyoruz. Eger yoksa bu koda karsilik
    //gelen endpoint hangisiyse reflection sayesinde onu bulup veritabanina eklicez daha sonra bunu rollerle iliskilendiricez.
    
    public async Task<List<string>> GetRolesToEndpointAsync(string code, string menu)
    {
        Endpoint? endpoint = await _endpointReadRepostiory.Table
            .Include(e => e.Roles)
            .Include(e => e.Menu)
            .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

        if (endpoint != null)
        {
            return endpoint.Roles.Select(r => r.Name).ToList();
        }
        else
        {
            return null;
        }
        
    }

   //menu olarak menunun isminden bahsediyoruz
}