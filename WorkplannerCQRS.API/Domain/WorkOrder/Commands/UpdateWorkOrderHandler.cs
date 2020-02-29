using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WorkplannerCQRS.API.Data;
using WorkplannerCQRS.API.Domain.WorkOrder.DTOs;

namespace WorkplannerCQRS.API.Domain.WorkOrder.Commands
{
    public class UpdateWorkOrderHandler : IRequestHandler<UpdateWorkOrderCommand, ApiResponse<WorkOrderResponse>>
    {
        private readonly WorkplannerDbContext _db;
        private readonly IMapper _mapper;

        public UpdateWorkOrderHandler(WorkplannerDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ApiResponse<WorkOrderResponse>> Handle(UpdateWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var workOrder = await _db.FindAsync<Models.WorkOrder>(request.ObjectNumber);
            if (workOrder == null)
            {
                return new ApiResponse<WorkOrderResponse>($"Could not find work order with ID: {request.ObjectNumber}");
            }
            
            workOrder = _mapper.Map<Models.WorkOrder>(request);
            _db.WorkOrders.Update(workOrder);
            await _db.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<WorkOrderResponse>(workOrder);
            return new ApiResponse<WorkOrderResponse>(response);
        }
    }
}