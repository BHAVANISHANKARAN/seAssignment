using MediatR;
using RL.Backend.Models;
using RL.Data.DataModels;

namespace RL.Backend.Commands.Queries;

public class GetUsersPlanProcedureQuery : IRequest<ApiResponse<List<User>>>
{
    public int PlanId { get; set; }
    public int ProcedureId { get; set; }
}