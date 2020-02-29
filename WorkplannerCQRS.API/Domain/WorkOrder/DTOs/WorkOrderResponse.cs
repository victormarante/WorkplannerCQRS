using System;
using WorkplannerCQRS.API.Domain.WorkOrder.Enums;
using WorkplannerCQRS.API.Domain.WorkOrder.Models;

namespace WorkplannerCQRS.API.Domain.WorkOrder.DTOs
{
    public class WorkOrderResponse
    {
        public string ObjectNumber { get; set; }
        
        public string Address { get; set; }
        
        public string Description { get; set; }
        
        public DateTimeOffset? StartDate { get; set; }
        
        public DateTimeOffset? EndDate { get; set; }

        public WorkOrderStatus Status { get; set; }
    }
}