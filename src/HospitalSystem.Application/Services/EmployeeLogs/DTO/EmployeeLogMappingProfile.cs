using AutoMapper;
using HospitalSystem.Domain;

namespace HospitalSystem.Services.EmployeeLogs.DTO
{
    public class EmployeeLogMappingProfile: Profile
    {
        public EmployeeLogMappingProfile()
        {
            CreateMap<EmployeeLog, EmployeeLogDTO>()
                .ForMember(e => e.EmployeeId, m => m.MapFrom(e => e.Employee.Id));

            CreateMap<EmployeeLogDTO, EmployeeLog>()
                .ForMember(n => n.Id, d => d.Ignore());
        }
    }
}
