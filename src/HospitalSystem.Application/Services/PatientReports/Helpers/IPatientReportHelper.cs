using Abp.Dependency;
using HospitalSystem.Domain;
using HospitalSystem.Services.PatientReports.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Services.PatientReports.Helpers
{
    public interface IPatientReportHelper : ISingletonDependency
    {
       Task ToggleEmployeeAvailabilityTest(PrescribedTest checkTest);
       Task ToggleEmployeeAvailabilityExam(Examination checkExam);
       Task CreatePatientPrescription(PatientReportDTO input, List<Medicine> medicines, PatientReport postPatientReport);
       Task DischargePatient(Patient checkPatient);
    }
}
