using System;
using MediatR;
using WorkplannerCQRS.API.Domain.WorkOrder.DTOs;

namespace WorkplannerCQRS.API.Domain.WorkOrder.Commands
{
    public class DeleteWorkOrderCommand : IRequest<ApiResponse<WorkOrderResponse>>
    {
        public string ObjectNumber { get; set; }
        
        public DeleteWorkOrderCommand(string objectNumber)
        {
            ObjectNumber = objectNumber;
        }
    }
}