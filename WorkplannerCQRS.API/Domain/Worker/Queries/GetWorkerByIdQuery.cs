using System;
using MediatR;
using WorkplannerCQRS.API.Domain.Worker.DTOs;

namespace WorkplannerCQRS.API.Domain.Worker.Queries
{
    public class GetWorkerByIdQuery : IRequest<ApiResponse<WorkerResponse>>
    {
        public GetWorkerByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}