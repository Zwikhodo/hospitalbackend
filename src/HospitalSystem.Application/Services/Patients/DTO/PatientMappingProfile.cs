using AutoMapper;
using HospitalSystem.Domain;

namespace HospitalSystem.Services.Patients.DTO
{
    public class PatientMappingProfile : Profile
    {
        public PatientMappingProfile()
        {
            CreateMap<Patient, PatientDTO>();
            CreateMap<PatientDTO, Patient>();
        }
    }
}
