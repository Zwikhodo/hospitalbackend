using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using HospitalSystem.Authorization;
using HospitalSystem.Domain;
using HospitalSystem.Domain.RefLists;
using HospitalSystem.Services.PrescribedTests.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Services.PrescribedTests
{
    /// Prescribed Tests performed by doctors
    [AbpAuthorize(PermissionNames.Pages_PrescribedTests)]
    public class PrescribedTestAppService: ApplicationService
    {
        private readonly IRepository<PrescribedTest, Guid> _prescribedTestRepository;
        private readonly IRepository<Admission, Guid> _admissionRepository;
        private readonly IRepository<EmployeeLog, Guid> _employeeLogRepository;
        /// Injecting repositories
        public PrescribedTestAppService(IRepository<EmployeeLog, Guid> employeeLogRepository, 
                                        IRepository<PrescribedTest, Guid> prescribedTestRepository, 
                                        IRepository<Admission, Guid> admissionRepository)
        {
            _admissionRepository = admissionRepository;
            _prescribedTestRepository = prescribedTestRepository;
            _employeeLogRepository = employeeLogRepository;
        }
        /// Creating prescribed tests
        public async Task<PrescribedTestDTO> CreateAsync(PrescribedTestDTO input)
        {
            var checkPrescribedTest = await _prescribedTestRepository.GetAll()
                                                                     .Where(n => n.Admission.Id == input.AdmissionId)
                                                                     .OrderBy(n => n.CreationTime)
                                                                     .LastOrDefaultAsync();
            if (checkPrescribedTest == null) return await CreatePrescribedTest(input);
            
            switch (checkPrescribedTest.Outcome)
            {
                case RefListOutcome.Conclusive:
                    throw new UserFriendlyException(@"No more prescribed tests are required for this 
                                                    patient as the results of the last one where conclusive.");
                case RefListOutcome.ProcedureRequired:
                    throw new UserFriendlyException(@"This patient is due for a procedure.
                                                     Please see patient test history.");
                default:
                    break;
            }
            return await CreatePrescribedTest(input);
        }
        /// Deleting entries
        public async Task DeleteAsync(Guid id)
        {
            await _prescribedTestRepository.DeleteAsync(id);
            
        }
        /// Filtering entity entries
        public async Task<List<PrescribedTestDTO>> GetPrescribedTestsAsync(string patientNumber, string employeeNumber, int outcome, int type)
        {
            var entityFilter = await _prescribedTestRepository.GetAllIncluding(n => n.Employee, m => m.Admission, x => x.Patient)
                                                              .Where(x => x.Employee.EmployeeNumber == employeeNumber
                                                                    || x.Patient.PatientNumber == patientNumber
                                                                    || (int)x.Outcome == outcome
                                                                    || (int)x.Type == type)
                                                              .ToListAsync();
            return ObjectMapper.Map<List<PrescribedTestDTO>>(entityFilter);
        }
        /// Get all entity entries
        public async Task<List<PrescribedTestDTO>> GetAllAsync()
        {
            var entries = await _prescribedTestRepository.GetAllIncluding(n => n.Employee, m => m.Admission)
                                                         .ToListAsync();
            return ObjectMapper.Map<List<PrescribedTestDTO>>(entries);
        }
        /// Update entity entries
        [HttpPut("room-number/{roomNumber}/patient-number{patientNumber}")]
        public async Task<PrescribedTestDTO> UpdateAsync(PrescribedTestDTO input, string patientNumber)
        {
            var entity = await _prescribedTestRepository.GetAll()
                                                        .Where(n => n.Admission.Patient.PatientNumber == patientNumber)
                                                        .FirstOrDefaultAsync();
            if (entity == null) throw new UserFriendlyException("Data not found");
            ObjectMapper.Map(input, entity);
            await _prescribedTestRepository.UpdateAsync(entity);
            return ObjectMapper.Map<PrescribedTestDTO>(entity);
        }
        private async Task<PrescribedTestDTO> CreatePrescribedTest(PrescribedTestDTO input)
        {
            var prescribedTest = ObjectMapper.Map<PrescribedTest>(input);
            prescribedTest.Admission = await _admissionRepository.GetAllIncluding(x => x.Patient)
                                                                 .Where(n => n.Id == input.AdmissionId)
                                                                 .FirstOrDefaultAsync();
            prescribedTest.Patient = prescribedTest.Admission.Patient;
            var checkEmployee = _employeeLogRepository.GetAllIncluding(n => n.Employee)
                                                      .Where(n => n.Availability == true && n.Employee.Type == RefListEmployeeType.Doctor)
                                                      .FirstOrDefault();
            prescribedTest.Employee = checkEmployee.Employee;
            checkEmployee.Availability = false;
            await _employeeLogRepository.UpdateAsync(checkEmployee);
            return ObjectMapper.Map<PrescribedTestDTO>(await _prescribedTestRepository.InsertAsync(prescribedTest));
        }
    }
}
