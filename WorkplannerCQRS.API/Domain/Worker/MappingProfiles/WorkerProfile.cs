using AutoMapper;
using WorkplannerCQRS.API.Domain.Worker.Commands;
using WorkplannerCQRS.API.Domain.Worker.DTOs;

namespace WorkplannerCQRS.API.Domain.Worker.MappingProfiles
{
    public class WorkerProfile : Profile
    {
        public WorkerProfile()
        {
            CreateMap<CreateWorkerCommand, Models.Worker>()
                .ForMember(x => x.WorkerId, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.ModifiedAt, opt => opt.Ignore());
            
            CreateMap<UpdateWorkerCommand, Models.Worker>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.ModifiedAt, opt => opt.Ignore());
            
            CreateMap<Models.Worker, WorkerResponse>()
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.CreatedAt, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.ModifiedAt, opt => opt.DoNotValidate())
                .ReverseMap();
        }
    }
}