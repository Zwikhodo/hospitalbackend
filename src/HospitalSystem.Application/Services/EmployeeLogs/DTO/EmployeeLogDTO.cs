using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HospitalSystem.Domain;
using System;

namespace HospitalSystem.Services.EmployeeLogs.DTO
{
    [AutoMapFrom(typeof(EmployeeLog))]
    public class EmployeeLogDTO: EntityDto<Guid>
    {
        public virtual Guid EmployeeId { get; set; }
        public virtual DateTime CheckIn { get; set; }
        public virtual DateTime? CheckOut { get; set; }
        public virtual bool Availability { get; set; }
    }
}
