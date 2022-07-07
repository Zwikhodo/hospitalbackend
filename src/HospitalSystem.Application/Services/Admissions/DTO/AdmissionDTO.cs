using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HospitalSystem.Domain;
using System;

namespace HospitalSystem.Services.Admissions.DTO
{
    [AutoMap(typeof(Admission))]
    public class AdmissionDTO : EntityDto<Guid>
    {
        public DateTime Date { get; set; }
        public DateTime DischargeDate { get; set; }
        public Guid PatientId { get; set; }
        public Guid RoomId { get; set; }
    }
}
