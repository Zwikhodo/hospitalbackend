using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using HospitalSystem.Authorization;
using HospitalSystem.Domain;
using HospitalSystem.Services.PatientReports.DTO;
using HospitalSystem.Services.PatientReports.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Services.PatientReports
{
    /// Endpoint for generating patient reports
    [AbpAuthorize(PermissionNames.Pages_Overview)]
    public class PatientReportAppService: ApplicationService
    {
        private readonly IRepository<Examination, Guid> _examinationRepository;
        private readonly IRepository<PrescribedTest, Guid> _prescribedTestRepository;
        private readonly IRepository<Procedure, Guid> _procedureRepository;
        private readonly IRepository<Patient, Guid> _patientRepository;
        private readonly IRepository<Medicine, Guid> _medicineRepository;
        private readonly IRepository<PatientReport, Guid> _patientReportRepository;
        private readonly IPatientReportHelper _patientReportHelper;

        /// Injecting repositories
        public PatientReportAppService(IRepository<Medicine, Guid> medicineRepository, 
                                       IRepository<PatientReport, Guid> patientReportRepository,
                                       IRepository<Examination, Guid> examinationRepository, 
                                       IRepository<PrescribedTest, Guid> prescribedTestRepository, 
                                       IRepository<Procedure, Guid> procedureRepository, 
                                       IRepository<Patient, Guid> patientRepository,
                                       IPatientReportHelper patientReportHelper)
        {
            _examinationRepository = examinationRepository;
            _prescribedTestRepository = prescribedTestRepository;
            _procedureRepository = procedureRepository;
            _patientRepository = patientRepository;
            _patientReportRepository = patientReportRepository;
            _medicineRepository = medicineRepository;
            _patientReportHelper = patientReportHelper;
        }

        ///  Creating patient reports
        public async Task<PatientReportDTO> CreateAsync(PatientReportDTO input)
        {
            var medicines = await _medicineRepository.GetAllListAsync();
            var checkPatient = await _patientRepository.GetAll().Where(n => n.Id == input.PatientId).FirstOrDefaultAsync();
            var checkProcedure = await _procedureRepository.GetAll().Where(n => n.Id == input.ProcedureId).FirstOrDefaultAsync();
            var checkExamination = await _examinationRepository.GetAllIncluding(n => n.Employee).Where(n => n.Id == input.ExaminationId).FirstOrDefaultAsync();
            var checkPrescribedTest = await _prescribedTestRepository.GetAllIncluding(n => n.Employee).Where(n => n.Id == input.PrescribedTestId).FirstOrDefaultAsync();
            var patientReport = ObjectMapper.Map<PatientReport>(input);
            patientReport.Patient = checkPatient;
            patientReport.Procedure = checkProcedure;
            patientReport.PrescribedTest = checkPrescribedTest;
            patientReport.Examination = checkExamination;

            var postPatientReport = await _patientReportRepository.InsertAsync(patientReport);

            await _patientReportHelper.ToggleEmployeeAvailabilityTest(checkPrescribedTest);
            await _patientReportHelper.ToggleEmployeeAvailabilityExam(checkExamination);
            await _patientReportHelper.CreatePatientPrescription(input, medicines, postPatientReport);
            await _patientReportHelper.DischargePatient(checkPatient);

            return ObjectMapper.Map<PatientReportDTO>(postPatientReport);
        }

        /// Deleting entity entries
        public async Task DeleteAsync(Guid id)
        {
            await _patientReportRepository.DeleteAsync(id);
            
        }

        /// Get all entity entries
        public async Task<List<PatientReportDTO>> GetAllAsync()
        {
            var entries = await _patientReportRepository.GetAllIncluding(n => n.Examination, m => m.Procedure,
                                                                         x => x.PrescribedTest, p => p.Patient)
                                                        .ToListAsync();
            return ObjectMapper.Map<List<PatientReportDTO>>(entries);
        }

        /// Filter all entity entries
        
        public async Task<List<PatientReportDTO>> GetReportsAsync(string patientNumber)
        {
            var entityFilter = await _patientReportRepository.GetAllIncluding(n => n.Examination, m => m.Procedure,
                                                                              x => x.PrescribedTest, p => p.Patient)
                                                             .Where(x => x.Patient.PatientNumber == patientNumber)
                                                             .ToListAsync();
            return ObjectMapper.Map<List<PatientReportDTO>>(entityFilter);
                        
        }

        /// Updating all entity entries
      
        public async Task<PatientReportDTO> UpdateAsync(PatientReportDTO input, string patientNumber)
        {
            var entity = await _patientReportRepository.GetAllIncluding(n => n.Examination, m => m.Procedure,
                                                                        x => x.PrescribedTest, p => p.Patient)
                                                       .Where(n => n.Patient.PatientNumber == patientNumber)
                                                       .FirstOrDefaultAsync();
            if (entity == null) throw new UserFriendlyException("Data not found");
            ObjectMapper.Map(input, entity);
            await _patientReportRepository.UpdateAsync(entity);
            return ObjectMapper.Map<PatientReportDTO>(entity);
        }
    }
}
