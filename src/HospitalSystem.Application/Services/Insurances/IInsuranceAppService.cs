using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HospitalSystem.Services.Insurances.DTO;
using System;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Insurances
{
    public interface IInsuranceAppService : IApplicationService
    {
        Task<InsuranceDTO> CreateAsync(InsuranceDTO input);
        Task<InsuranceDTO> UpdateInsurance(InsuranceDTO input);
        Task<PagedResultDto<InsuranceDTO>> GetAllAsync(PagedAndSortedResultRequestDto input);
        Task<PagedResultDto<InsuranceDTO>> GetAsync(PagedAndSortedResultRequestDto input, Guid id);
        void DeleteAsync(Guid id);
    }
}
