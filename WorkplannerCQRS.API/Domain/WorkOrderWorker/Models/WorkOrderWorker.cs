using System;

namespace WorkplannerCQRS.API.Domain.WorkOrderWorker.Models
{
    public class WorkOrderWorker
    {
        public string ObjectNumber { get; set; }

        public WorkOrder.Models.WorkOrder WorkOrder { get; set; }

        public int WorkerId { get; set; }

        public Worker.Models.Worker Worker { get; set; }

        public int HoursWorked { get; set; }

        public DateTime DateTime { get; set; }
    }
}