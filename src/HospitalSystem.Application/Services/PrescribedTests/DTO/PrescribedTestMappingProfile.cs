using AutoMapper;
using HospitalSystem.Domain;
using System;

namespace HospitalSystem.Services.PrescribedTests.DTO
{
    public class PrescribedTestMappingProfile: Profile
    {
        public PrescribedTestMappingProfile()
        {

            CreateMap<PrescribedTest, PrescribedTestDTO>()
                .ForMember(e => e.AdmissionId, m => m.MapFrom(e => e.Admission != null ? e.Admission.Id : (Guid?)null));

            CreateMap<PrescribedTestDTO, PrescribedTest>()
            .ForMember(n => n.Id, d => d.Ignore());
        }
    }
}
