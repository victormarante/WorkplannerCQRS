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
    public class GetWorkOrderByIdHandler : IRequestHandler<GetWorkOrderByIdQuery, ApiResponse<WorkOrderResponse>>
    {
        private readonly WorkplannerDbContext _db;
        private readonly IMapper _mapper;
        
        public GetWorkOrderByIdHandler(WorkplannerDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<ApiResponse<WorkOrderResponse>> Handle(GetWorkOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var workOrder = await _db.WorkOrders
                .ProjectTo<WorkOrderResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.ObjectNumber == request.ObjectNumber, cancellationToken);

            if (workOrder == null)
            {
                return new ApiResponse<WorkOrderResponse>($"Work order with id: '{request.ObjectNumber}' could not by found");
            }
            
            return new ApiResponse<WorkOrderResponse>(workOrder);
        }
    }
}