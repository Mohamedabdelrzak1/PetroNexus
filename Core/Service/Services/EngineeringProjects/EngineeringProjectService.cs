using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IEngineeringProjects;
using Shared.Dto.EngineeringProjects;

namespace Service.Services.EngineeringProjects
{
    public class EngineeringProjectService : BaseService<EngineeringProject, int, EngineeringProjectResponseDto, EngineeringProjectCreateDto, EngineeringProjectUpdateDto>, IEngineeringProjectService
    {
        public EngineeringProjectService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<EngineeringProjectCreateDto> createValidator, IValidator<EngineeringProjectUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
