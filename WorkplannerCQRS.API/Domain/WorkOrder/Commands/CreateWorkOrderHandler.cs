using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WorkplannerCQRS.API.Data;
using WorkplannerCQRS.API.Domain.WorkOrder.DTOs;

namespace WorkplannerCQRS.API.Domain.WorkOrder.Commands
{
    public class CreateWorkOrderHandler : IRequestHandler<CreateWorkOrderCommand, ApiResponse<WorkOrderResponse>>
    {
        private readonly WorkplannerDbContext _db;
        private readonly IMapper _mapper;

        public CreateWorkOrderHandler(WorkplannerDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ApiResponse<WorkOrderResponse>> Handle(CreateWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var workOrder = _mapper.Map<Models.WorkOrder>(request);
            var result = _db.WorkOrders.Add(workOrder);
            await _db.SaveChangesAsync(cancellationToken);
            var response = _mapper.Map<WorkOrderResponse>(result.Entity);
            return new ApiResponse<WorkOrderResponse>(response);
        }
    }
}