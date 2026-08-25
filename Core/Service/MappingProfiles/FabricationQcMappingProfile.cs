using AutoMapper;
using Domain.Models;
using Shared.Dto.FabricationOrders;
using Shared.Dto.NonConformanceReports;

namespace Service.MappingProfiles
{
    public class FabricationQcMappingProfile : Profile
    {
        public FabricationQcMappingProfile()
        {
            // ==========================================
            // FABRICATION & QC
            // ==========================================
            CreateMap<FabricationOrderCreateDto, FabricationOrder>();
            CreateMap<FabricationOrderUpdateDto, FabricationOrder>();
            CreateMap<FabricationOrder, FabricationOrderResponseDto>()
                .ForMember(dest => dest.PoNumber, opt => opt.MapFrom(src => src.PurchaseOrder != null ? src.PurchaseOrder.PoNumber : string.Empty));
            CreateMap<FabricationOrder, FabricationOrderDetailsDto>()
                .ForMember(dest => dest.Inspections, opt => opt.MapFrom(src => src.Inspections));

            CreateMap<QualityInspectionCreateDto, QualityInspection>();
            CreateMap<QualityInspectionUpdateDto, QualityInspection>();
            CreateMap<QualityInspection, QualityInspectionResponseDto>();
            CreateMap<QualityInspection, QualityInspectionDetailsDto>()
                .ForMember(dest => dest.NonConformanceReports, opt => opt.MapFrom(src => src.NonConformanceReports));

            CreateMap<NonConformanceReportCreateDto, NonConformanceReport>();
            CreateMap<NonConformanceReportUpdateDto, NonConformanceReport>();
            CreateMap<NonConformanceReport, NonConformanceReportResponseDto>();
        }
    }
}