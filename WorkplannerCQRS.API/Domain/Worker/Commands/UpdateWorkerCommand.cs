using MediatR;
using WorkplannerCQRS.API.Domain.Worker.DTOs;

namespace WorkplannerCQRS.API.Domain.Worker.Commands
{
    public class UpdateWorkerCommand : IRequest<ApiResponse<WorkerResponse>>
    {
        public UpdateWorkerCommand(int id, string name, string email, string phoneNumber)
        {
            WorkerId = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public int WorkerId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}