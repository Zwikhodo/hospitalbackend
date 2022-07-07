using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HospitalSystem.Domain;
using System;

namespace HospitalSystem.Services.PatientReports.DTO
{
    [AutoMapFrom(typeof(PatientReport))]
    public class PatientReportDTO : EntityDto<Guid>
    {
        public virtual string Diagnosis { get; set; }
        public virtual Guid PatientId { get; set; }
        public virtual Guid ExaminationId { get; set; }
        public virtual Guid PrescribedTestId { get; set; }
        public virtual Guid ProcedureId { get; set; }
        public virtual string[] MedicineCode { get; set; }
    }
}
