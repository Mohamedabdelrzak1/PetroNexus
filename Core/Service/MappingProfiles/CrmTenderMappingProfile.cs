using AutoMapper;
using Domain.Models;
using Shared.Dto.Clients;
using Shared.Dto.Quotations;
using Shared.Dto.Tenders;

namespace Service.MappingProfiles
{
    public class CrmTenderMappingProfile : Profile
    {
        public CrmTenderMappingProfile()
        {
            // ==========================================
            // CRM & TENDERS
            // ==========================================
            CreateMap<ClientCreateDto, Client>();
            CreateMap<ClientUpdateDto, Client>();
            CreateMap<Client, ClientResponseDto>();
            CreateMap<Client, ClientDetailsDto>()
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src.Contacts))
                .ForMember(dest => dest.Tenders, opt => opt.MapFrom(src => src.Tenders));
            CreateMap<Client, ClientSummaryDto>();
            CreateMap<Client, ClientLookupDto>();

            CreateMap<ClientContactCreateDto, ClientContact>();
            CreateMap<ClientContactUpdateDto, ClientContact>();
            CreateMap<ClientContact, ClientContactResponseDto>();

            CreateMap<ClientInteractionCreateDto, ClientInteraction>();
            CreateMap<ClientInteractionUpdateDto, ClientInteraction>();
            CreateMap<ClientInteraction, ClientInteractionResponseDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.Name : string.Empty))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.CreatedByUser != null ? src.CreatedByUser.DisplayName : string.Empty));

            CreateMap<ClientPortalUserCreateDto, ClientPortalUser>();
            CreateMap<ClientPortalUserUpdateDto, ClientPortalUser>();
            CreateMap<ClientPortalUser, ClientPortalUserResponseDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.Name : string.Empty));

            CreateMap<TenderCreateDto, Tender>();
            CreateMap<TenderUpdateDto, Tender>();
            CreateMap<Tender, TenderResponseDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.Name : string.Empty));
            CreateMap<Tender, TenderDetailsDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.DocumentAnalysis, opt => opt.MapFrom(src => src.DocumentAnalysis));

            CreateMap<TenderItemCreateDto, TenderItem>();
            CreateMap<TenderItemUpdateDto, TenderItem>();
            CreateMap<TenderItem, TenderItemResponseDto>()
                .ForMember(dest => dest.PrincipalProductName, opt => opt.MapFrom(src => src.PrincipalProduct != null ? src.PrincipalProduct.Name : string.Empty));

            CreateMap<TenderLeadCreateDto, TenderLead>();
            CreateMap<TenderLeadUpdateDto, TenderLead>();
            CreateMap<TenderLead, TenderLeadResponseDto>();

            CreateMap<TenderDocumentAnalysisCreateDto, TenderDocumentAnalysis>();
            CreateMap<TenderDocumentAnalysisUpdateDto, TenderDocumentAnalysis>();
            CreateMap<TenderDocumentAnalysis, TenderDocumentAnalysisResponseDto>()
                .ForMember(dest => dest.SuggestedPrincipalName, opt => opt.MapFrom(src => src.SuggestedPrincipalId != null ? "Principal" : string.Empty));

            CreateMap<QuotationCreateDto, Quotation>();
            CreateMap<QuotationUpdateDto, Quotation>();
            CreateMap<Quotation, QuotationResponseDto>()
                .ForMember(dest => dest.TenderTitle, opt => opt.MapFrom(src => src.Tender != null ? src.Tender.Title : string.Empty))
                .ForMember(dest => dest.PrincipalName, opt => opt.MapFrom(src => src.Principal != null ? src.Principal.Name : string.Empty));
            CreateMap<Quotation, QuotationDetailsDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<QuotationItemCreateDto, QuotationItem>();
            CreateMap<QuotationItemUpdateDto, QuotationItem>();
            CreateMap<QuotationItem, QuotationItemResponseDto>();
        }
    }
}