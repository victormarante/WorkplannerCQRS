using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WorkplannerCQRS.API.Data;
using WorkplannerCQRS.API.Domain.Worker.DTOs;

namespace WorkplannerCQRS.API.Domain.Worker.Commands
{
    public class CreateWorkerHandler : IRequestHandler<CreateWorkerCommand, ApiResponse<WorkerResponse>>
    {
        private readonly WorkplannerDbContext _db;
        private readonly IMapper _mapper;
        
        public CreateWorkerHandler(WorkplannerDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<ApiResponse<WorkerResponse>> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = _mapper.Map<Worker>(request);
            _db.Set<Worker>().Add(worker);
            await _db.SaveChangesAsync(cancellationToken);
            var response = _mapper.Map<WorkerResponse>(worker);
            return new ApiResponse<WorkerResponse>(response);
        }
    }
}