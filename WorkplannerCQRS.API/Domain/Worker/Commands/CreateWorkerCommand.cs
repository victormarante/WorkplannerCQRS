using MediatR;
using WorkplannerCQRS.API.Domain.Worker.DTOs;

namespace WorkplannerCQRS.API.Domain.Worker.Commands
{
    public class CreateWorkerCommand : IRequest<ApiResponse<WorkerResponse>>
    {
        // Needed for fluent validation tests
        public CreateWorkerCommand()
        {
        }
        
        public CreateWorkerCommand(string name, string email, string phoneNumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Name of the worker
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Email address of the worker
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone number of the worker
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}