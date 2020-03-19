using System.Collections.Generic;

namespace WorkplannerCQRS.API.Domain.Worker.Models
{
    public class Worker : BaseEntity
    {
        public int WorkerId { get; set; }
        
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        
        public ICollection<WorkOrderWorker.Models.WorkOrderWorker> WorkOrderWorkers { get; set; }
    }
}