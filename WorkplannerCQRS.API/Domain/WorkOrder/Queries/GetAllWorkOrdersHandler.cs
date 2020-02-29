using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkplannerCQRS.API.Data;
using WorkplannerCQRS.API.Domain.WorkOrder.DTOs;

namespace WorkplannerCQRS.API.Domain.WorkOrder.Queries
{
    public class GetAllWorkOrdersHandler : IRequestHandler<GetAllWorkOrdersQuery, ApiResponse<List<WorkOrderResponse>>>
    {
        private readonly WorkplannerDbContext _db;
        private readonly IMapper _mapper;

        public GetAllWorkOrdersHandler(WorkplannerDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<WorkOrderResponse>>> Handle(GetAllWorkOrdersQuery request, CancellationToken cancellationToken)
        {
            var workOrders = await _db.WorkOrders
                .ProjectTo<WorkOrderResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var response = new ApiResponse<List<WorkOrderResponse>>(workOrders);
            return response;
        }
    }
}