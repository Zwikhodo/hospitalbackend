using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HospitalSystem.Services.Patients.DTO;
using System;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Patients
{
    public interface IPatientAppService: IApplicationService
    {
        Task<PatientDTO> CreateAsync(PatientDTO input);
        Task<PatientDTO> UpdatePatient(PatientDTO input);
        Task<PagedResultDto<PatientDTO>> GetAllAsync(PagedAndSortedResultRequestDto input);
        Task<PagedResultDto<PatientDTO>> GetAsync(PagedAndSortedResultRequestDto input, Guid id);
        void DeleteAsync(Guid id);
    }
}
