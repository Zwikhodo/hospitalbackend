using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HospitalSystem.Services.EmployeeLogs.DTO;
using System;
using System.Threading.Tasks;

namespace HospitalSystem.Services.EmployeeLogs
{
    public interface IEmployeeLogAppService : IApplicationService
    {
        Task<EmployeeLogDTO> Create(EmployeeLogDTO input);
        Task<EmployeeLogDTO> Update(EmployeeLogDTO input);
        Task<PagedResultDto<EmployeeLogDTO>> GetAll(PagedAndSortedResultRequestDto input);
        Task<PagedResultDto<EmployeeLogDTO>> Get(PagedAndSortedResultRequestDto input, Guid id);
        void DeleteAsync(Guid id);
    }
}
