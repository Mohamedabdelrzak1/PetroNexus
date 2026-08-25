
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IEmployees;
using Shared.Dto.Employees;


namespace Service.Services.Employees
{
    public class EmployeeDocumentService : BaseService<EmployeeDocument, int, EmployeeDocumentResponseDto, EmployeeDocumentCreateDto, EmployeeDocumentUpdateDto>, IEmployeeDocumentService
    {
        public EmployeeDocumentService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<EmployeeDocumentCreateDto> createValidator, IValidator<EmployeeDocumentUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
