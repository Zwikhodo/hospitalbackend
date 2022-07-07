using AutoMapper;
using HospitalSystem.Domain;

namespace HospitalSystem.Services.PatientReports.DTO
{
    public class PatientReportMappingProfile : Profile
    {
        public PatientReportMappingProfile()
        {
            CreateMap<PatientReport, PatientReportDTO>()
                .ForMember(e => e.ProcedureId, m => m.MapFrom(e => e.Procedure.Id))
                .ForMember(e => e.ExaminationId, m => m.MapFrom(e => e.Examination.Id))
                .ForMember(e => e.PatientId, m => m.MapFrom(e => e.Patient.Id))
                .ForMember(e => e.PrescribedTestId, m => m.MapFrom(e => e.PrescribedTest.Id));

            CreateMap<PatientReportDTO, PatientReport>();
        }
    }
}
