using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HospitalSystem.Services.Admissions.DTO;
using System;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Admissions
{
    public interface IAdmissionAppService:IApplicationService
    {
        Task<AdmissionDTO> Create(AdmissionDTO input);
        Task<AdmissionDTO> Update(AdmissionDTO input);
        Task<PagedResultDto<AdmissionDTO>> GetAll(PagedAndSortedResultRequestDto input);
        Task<PagedResultDto<AdmissionDTO>> GetPatientDischargeDate(PagedAndSortedResultRequestDto input, Guid id);
        void DeleteAsync(Guid id);

    }
}
