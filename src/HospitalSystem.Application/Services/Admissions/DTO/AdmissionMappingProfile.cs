using AutoMapper;
using HospitalSystem.Domain;

namespace HospitalSystem.Services.Admissions.DTO
{
    public class AdmissionMappingProfile:Profile
    {
        public AdmissionMappingProfile()
        {
            CreateMap<Admission, AdmissionDTO>()
                .ForMember(e => e.PatientId, m => m.MapFrom(e => e.Patient.Id))
                .ForMember(e => e.RoomId, m => m.MapFrom(e => e.Room.Id));

            CreateMap<AdmissionDTO, Admission>();
        }
    }
}
