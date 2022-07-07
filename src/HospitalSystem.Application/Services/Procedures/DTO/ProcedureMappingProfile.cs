using AutoMapper;
using HospitalSystem.Domain;

namespace HospitalSystem.Services.Procedures.DTO
{
    public class ProcedureMappingProfile : Profile
    {
        public ProcedureMappingProfile()
        {
            CreateMap<Procedure, ProcedureDTO>()
                .ForMember(e => e.PrescribedTestId, m => m.MapFrom(e => e.PrescribedTest.Id));

            CreateMap<ProcedureDTO, Procedure>();
        }
    }
}
