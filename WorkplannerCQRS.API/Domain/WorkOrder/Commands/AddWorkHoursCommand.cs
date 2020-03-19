using System;
using MediatR;

namespace WorkplannerCQRS.API.Domain.WorkOrder.Commands
{
    public class AddWorkHoursCommand : IRequest
    {
        public string ObjectNumber { get; set; }

        public int WorkerId { get; set; }

        public int HoursWorked { get; set; }

        public DateTime DateTime { get; set; }
    }
}