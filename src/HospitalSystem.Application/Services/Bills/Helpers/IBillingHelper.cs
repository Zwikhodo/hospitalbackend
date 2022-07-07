using Abp.Dependency;
using HospitalSystem.Domain;

namespace HospitalSystem.Services.Bills.Helpers
{
    public interface IBillingHelper : ISingletonDependency
    {
        decimal CalculateProcedureCharge(PatientReport PatientReport, decimal procedureCharge);
        decimal CalculatePrescribedTestCharge(PatientReport PatientReport, decimal prescribedTestCharge);
        decimal CalculateMedicineCharge(PatientReport PatientReport, decimal medicineCharge);
        decimal CalculateEmployeeCharge(PatientReport PatientReport, decimal employeeCharge);
        decimal CalculateCostPerNight(PatientReport PatientReport, decimal costPerNight);
    }
}
