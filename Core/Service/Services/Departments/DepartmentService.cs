using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IDepartments;
using Shared.Dto.Departments;


namespace Service.Services.Departments
{
    public class DepartmentService : BaseService<Department, int, DepartmentResponseDto, DepartmentCreateDto, DepartmentUpdateDto>, IDepartmentService
    {
        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<DepartmentCreateDto> createValidator, IValidator<DepartmentUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
