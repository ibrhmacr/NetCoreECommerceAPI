using MediatR;

namespace Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;

public class AssignRoleEndpointCommandRequest : IRequest<AssignRoleEndpointCommandResponse>
{
    public string[] Roles { get; set; }

    public string EndpointCode { get; set; }

    public string Menu { get; set; }

    public Type? Type { get; set; } //Type olarak API nun type ni controllerdan vericez
                                    //Type kismini requestte nullable olarak isaretlememiz gerekiyor cunku clienttan degil
                                    //type i kendimiz veriyoruz
}