using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HospitalSystem.Domain;
using HospitalSystem.Domain.RefLists;
using System;

namespace HospitalSystem.Services.Procedures.DTO
{
    [AutoMapFrom(typeof(Procedure))]
    public class ProcedureDTO : EntityDto<Guid>
    {
        public virtual RefListProcedureType Type { get; set; }
        public virtual string Description { get; set; }
        public virtual RefListOutcome Outcome { get; set; }
        public virtual Guid PrescribedTestId { get; set; }
    }
}
