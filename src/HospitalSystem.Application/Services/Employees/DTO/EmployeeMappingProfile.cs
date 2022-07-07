using AutoMapper;
using HospitalSystem.Authorization.Users;
using HospitalSystem.Domain;

namespace HospitalSystem.Services.Employees.DTO
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>()
                   .ForMember(e => e.Username, m => m.MapFrom(e => e.User != null ? e.User.UserName : null))
                   .ForMember(e => e.RoleNames, m => m.MapFrom(e => e.User != null ? e.User.Roles : null))
                   .ForMember(e => e.Password, m => m.MapFrom(e => e.User != null ? e.User.Password : null));

            CreateMap<EmployeeDTO, Employee>()
                .ForMember(e => e.Id, d => d.Ignore());

            CreateMap<EmployeeDTO, User>()
                .ForMember(e => e.Id, m => m.MapFrom(e => e.UserId));

            CreateMap<User, EmployeeDTO>()
                .ForMember(e => e.Id, d => d.Ignore());

        }
    }
}
