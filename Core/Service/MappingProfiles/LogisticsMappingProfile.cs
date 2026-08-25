using AutoMapper;
using Domain.Models;
using Shared.Dto.LettersOfCredit;
using Shared.Dto.LiquidatedDamages;
using Shared.Dto.PurchaseOrders;
using Shared.Dto.Shipments;

namespace Service.MappingProfiles
{
    public class LogisticsMappingProfile : Profile
    {
        public LogisticsMappingProfile()
        {
            // ==========================================
            // LOGISTICS
            // ==========================================
            CreateMap<PurchaseOrderCreateDto, PurchaseOrder>();
            CreateMap<PurchaseOrderUpdateDto, PurchaseOrder>();
            CreateMap<PurchaseOrder, PurchaseOrderResponseDto>()
                .ForMember(dest => dest.PrincipalName, opt => opt.MapFrom(src => src.Principal != null ? src.Principal.Name : string.Empty))
                .ForMember(dest => dest.TenderTitle, opt => opt.MapFrom(src => src.Tender != null ? src.Tender.Title : string.Empty));
            CreateMap<PurchaseOrder, PurchaseOrderDetailsDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.Shipments, opt => opt.MapFrom(src => src.Shipments));

            CreateMap<PurchaseOrderItemCreateDto, PurchaseOrderItem>();
            CreateMap<PurchaseOrderItemUpdateDto, PurchaseOrderItem>();
            CreateMap<PurchaseOrderItem, PurchaseOrderItemResponseDto>()
                .ForMember(dest => dest.PrincipalProductName, opt => opt.MapFrom(src => src.PrincipalProduct != null ? src.PrincipalProduct.Name : string.Empty));

            CreateMap<LetterOfCreditCreateDto, LetterOfCredit>();
            CreateMap<LetterOfCreditUpdateDto, LetterOfCredit>();
            CreateMap<LetterOfCredit, LetterOfCreditResponseDto>();

            CreateMap<ShipmentCreateDto, Shipment>();
            CreateMap<ShipmentUpdateDto, Shipment>();
            CreateMap<Shipment, ShipmentResponseDto>()
                .ForMember(dest => dest.PoNumber, opt => opt.MapFrom(src => src.PurchaseOrder != null ? src.PurchaseOrder.PoNumber : string.Empty));
            CreateMap<Shipment, ShipmentDetailsDto>()
                .ForMember(dest => dest.TrackingEvents, opt => opt.MapFrom(src => src.TrackingEvents))
                .ForMember(dest => dest.LiquidatedDamages, opt => opt.MapFrom(src => src.LiquidatedDamages));

            CreateMap<ShipmentTrackingEventCreateDto, ShipmentTrackingEvent>();
            CreateMap<ShipmentTrackingEventUpdateDto, ShipmentTrackingEvent>();
            CreateMap<ShipmentTrackingEvent, ShipmentTrackingEventResponseDto>();

            CreateMap<LiquidatedDamageCreateDto, LiquidatedDamage>();
            CreateMap<LiquidatedDamageUpdateDto, LiquidatedDamage>();
            CreateMap<LiquidatedDamage, LiquidatedDamageResponseDto>();
        }
    }
}