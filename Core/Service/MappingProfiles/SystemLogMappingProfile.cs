using AutoMapper;
using Domain.Models;
using Shared.Dto.AuditLogs;
using Shared.Dto.EscalationLogs;
using Shared.Dto.Notifications;

namespace Service.MappingProfiles
{
    public class SystemLogMappingProfile : Profile
    {
        public SystemLogMappingProfile()
        {
            // ==========================================
            // SYSTEM LOGS & NOTIFICATIONS
            // ==========================================
            CreateMap<NotificationCreateDto, Notification>();
            CreateMap<NotificationUpdateDto, Notification>();
            CreateMap<Notification, NotificationResponseDto>();

            CreateMap<EscalationLogCreateDto, EscalationLog>();
            CreateMap<EscalationLogUpdateDto, EscalationLog>();
            CreateMap<EscalationLog, EscalationLogResponseDto>();

            CreateMap<AuditLogCreateDto, AuditLog>();
            CreateMap<AuditLog, AuditLogResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : string.Empty));
        }
    }
}