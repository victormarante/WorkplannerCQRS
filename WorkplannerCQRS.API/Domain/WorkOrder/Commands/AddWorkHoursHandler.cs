using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WorkplannerCQRS.API.Data;

namespace WorkplannerCQRS.API.Domain.WorkOrder.Commands
{
    public class AddWorkHoursHandler : IRequestHandler<AddWorkHoursCommand>
    {
        private readonly WorkplannerDbContext _db;
        private readonly IMapper _mapper;
        
        public AddWorkHoursHandler(WorkplannerDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<Unit> Handle(AddWorkHoursCommand request, CancellationToken cancellationToken)
        {
            var workOrderWorker = _mapper.Map<WorkOrderWorker.Models.WorkOrderWorker>(request);
            _db.Set<WorkOrderWorker.Models.WorkOrderWorker>().Add(workOrderWorker);
            await _db.SaveChangesAsync(cancellationToken);
            return await Unit.Task;
        }
    }
}