using Abp.AutoMapper;
using HospitalSystem.Domain;
using System;

namespace HospitalSystem.Services.Bills.DTO
{
    [AutoMap(typeof(Bill))]
    public class BillDTO
    {
        public decimal MedicineCharge { get; set; }
        public decimal EmployeeCharge { get; set; }
        public decimal PrescribedTestCharge { get; set; }
        public decimal ProcedureCharge { get; set; }
        public decimal CostPerNight { get; set; }
        public decimal TotalBill { get; set; }
        public Guid PatientReportId { get; set; }
    }
}
