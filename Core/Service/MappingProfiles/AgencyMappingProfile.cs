using AutoMapper;
using Domain.Models;
using Shared.Dto.Commissions;
using Shared.Dto.Principals;

namespace Service.MappingProfiles
{
    public class AgencyMappingProfile : Profile
    {
        public AgencyMappingProfile()
        {
            // ==========================================
            // AGENCY
            // ==========================================
            CreateMap<PrincipalCreateDto, Principal>();
            CreateMap<PrincipalUpdateDto, Principal>();
            CreateMap<Principal, PrincipalResponseDto>();
            CreateMap<Principal, PrincipalDetailsDto>()
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src.Contacts))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                .ForMember(dest => dest.PerformanceReviews, opt => opt.MapFrom(src => src.PerformanceReviews))
                .ForMember(dest => dest.Commissions, opt => opt.MapFrom(src => src.Commissions));

            CreateMap<PrincipalContactCreateDto, PrincipalContact>();
            CreateMap<PrincipalContactUpdateDto, PrincipalContact>();
            CreateMap<PrincipalContact, PrincipalContactResponseDto>();

            CreateMap<PrincipalProductCreateDto, PrincipalProduct>();
            CreateMap<PrincipalProductUpdateDto, PrincipalProduct>();
            CreateMap<PrincipalProduct, PrincipalProductResponseDto>()
                .ForMember(dest => dest.PrincipalName, opt => opt.MapFrom(src => src.Principal != null ? src.Principal.Name : string.Empty));

            CreateMap<PrincipalPerformanceReviewCreateDto, PrincipalPerformanceReview>();
            CreateMap<PrincipalPerformanceReviewUpdateDto, PrincipalPerformanceReview>();
            CreateMap<PrincipalPerformanceReview, PrincipalPerformanceReviewResponseDto>()
                .ForMember(dest => dest.PrincipalName, opt => opt.MapFrom(src => src.Principal != null ? src.Principal.Name : string.Empty));

            CreateMap<CommissionCreateDto, Commission>();
            CreateMap<CommissionUpdateDto, Commission>();
            CreateMap<Commission, CommissionResponseDto>()
                .ForMember(dest => dest.PrincipalName, opt => opt.MapFrom(src => src.Principal != null ? src.Principal.Name : string.Empty))
                .ForMember(dest => dest.PurchaseOrderNumber, opt => opt.MapFrom(src => src.PurchaseOrder != null ? src.PurchaseOrder.PoNumber : string.Empty));
        }
    }
}