using System;
using MediatR;
using WorkplannerCQRS.API.Domain.WorkOrder.DTOs;
using WorkplannerCQRS.API.Domain.WorkOrder.Enums;

namespace WorkplannerCQRS.API.Domain.WorkOrder.Commands
{
    public class CreateWorkOrderCommand : IRequest<ApiResponse<WorkOrderResponse>>
    {
        public string ObjectNumber { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
        
        public DateTimeOffset? StartDate { get; set; }
        
        public DateTimeOffset? EndDate { get; set; }

        public WorkOrderStatus Status { get; set; }
        
        // Needed for fluent validation
        public CreateWorkOrderCommand()
        {
        }

        public CreateWorkOrderCommand(
            string objectNumber, 
            string address, 
            string description,
            DateTimeOffset? startDate,
            DateTimeOffset? endDate,
            WorkOrderStatus status)
        {
            ObjectNumber = objectNumber;
            Address = address;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }
    }
}