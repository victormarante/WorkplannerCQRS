using System;
using WorkplannerCQRS.API.Domain.WorkOrder.Enums;

namespace WorkplannerCQRS.API.Domain.WorkOrder.Models
{
    public class WorkOrder : BaseEntity
    {
        public string ObjectNumber { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartDate { get; set; }
        
        public DateTimeOffset? EndDate { get; set; }

        public WorkOrderStatus Status { get; set; }
    }
}