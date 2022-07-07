using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using HospitalSystem.Authorization;
using HospitalSystem.Domain;
using HospitalSystem.Services.Examinations.DTO;
using HospitalSystem.Services.Examinations.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Examinations
{
    /// <summary>
    /// Endpoints for examining the patient 
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Examination)]
    public class ExaminationAppService : ApplicationService
    {
        private readonly IRepository<Examination, Guid> _examinationRepository;
        private readonly IRepository<Patient, Guid> _patientRepository;
        private readonly IExaminationHelper _helper;

        /// Injecting repositories
        public ExaminationAppService(IExaminationHelper helper, IRepository<Examination, Guid> examinationRepository,IRepository<Patient, Guid> patientRepository)
        {
            _examinationRepository = examinationRepository;
            _patientRepository = patientRepository;
            _helper = helper;
        }

        ///Creating examination
        public async Task<ExaminationDTO> CreateAsync(ExaminationDTO input)
        {
            var checkExamination = await _examinationRepository.GetAllIncluding(n => n.Patient, m => m.Employee)
                                                               .Where(n => n.Patient.Id == input.PatientId)
                                                               .FirstOrDefaultAsync();
            if (checkExamination != null) throw new UserFriendlyException("An examination already exists for this patient. Try viewing or updating the entry.");
            var patient = await _patientRepository.GetAll().Where(n => n.Id == input.PatientId).FirstOrDefaultAsync();
            var vitals = ObjectMapper.Map<Examination>(input);
            await _helper.AssignEmployee(input, vitals);
            vitals.Patient = patient;
            await _helper.PostAdmission(input, patient);

            return ObjectMapper.Map<ExaminationDTO>(await _examinationRepository.InsertAsync(vitals));
        }

        /// Deleting Entries
        public async Task DeleteAsync(Guid id)
        {
            await _examinationRepository.DeleteAsync(id);
        }

        /// Getting all entity entries
        [AbpAuthorize(PermissionNames.Pages_Overview)]
        public async Task<List<ExaminationDTO>> GetAllAsync()
        {
            var entries = await _examinationRepository.GetAllIncluding(n => n.Patient, m => m.Employee).ToListAsync();
            return ObjectMapper.Map<List<ExaminationDTO>>(entries);
        }

        /// Filtering entity
        [AbpAuthorize(PermissionNames.Pages_Overview)]
        public async Task<List<ExaminationDTO>> GetExaminationAsync(int outcome, string employeeNumber, string patientNumber)
        {
            var entityFilter = await _examinationRepository.GetAllIncluding(n => n.Employee, m => m.Patient)
                                                           .Where(x => (int)x.Outcome == outcome
                                                           || x.Employee.EmployeeNumber == employeeNumber
                                                           || x.Patient.PatientNumber == patientNumber).ToListAsync();
            return ObjectMapper.Map<List<ExaminationDTO>>(entityFilter);
        }

        /// Updating entity entry
        [HttpPut("patient-number/{patientNumber}")]
        public async Task<ExaminationDTO> UpdateAsync(ExaminationDTO input, string patientNumber)
        {
            var entity = await _examinationRepository.GetAllIncluding(n => n.Employee, m => m.Patient)
                                                   .Where(n => n.Patient.PatientNumber == patientNumber)
                                                   .FirstOrDefaultAsync();
            if (entity == null) throw new UserFriendlyException("Data not found");
            ObjectMapper.Map(input, entity);
            await _examinationRepository.UpdateAsync(entity);
            return ObjectMapper.Map<ExaminationDTO>(entity);
        }
    }
}
