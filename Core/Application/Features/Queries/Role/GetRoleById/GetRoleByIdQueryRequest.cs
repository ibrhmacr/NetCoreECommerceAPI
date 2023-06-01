using Application.Features.Queries.Role.GetRoles;
using MediatR;

namespace Application.Features.Queries.Role.GetRoleById;

public class GetRoleByIdQueryRequest : IRequest<GetRoleByIdQueryResponse>
{
    public string Id { get; set; }
}