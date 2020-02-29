using MediatR;
using WorkplannerCQRS.API.Domain.WorkOrder.DTOs;

namespace WorkplannerCQRS.API.Domain.WorkOrder.Queries
{
    public class GetWorkOrderByIdQuery : IRequest<ApiResponse<WorkOrderResponse>>
    {
        public string ObjectNumber { get; set; }

        public GetWorkOrderByIdQuery(string objectNumber)
        {
            ObjectNumber = objectNumber;
        }
    }
}