using Abp.Dependency;
using Abp.Domain.Repositories;
using HospitalSystem.Domain;
using System;
using System.Linq;

namespace HospitalSystem.Services.Bills.Helpers
{
    /// contains extensions and helpers which assist the billing app services 
    public class BillingHelper : IBillingHelper
    {
        private readonly IRepository<Procedure, Guid> procedureRepository;
        private readonly IRepository<PrescribedTest, Guid> prescribedTestRepository;
        private readonly IRepository<Prescription, Guid> prescriptionRepository;
        private readonly IRepository<Admission, Guid> admissionRepository;


        /// injecting required repositories
        public BillingHelper()
        {
            procedureRepository = IocManager.Instance.Resolve<IRepository<Procedure, Guid>>();
            prescribedTestRepository = IocManager.Instance.Resolve<IRepository<PrescribedTest, Guid>>();
            prescriptionRepository = IocManager.Instance.Resolve<IRepository<Prescription, Guid>>();
            admissionRepository = IocManager.Instance.Resolve<IRepository<Admission, Guid>>();
        }

        /// calculating cost of tests performed on patient
        public decimal CalculatePrescribedTestCharge(PatientReport PatientReport, decimal prescribedTestCharge)
        {
            var prescribedTestsTypes = prescribedTestRepository.GetAllIncluding(n => n.Patient, m => m.Admission)
                                                               .Where(x => x.Admission.Patient.Id == PatientReport.Patient.Id &&
                                                                      x.Type.HasValue)
                                                               .Select(z => z.Type.Value).ToList();
            if (prescribedTestsTypes.Any())
            {
                foreach (var testType in prescribedTestsTypes)
                {
                    prescribedTestCharge += PrescribedTestCost((int)testType);
                }
            }

            return prescribedTestCharge;
        }

        /// calculating cost of procedures performed on patient
        public decimal CalculateProcedureCharge(PatientReport patientReport, decimal procedureCharge)
        {
            var procedureTypes = procedureRepository
                .GetAllIncluding(n => n.PrescribedTest)
                .Where(x => x.PrescribedTest.Admission.Patient.Id == patientReport.Patient.Id &&
                            x.Type.HasValue)
                .Select(z => z.Type.Value).ToList();

            if (procedureTypes.Any())
            {
                foreach (var procedureType in procedureTypes)
                {
                    procedureCharge += ProcedureCost((int)procedureType);
                }
            }

            return procedureCharge;
        }

        /// calculating cost of prescribed medicine
        public decimal CalculateMedicineCharge(PatientReport PatientReport, decimal medicineCharge)
        {
            var prescribedMedicine = prescriptionRepository.GetAllIncluding(n => n.Medicine)
                                                      .Where(x => x.PatientReport.Id == PatientReport.Id)
                                                      .Select(z => z.Medicine.Cost).ToList();
            if (prescribedMedicine.Any())
            {
                foreach (var medicine in prescribedMedicine)
                {
                    medicineCharge += medicine;
                }
            }

            return medicineCharge;
        }

        /// calculating cost of employees seen by patient
        public decimal CalculateEmployeeCharge(PatientReport PatientReport, decimal employeeCharge)
        {
            if (PatientReport.Examination != null) employeeCharge += EmployeeChargeCost((int)PatientReport.Examination.Employee.Type);
            if (PatientReport.PrescribedTest != null) employeeCharge += EmployeeChargeCost((int)PatientReport.PrescribedTest.Employee.Type);
            return employeeCharge;
        }

        /// calculating cost of rooms patient stayed in
        public decimal CalculateCostPerNight(PatientReport PatientReport, decimal costPerNight)
        {
            var checkAdmission = admissionRepository.GetAllIncluding(n => n.Room)
                                                    .Where(x => x.Patient.Id == PatientReport.Patient.Id)
                                                    .FirstOrDefault();
            if (checkAdmission != null)
            {
                costPerNight = (decimal)(checkAdmission.DischargeDate - checkAdmission.Date).TotalDays * checkAdmission.Room.CostPerNight;
            }

            return costPerNight;
        }

        private static decimal EmployeeChargeCost(int employeeType)
        {
            var charge = 0;
            switch (employeeType)
            {
                case 1:
                    charge = 450;
                    break;
                case 2:
                    charge = 1000;
                    break;
                case 3:
                    charge = 2000;
                    break;
                default:
                    break;
            }
            return charge;
        }

        private static decimal PrescribedTestCost(int test)
        {
            decimal charge = 0;
            switch (test)
            {
                case 1:
                    charge = 1000;
                    break;
                case 2:
                    charge = 1200;
                    break;
                case 3:
                    charge = 1400;
                    break;
                case 4:
                    charge = 1600;
                    break;
                case 5:
                    charge = 1800;
                    break;
                case 6:
                    charge = 2000;
                    break;
                case 7:
                    charge = 2200;
                    break;
                case 8:
                    charge = 2400;
                    break;
                case 9:
                    charge = 2600;
                    break;
                case 10:
                    charge = 2800;
                    break;
                case 11:
                    charge = 3000;
                    break;
                case 12:
                    charge = 1000;
                    break;
                default:
                    break;
            }
            return charge;
        }

        private static decimal ProcedureCost(int procedure)
        {
            decimal charge = 0;
            switch (procedure)
            {
                case 1:
                    charge = 6000;
                    break;
                case 2:
                    charge = 6000;
                    break;
                case 3:
                    charge = 6000;
                    break;
                case 4:
                    charge = 6000;
                    break;
                case 5:
                    charge = 6000;
                    break;
                case 6:
                    charge = 6000;
                    break;
                case 7:
                    charge = 6000;
                    break;
                case 8:
                    charge = 6000;
                    break;
                case 9:
                    charge = 6000;
                    break;
                default:
                    break;
            }
            return charge;
        }
    }
}
