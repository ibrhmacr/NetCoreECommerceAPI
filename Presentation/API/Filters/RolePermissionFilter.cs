using System.Reflection;
using Application.Abstractions.Services;
using Application.CustomAttribute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.Filters;

public class RolePermissionFilter : IAsyncActionFilter
{
    private readonly IUserService _userService;

    public RolePermissionFilter(IUserService userService)
    {
        _userService = userService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //Ilgili kullanicinin username bilgisi gelicek nerden gelicek TokenHandler classinda Claimtype bilgisine karsilik
        //username i vermistik bu ozelligi de burda kullaniyor olucaz.
        var name = context.HttpContext.User.Identity?.Name;
        if (!string.IsNullOrEmpty(name) && name != "pakizee")//Dolayli olarak bu kontrollere admini vermemis oluyoruz
        {                                           
            //ActionDescriptor bize yuzeysel bilgilere ulasmamizi saglayabilir(parametreler route bilgiler vs) fakat
            //ActionName ine ulasamiyoruz, ControllerAction descriptor olarak verdigimizde Actionname e ulasabiliyoruz.
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            //MethodInfo sayesinde Custom hazirlamis oldugumuz Attribute ile isaretlenmis olanlari aliyoruz.
            var attribute = descriptor.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
            //Attirubute olarak degil AuthoizeDefinitionAttributein ozelliklerini kullanmak istedigimizden dolayi AuthorizeDefinitionAttribute olarak bildiriyoruz


            var httpMethodAttribute = descriptor.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;
            
            //Attribute unu vermedigimiz Actionun typei default olarak get olarak tutulur bunu belirtiyoruz
            var code =
                $"{(httpMethodAttribute != null ? httpMethodAttribute.HttpMethods.First() : HttpMethods.Get)}" +
                $".{attribute.ActionType}" +
                $".{attribute.Definition.Replace(" ", "")}";
            
            //Gerekli dogrulamayi User serviste Sagliyoruz
            var hasRole = await _userService.HasRolePermissionToEndpointAsync(name, code);
            
            //Servisten gelen sonucu kontrol ediyoruz
            if (!hasRole)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                await next();
            }
        }
        else
            await next();
    }
}