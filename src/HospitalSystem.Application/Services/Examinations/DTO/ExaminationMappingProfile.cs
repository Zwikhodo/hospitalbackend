using AutoMapper;
using HospitalSystem.Domain;
using System;

namespace HospitalSystem.Services.Examinations.DTO
{
    public class ExaminationMappingProfile : Profile
    {
        public ExaminationMappingProfile()
        {
            CreateMap<Examination, ExaminationDTO>()
                .ForMember(e => e.PatientId, m => m.MapFrom(e => e.Patient != null ? e.Patient.Id : (Guid?)null));

            CreateMap<ExaminationDTO, Examination>()
            .ForMember(n => n.Id, d => d.Ignore());

        }
    }
}
