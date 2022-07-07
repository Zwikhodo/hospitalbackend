using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using HospitalSystem.Authorization;
using HospitalSystem.Domain;
using HospitalSystem.Services.Appointments.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Appointments
{
    /// <summary>
    /// Endpoints for creating appointment for the patient 
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_CreateAppointment)]
    public class AppointmentAppService: ApplicationService
    {
        private readonly IRepository<Appointment, Guid> _appointmentRepository;
        private readonly IRepository<Patient, Guid> _patientRepository;
        private readonly IRepository<EmployeeLog, Guid> _employeeLogRepository;

        /// Injecting repositories
        public AppointmentAppService(IRepository<Appointment, Guid> appointmentRepository, 
                                     IRepository<Patient, Guid> patientRepository, 
                                     IRepository<EmployeeLog, Guid> employeeLogRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _employeeLogRepository = employeeLogRepository;
        }

        ///Creating appointment
        public async Task<AppointmentDTO> CreateAsync(AppointmentDTO input)
        {
            var checkPatient = _patientRepository.GetAll().Where(x => x.Id == input.PatientId).FirstOrDefault();
            var checkSlot = _employeeLogRepository.GetAllIncluding(n => n.Employee)
                                                  .Where(n =>input.StartTime > n.CheckIn && n.Availability == true)
                                                  .FirstOrDefault();
            var appointment = ObjectMapper.Map<Appointment>(input);
            appointment.Patient = checkPatient;
            appointment.Employee = checkSlot.Employee;
            appointment.EndTime = input.StartTime.AddMinutes(input.Duration);
            appointment.AppointmentNumber = $"APP{new Random().Next(10000)}_{appointment.Patient.FirstName.Substring(0, 3)}_{appointment.Employee.FirstName.Substring(2, 2)}";
            var checkAppointment = _appointmentRepository.GetAll().Where(x => x.Patient.Id == appointment.Patient.Id).FirstOrDefault();
            checkSlot.Availability = false;
            await _employeeLogRepository.UpdateAsync(checkSlot);
            if (checkAppointment != null) throw new UserFriendlyException("Patient already has an appointment set. Try updating the appointment entry.");
            return ObjectMapper.Map<AppointmentDTO>(await _appointmentRepository.InsertAsync(appointment));
        }

        /// Deleting Entries
        public async Task DeleteAsync(Guid id)
        {
            await _appointmentRepository.DeleteAsync(id);
        }

        /// Getting all entity entries
        [AbpAuthorize(PermissionNames.Pages_Overview)]
        public async Task<List<AppointmentDTO>> GetAllAsync()
        {
            var entries = await _appointmentRepository.GetAllIncluding(n => n.Patient, m => m.Employee).ToListAsync();
            return ObjectMapper.Map<List<AppointmentDTO>>(entries);
        }

        /// Filtering entity
        [AbpAuthorize(PermissionNames.Pages_Overview)]
        public async Task<List<AppointmentDTO>> GetAppointmentsAsync(string appointmentNumber, string employeeNumber, string patientNumber, int status)
        {
            var entityFilter = await _appointmentRepository.GetAllIncluding(n => n.Patient, m => m.Employee)
                                                           .Where(x => x.AppointmentNumber == appointmentNumber 
                                                           || x.Employee.EmployeeNumber == employeeNumber 
                                                           || x.Patient.PatientNumber == patientNumber 
                                                           || (int)x.Status == status).ToListAsync();
            return ObjectMapper.Map<List<AppointmentDTO>>(entityFilter);
        }

        /// Updating entity entry

        public async Task<AppointmentDTO> UpdateAsync(AppointmentDTO input, string appointmentNumber)
        {
            var entity = await _appointmentRepository.GetAllIncluding(n => n.Employee, m => m.Patient)
                                                   .Where(n => n.AppointmentNumber == appointmentNumber)
                                                   .FirstOrDefaultAsync();
            if (entity == null) throw new UserFriendlyException("Data not found");
            ObjectMapper.Map(input, entity);
            await _appointmentRepository.UpdateAsync(entity);
            return ObjectMapper.Map<AppointmentDTO>(entity);
        }
    }
}
