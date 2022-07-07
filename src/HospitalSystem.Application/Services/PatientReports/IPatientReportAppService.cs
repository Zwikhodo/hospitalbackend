using Abp.Application.Services;
using HospitalSystem.Services.PatientReports.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalSystem.Services.PatientReports
{
    public interface IPatientReportAppService: IApplicationService
    {
        Task<PatientReportDTO> CreateAsync(PatientReportDTO input);
        Task<PatientReportDTO> Update(PatientReportDTO input);
        Task<List<PatientReportDTO>> GetAllAsync();
        Task<List<PatientReportDTO>> GetAsync(Guid id);
        void DeleteAsync(Guid id);
    }
}
