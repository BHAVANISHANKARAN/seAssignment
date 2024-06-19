using MediatR;
using RL.Backend.Models;
using RL.Data.DataModels;
using RL.Data;

namespace RL.Backend.Commands.Handlers.Users;

public class UserPlanProcedureCommandHandler : IRequestHandler<UserPlanProcedureCommand, ApiResponse<Unit>>
{
    private readonly RLContext _context;

    public UserPlanProcedureCommandHandler(RLContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<Unit>> Handle(UserPlanProcedureCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingEntries = _context.UserPlanProcedures
        .Where(up => up.PlanId == request.PlanId && up.ProcedureId == request.ProcedureId)
        .ToList();

            _context.UserPlanProcedures.RemoveRange(existingEntries);

            var userPlanProcedures = request?.UserIds?.Select(userId => new UserPlanProcedure
            {
                UserId = userId,
                PlanId = request.PlanId,
                ProcedureId = request.ProcedureId,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            }).ToList();

            await _context.UserPlanProcedures.AddRangeAsync(userPlanProcedures, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return ApiResponse<Unit>.Succeed(Unit.Value);
        }
        catch (Exception ex)
        {
            return ApiResponse<Unit>.Fail(ex);
        }
    }
}