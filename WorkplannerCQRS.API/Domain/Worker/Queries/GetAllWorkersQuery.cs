using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkplannerCQRS.API.Data;
using WorkplannerCQRS.API.Domain.Worker.DTOs;
using WorkplannerCQRS.API.Domain.WorkOrder.DTOs;

namespace WorkplannerCQRS.API.Domain.Worker.Queries
{
    public class GetAllWorkersQuery : IRequest<ApiResponse<List<WorkerResponse>>>
    {
    }
    
    public class GetAllWorkersHandler : IRequestHandler<GetAllWorkersQuery, ApiResponse<List<WorkerResponse>>>
    {
        private readonly WorkplannerDbContext _db;
        private readonly IMapper _mapper;
        
        public GetAllWorkersHandler(WorkplannerDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<ApiResponse<List<WorkerResponse>>> Handle(GetAllWorkersQuery request, CancellationToken cancellationToken)
        {
            var workers = await _db.Workers
                .ProjectTo<WorkerResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ApiResponse<List<WorkerResponse>>(workers);
        }
    }
}