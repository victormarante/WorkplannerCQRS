using MediatR;
using WorkplannerCQRS.API.Domain.Worker.DTOs;

namespace WorkplannerCQRS.API.Domain.Worker.Commands
{
    public class DeleteWorkerCommand : IRequest<ApiResponse<WorkerResponse>>
    {
        public DeleteWorkerCommand(int id)
        {
            WorkerId = id;
        }

        public int WorkerId { get; set; }
    }
}