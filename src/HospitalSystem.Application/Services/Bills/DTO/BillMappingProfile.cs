using AutoMapper;
using HospitalSystem.Domain;

namespace HospitalSystem.Services.Bills.DTO
{
    class BillMappingProfile: Profile
    {
        public BillMappingProfile()
        {
            CreateMap<Bill, BillDTO>()
                .ForMember(e => e.PatientReportId, m => m.MapFrom(e => e.PatientReport.Id));
            CreateMap<InputBillDTO, Bill>();
            CreateMap<Bill, InputBillDTO>();
            CreateMap<BillDTO, Bill>();
        }
    }
}
