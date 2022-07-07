using Abp.AutoMapper;
using HospitalSystem.Domain;
using System;

namespace HospitalSystem.Services.Bills.DTO
{
    [AutoMapFrom(typeof(Bill))]
    public class InputBillDTO
    {
        public virtual Guid PatientReportId { get; set; }
    }
}
//guid 
//refList