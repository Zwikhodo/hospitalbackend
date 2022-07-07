using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HospitalSystem.Services.Employees.DTO;
using System;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Employees
{
    public interface IEmployeeAppService: IApplicationService
    {
        Task<EmployeeDTO> Create(EmployeeDTO input);
        Task<EmployeeDTO> Update(EmployeeDTO input);
        Task<PagedResultDto<EmployeeDTO>> GetAll(PagedAndSortedResultRequestDto input);
        Task<PagedResultDto<EmployeeDTO>> GetById(PagedAndSortedResultRequestDto input, Guid id);
        void DeleteAsync(Guid id);
    }
}
