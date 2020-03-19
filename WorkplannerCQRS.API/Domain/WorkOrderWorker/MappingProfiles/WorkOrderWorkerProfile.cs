using AutoMapper;
using WorkplannerCQRS.API.Domain.WorkOrder.Commands;

namespace WorkplannerCQRS.API.Domain.WorkOrderWorker.MappingProfiles
{
    public class WorkOrderWorkerProfile : Profile
    {
        public WorkOrderWorkerProfile()
        {
            CreateMap<AddWorkHoursCommand, Models.WorkOrderWorker>()
                .ForMember(x => x.Worker, opt => opt.Ignore())
                .ForMember(x => x.WorkOrder, opt => opt.Ignore());
        }
    }
}