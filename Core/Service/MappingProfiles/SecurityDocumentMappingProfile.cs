using AutoMapper;
using Domain.Models;
using Shared.Dto.Auth;
using Shared.Dto.DocumentRecords;
using Shared.Dto.DocumentSignatures;
using Shared.Dto.RolePermissions;
using Shared.Dto.Users;

namespace Service.MappingProfiles
{
    public class SecurityDocumentMappingProfile : Profile
    {
        public SecurityDocumentMappingProfile()
        {
            // ==========================================
            // SECURITY & DOCUMENTS
            // ==========================================
            CreateMap<PermissionCreateDto, Permission>();
            CreateMap<PermissionUpdateDto, Permission>();
            CreateMap<Permission, PermissionResponseDto>();

            CreateMap<RolePermissionCreateDto, RolePermission>();
            CreateMap<RolePermissionUpdateDto, RolePermission>();
            CreateMap<RolePermission, RolePermissionResponseDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : string.Empty))
                .ForMember(dest => dest.PermissionCode, opt => opt.MapFrom(src => src.Permission != null ? src.Permission.Code : string.Empty));

            CreateMap<RefreshTokenCreateDto, RefreshToken>();
            CreateMap<RefreshTokenUpdateDto, RefreshToken>();
            CreateMap<RefreshToken, RefreshTokenResponseDto>();

            CreateMap<DocumentRecordCreateDto, DocumentRecord>();
            CreateMap<DocumentRecordUpdateDto, DocumentRecord>();
            CreateMap<DocumentRecord, DocumentRecordResponseDto>();
            CreateMap<DocumentRecord, DocumentRecordDetailsDto>()
                .ForMember(dest => dest.Signatures, opt => opt.MapFrom(src => src.Signatures));

            CreateMap<DocumentSignatureCreateDto, DocumentSignature>();
            CreateMap<DocumentSignatureUpdateDto, DocumentSignature>();
            CreateMap<DocumentSignature, DocumentSignatureResponseDto>()
                .ForMember(dest => dest.SignerUserName, opt => opt.MapFrom(src => src.SignerUser != null ? src.SignerUser.UserName : string.Empty));


            
        }
    }
}