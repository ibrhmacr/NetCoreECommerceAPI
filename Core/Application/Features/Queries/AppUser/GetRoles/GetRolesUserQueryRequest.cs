using MediatR;

namespace Application.Features.Queries.AppUser.GetRoles;

public class GetRolesUserQueryRequest : IRequest<GetRolesUserQueryResponse>
{
    public string UserId { get; set; }
}