using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using HospitalSystem.Domain;
using HospitalSystem.Domain.RefLists;
using HospitalSystem.Services.Patients.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Patients
{
    /// <summary>
    /// Endpoints for creating patient 
    /// </summary>
    public class PatientAppService : ApplicationService
    {
        private readonly IRepository<Patient, Guid> _patientRepository;

        /// Injecting repositories
        public PatientAppService(IRepository<Patient, Guid> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        /// Creating patient
        public async Task<PatientDTO> CreateAsync(PatientDTO input)
        {
            var checkExists = await _patientRepository.FirstOrDefaultAsync(n => n.IdentificationNumber == input.IdentificationNumber);
            if (checkExists != null) throw new UserFriendlyException("Patient Already Exists In the Hospital Database");
            var patient = ObjectMapper.Map<Patient>(input);
            if (input.Type == RefListPatientType.Emergency)
            {
                patient.PatientNumber = $"EME{new Random().Next(10000)}";
            }
            else
            {
                patient.PatientNumber = $"PAT{ input.FirstName.Substring(0, 3)}_{new Random().Next(10000)}_{input.LastName.Substring(0, 3)}";
            }
            return ObjectMapper.Map<PatientDTO>(await _patientRepository.InsertAsync(patient));
        }

        /// Deleting Entries
        public async Task DeleteAsync(Guid id)
        {
            await _patientRepository.DeleteAsync(id);
        }

        /// Getting all entities
        public async Task<List<PatientDTO>> GetAllAsync()
        {
            var entries = await _patientRepository.GetAll().ToListAsync();
            return ObjectMapper.Map<List<PatientDTO>>(entries);
        }

        /// Filtering entity
        public async Task<List<PatientDTO>> GetPatientAsync(string patientNumber, int patientType)
        {
            var entityFilter = await _patientRepository.GetAll()
                                                       .Where(x => (int)x.Type == patientType 
                                                       || x.PatientNumber == patientNumber)
                                                       .ToListAsync();
            return ObjectMapper.Map<List<PatientDTO>>(entityFilter);
        }

        /// Updating entity entry
        public async Task<PatientDTO> UpdateAsync(PatientDTO input, string patientNumber)
        {
            var entity = await _patientRepository.GetAll()
                                                 .Where(n => n.PatientNumber == patientNumber)
                                                 .FirstOrDefaultAsync();
            if (entity == null) throw new UserFriendlyException("Data not found");
            ObjectMapper.Map(input, entity);
            await _patientRepository.UpdateAsync(entity);
            CurrentUnitOfWork.SaveChanges();
            return ObjectMapper.Map<PatientDTO>(entity);
        }
    }
}
