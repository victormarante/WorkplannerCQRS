using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WorkplannerCQRS.API.Data;
using WorkplannerCQRS.API.Domain.Worker.DTOs;

namespace WorkplannerCQRS.API.Domain.Worker.Commands
{
    public class DeleteWorkerHandler : IRequestHandler<DeleteWorkerCommand, ApiResponse<WorkerResponse>>
    {
        private readonly WorkplannerDbContext _db;
        private readonly IMapper _mapper;
        
        public DeleteWorkerHandler(WorkplannerDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<ApiResponse<WorkerResponse>> Handle(DeleteWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await _db.FindAsync<Models.Worker>(request.WorkerId);
            if (worker == null)
            {
                return new ApiResponse<WorkerResponse>($"Could not find work order with Object number: {request.WorkerId}");
            }
            
            _db.Remove(worker);
            await _db.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<WorkerResponse>(worker);
            return new ApiResponse<WorkerResponse>(response);
        }
    }
}