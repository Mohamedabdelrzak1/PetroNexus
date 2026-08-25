using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IEngineeringProjects;
using Shared.Dto.EngineeringProjects;

namespace Service.Services.EngineeringProjects
{
    public class EngineeringDeliverableService : BaseService<EngineeringDeliverable, int, EngineeringDeliverableResponseDto, EngineeringDeliverableCreateDto, EngineeringDeliverableUpdateDto>, IEngineeringDeliverableService
    {
        public EngineeringDeliverableService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<EngineeringDeliverableCreateDto> createValidator, IValidator<EngineeringDeliverableUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
