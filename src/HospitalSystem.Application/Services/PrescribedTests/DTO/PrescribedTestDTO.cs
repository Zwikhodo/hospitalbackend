using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HospitalSystem.Domain;
using HospitalSystem.Domain.RefLists;
using System;

namespace HospitalSystem.Services.PrescribedTests.DTO
{
    [AutoMapFrom(typeof(PrescribedTest))]
    public class PrescribedTestDTO: EntityDto<Guid>
    {
        public virtual RefListPrescribedTest Type { get; set; }
        public virtual RefListOutcome Outcome { get; set; }
        public virtual Guid AdmissionId { get; set; }
    }
}
