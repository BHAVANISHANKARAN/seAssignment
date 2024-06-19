using MediatR;
using RL.Backend.Models;
using RL.Data.DataModels;
using RL.Data;
using RL.Backend.Commands.Queries;
using Microsoft.EntityFrameworkCore;

namespace RL.Backend.Commands.Handlers.Users;

public class GetUsersByPlanProcedureQueryHandler : IRequestHandler<GetUsersPlanProcedureQuery, ApiResponse<List<User>>>
{
    private readonly RLContext _context;

    public GetUsersByPlanProcedureQueryHandler(RLContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<List<User>>> Handle(GetUsersPlanProcedureQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _context.UserPlanProcedures
                .Where(upp => upp.PlanId == request.PlanId && upp.ProcedureId == request.ProcedureId)
                .Select(upp => upp.User)
                .ToListAsync(cancellationToken);

            return ApiResponse<List<User>>.Succeed(users);
        }
        catch (Exception ex)
        {
            return ApiResponse<List<User>>.Fail(ex);
        }
    }
}
