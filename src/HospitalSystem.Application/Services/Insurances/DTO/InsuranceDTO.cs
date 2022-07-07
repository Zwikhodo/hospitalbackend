using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HospitalSystem.Domain;
using System;

namespace HospitalSystem.Services.Insurances.DTO
{
    [AutoMap(typeof(Insurance))]
    public class InsuranceDTO : EntityDto<Guid>
    {
        public virtual string Company { get; set; }
        public virtual string RegistrationNumber { get; set; }
        public virtual decimal InsuranceCapAmount { get; set; }
    }
}
