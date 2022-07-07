using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using HospitalSystem.Authorization;
using HospitalSystem.Domain;
using HospitalSystem.Services.Admissions.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Admissions
{
    /// <summary>
    /// Endpoints for admitting the patient 
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Admission)]
    public class AdmissionAppService : ApplicationService
    {
        private readonly IRepository<Admission, Guid> _admissionRepository;
        private readonly IRepository<Patient, Guid> _patientRepository;
        private readonly IRepository<Room, Guid> _roomRepository;
        
        /// Injecting repositories
        public AdmissionAppService(IRepository<Admission, Guid> admissionRepository, 
                                   IRepository<Patient, Guid> patientRepository, 
                                   IRepository<Room, Guid> roomRepository)
        {
            _admissionRepository = admissionRepository;
            _patientRepository = patientRepository;
            _roomRepository = roomRepository;
        }

        ///Creating admission
        public async Task<AdmissionDTO> CreateAsync(AdmissionDTO input)
        {
            var checkAdmission = await _admissionRepository.GetAllIncluding(n => n.Room, m => m.Patient)
                                                           .Where(n => n.Patient.Id == input.PatientId)
                                                           .FirstOrDefaultAsync();
            if (checkAdmission != null) throw new UserFriendlyException($"Patient has already been admitted: {checkAdmission}");
            var admission = ObjectMapper.Map<Admission>(input);
            admission.Patient = await _patientRepository.FirstOrDefaultAsync(n => n.Id == input.PatientId);
            admission.Room = await _roomRepository.GetAll().Where(n => n.Id == input.RoomId).FirstOrDefaultAsync();

            if (admission.Room.AvailableBeds <= 0) throw new UserFriendlyException("There are no available beds in this ward");
            await _admissionRepository.InsertAsync(admission);
            admission.Room.AvailableBeds -= 1;
            await _roomRepository.UpdateAsync(admission.Room);

            return ObjectMapper.Map<AdmissionDTO>(admission);
        }

        /// Deleting Entries
        public async Task DeleteAsync(Guid id)
        {
            await _admissionRepository.DeleteAsync(id);
        }

        /// Getting all entity entries
        [AbpAuthorize(PermissionNames.Pages_Overview)]
        public async Task<List<AdmissionDTO>> GetAllAsync()
        {
            var entries = await _admissionRepository.GetAllIncluding(n => n.Patient, m => m.Room).ToListAsync();
            return ObjectMapper.Map<List<AdmissionDTO>>(entries);
        }

        /// Filtering entity
        [AbpAuthorize(PermissionNames.Pages_Overview)]
        
        public async Task<List<AdmissionDTO>> GetAdmissionsAsync(string roomNumber, string patientNumber)
        {
            var entityFilter = await _admissionRepository.GetAllIncluding(n => n.Room, m => m.Patient)
                                                         .Where(x => x.Room.RoomNumber == roomNumber || x.Patient.PatientNumber == patientNumber)
                                                         .ToListAsync();
            return ObjectMapper.Map<List<AdmissionDTO>>(entityFilter);
        }

        /// Updating entity entry

        public async Task<AdmissionDTO> UpdateAsync (AdmissionDTO input, string patientNumber)
        {
            var entity = await _admissionRepository.GetAllIncluding(n => n.Room, m => m.Patient)
                                                   .Where(n => n.Patient.PatientNumber == patientNumber)
                                                   .FirstOrDefaultAsync();
            if (entity == null) throw new UserFriendlyException("Data not found");
            ObjectMapper.Map(input, entity);
            await _admissionRepository.UpdateAsync(entity);
            return ObjectMapper.Map<AdmissionDTO>(entity);
        }
    }
}