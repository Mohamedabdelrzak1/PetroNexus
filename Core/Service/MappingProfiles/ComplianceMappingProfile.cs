using AutoMapper;
using Domain.Models;
using Shared.Dto.RegistrationDocuments;
using Shared.Dto.VendorRegistrations;

namespace Service.MappingProfiles
{
    public class ComplianceMappingProfile : Profile
    {
        public ComplianceMappingProfile()
        {
            // ==========================================
            // COMPLIANCE
            // ==========================================
            CreateMap<VendorRegistrationCreateDto, VendorRegistration>();
            CreateMap<VendorRegistrationUpdateDto, VendorRegistration>();
            CreateMap<VendorRegistration, VendorRegistrationResponseDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.Name : string.Empty));
            CreateMap<VendorRegistration, VendorRegistrationDetailsDto>()
                .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Documents));

            CreateMap<RegistrationDocumentCreateDto, RegistrationDocument>();
            CreateMap<RegistrationDocumentUpdateDto, RegistrationDocument>();
            CreateMap<RegistrationDocument, Shared.Dto.RegistrationDocuments.RegistrationDocumentResponseDto>();
        }
    }
}