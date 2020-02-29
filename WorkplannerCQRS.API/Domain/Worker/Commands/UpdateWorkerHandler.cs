using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WorkplannerCQRS.API.Data;
using WorkplannerCQRS.API.Domain.Worker.DTOs;

namespace WorkplannerCQRS.API.Domain.Worker.Commands
{
    public class UpdateWorkerHandler : IRequestHandler<UpdateWorkerCommand, ApiResponse<WorkerResponse>>
    {
        private readonly WorkplannerDbContext _db;
        private readonly IMapper _mapper;

        public UpdateWorkerHandler(WorkplannerDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ApiResponse<WorkerResponse>> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await _db.FindAsync<Models.Worker>(request.WorkerId);
            if (worker == null)
            {
                return new ApiResponse<WorkerResponse>($"Could not find work order with ID: {request.WorkerId}");
            }
            
            worker = _mapper.Map<Models.Worker>(request);
            _db.Workers.Update(worker);
            await _db.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<WorkerResponse>(worker);
            return new ApiResponse<WorkerResponse>(response);
        }
    }
}