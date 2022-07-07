using AutoMapper;
using HospitalSystem.Domain;
using System;

namespace HospitalSystem.Services.Appointments.DTO
{
    public class AppointmentMappingProfile: Profile
    {
        public AppointmentMappingProfile()
        {
            CreateMap<Appointment, AppointmentDTO>()
                .ForMember(e => e.EmployeeId, m => m.MapFrom(e => e.Employee != null ? e.Employee.Id : (Guid?)null))
                .ForMember(e => e.PatientId, m => m.MapFrom(e => e.Patient != null ? e.Patient.Id : (Guid?)null));

            CreateMap<AppointmentDTO, Appointment>()
                .ForMember(n => n.Id, d => d.Ignore());
        }
    }
}
