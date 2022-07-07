using Abp.AutoMapper;
using HospitalSystem.Domain;
using HospitalSystem.Domain.RefLists;
using HospitalSystem.Services.Persons.DTO;

namespace HospitalSystem.Services.Employees.DTO
{
    [AutoMap(typeof(Employee))]
    public class EmployeeDTO : PersonDTO
    {
        public virtual string EmployeeNumber { get; set; }
        public virtual RefListEmployeeType Type { get; set; }
        public virtual RefListSpecialization? Specialization { get; set; }
        public virtual long? UserId { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string[] RoleNames { get; set; }
    }
}
