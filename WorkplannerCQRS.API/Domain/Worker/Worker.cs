using System;

namespace WorkplannerCQRS.API.Domain.Worker
{
    public class Worker : BaseEntity
    {
        public int WorkerId { get; set; }
        
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}