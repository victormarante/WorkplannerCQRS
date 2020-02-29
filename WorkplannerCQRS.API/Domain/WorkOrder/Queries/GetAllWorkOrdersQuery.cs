using System.Collections.Generic;
using MediatR;
using WorkplannerCQRS.API.Domain.WorkOrder.DTOs;

namespace WorkplannerCQRS.API.Domain.WorkOrder.Queries
{
    public class GetAllWorkOrdersQuery : IRequest<ApiResponse<List<WorkOrderResponse>>>
    {
    }
}