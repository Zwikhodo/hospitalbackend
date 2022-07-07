using Abp.Application.Services.Dto;
using HospitalSystem.Domain.RefLists;
using System;

namespace HospitalSystem.Services.Examinations.DTO
{
    public class ExaminationDTO : EntityDto<Guid>
    {
        public virtual float Weight { get; set; }
        public virtual float Height { get; set; }
        public virtual string BloodPressure { get; set; }
        public virtual float Temperature { get; set; }
        public virtual string PulseRate { get; set; }
        public virtual string RespiratoryRate { get; set; }
        public virtual RefListOutcome Outcome { get; set; }
        public virtual Guid PatientId { get; set; }
    }
}
