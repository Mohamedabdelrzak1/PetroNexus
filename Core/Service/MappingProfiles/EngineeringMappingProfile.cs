using AutoMapper;
using Domain.Models;
using Shared.Dto.EngineeringProjects;

namespace Service.MappingProfiles
{
    public class EngineeringMappingProfile : Profile
    {
        public EngineeringMappingProfile()
        {
            // ==========================================
            // ENGINEERING
            // ==========================================
            CreateMap<EngineeringProjectCreateDto, EngineeringProject>();
            CreateMap<EngineeringProjectUpdateDto, EngineeringProject>();
            CreateMap<EngineeringProject, EngineeringProjectResponseDto>()
                .ForMember(dest => dest.TenderTitle, opt => opt.MapFrom(src => src.Tender != null ? src.Tender.Title : string.Empty));
            CreateMap<EngineeringProject, EngineeringProjectDetailsDto>()
                .ForMember(dest => dest.Deliverables, opt => opt.MapFrom(src => src.Deliverables));

            CreateMap<EngineeringDeliverableCreateDto, EngineeringDeliverable>();
            CreateMap<EngineeringDeliverableUpdateDto, EngineeringDeliverable>();
            CreateMap<EngineeringDeliverable, EngineeringDeliverableResponseDto>()
                .ForMember(dest => dest.EngineeringProjectTitle, opt => opt.MapFrom(src => src.EngineeringProject != null ? src.EngineeringProject.Title : string.Empty));
        }
    }
}