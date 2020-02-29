using AutoMapper;
using WorkplannerCQRS.API.Domain.WorkOrder.Commands;
using WorkplannerCQRS.API.Domain.WorkOrder.DTOs;

namespace WorkplannerCQRS.API.Domain.WorkOrder.MappingProfiles
{
    public class WorkOrderProfile : Profile 
    {
        public WorkOrderProfile()
        {
            CreateMap<CreateWorkOrderCommand, Models.WorkOrder>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.ModifiedAt, opt => opt.Ignore());
            
            CreateMap<UpdateWorkOrderCommand, Models.WorkOrder>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.ModifiedAt, opt => opt.Ignore());
            
            CreateMap<Models.WorkOrder, WorkOrderResponse>()
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.CreatedAt, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.ModifiedAt, opt => opt.DoNotValidate())
                .ReverseMap();
        }
    }
}