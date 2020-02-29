using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkplannerCQRS.API.Data;
using WorkplannerCQRS.API.Domain.Worker.DTOs;

namespace WorkplannerCQRS.API.Domain.Worker.Queries
{
    public class GetWorkerByIdHandler : IRequestHandler<GetWorkerByIdQuery, ApiResponse<WorkerResponse>>
    {
        private readonly WorkplannerDbContext _db;
        private readonly IMapper _mapper;

        public GetWorkerByIdHandler(WorkplannerDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<ApiResponse<WorkerResponse>> Handle(GetWorkerByIdQuery request, CancellationToken cancellationToken)
        {
            var worker = await _db.Workers
                .ProjectTo<WorkerResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.WorkerId == request.Id, cancellationToken);

            if (worker == null)
            {
                return new ApiResponse<WorkerResponse>($"Worker with Id: '{request.Id}' cannot be found");
            }
            
            return new ApiResponse<WorkerResponse>(worker);
        }
    }
}