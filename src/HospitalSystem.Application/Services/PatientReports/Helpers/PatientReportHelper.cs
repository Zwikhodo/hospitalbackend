using Abp.Dependency;
using Abp.Domain.Repositories;
using HospitalSystem.Domain;
using HospitalSystem.Services.PatientReports.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Services.PatientReports.Helpers
{
    /// Patient report helper 
    public class PatientReportHelper : IPatientReportHelper
    {

        private readonly IRepository<Prescription, Guid> prescriptionRepository;
        private readonly IRepository<Admission, Guid> admissionRepository;
        private readonly IRepository<Room, Guid> roomRepository;
        private readonly IRepository<EmployeeLog, Guid> employeeLogRepository;

        /// Injecting repositories
        public PatientReportHelper()
        {
            prescriptionRepository = IocManager.Instance.Resolve<IRepository<Prescription, Guid>>();
            admissionRepository = IocManager.Instance.Resolve<IRepository<Admission, Guid>>();
            roomRepository = IocManager.Instance.Resolve<IRepository<Room, Guid>>();
            employeeLogRepository = IocManager.Instance.Resolve<IRepository<EmployeeLog, Guid>>();
        }

        /// Creating the patients prescriptions
        public async Task CreatePatientPrescription(PatientReportDTO input, List<Medicine> medicines, PatientReport postPatientReport)
        {
            foreach (var medicine in input.MedicineCode)
            {
                var prescription = new Prescription()
                {
                    Medicine = medicines.Where(n => n.Code == medicine).FirstOrDefault(),
                    PatientReport = postPatientReport
                };
                await prescriptionRepository.InsertAsync(prescription);
            }
        }
        /// Discharge patient method
        public async Task DischargePatient(Patient checkPatient)
        {
            var checkAdmission = await admissionRepository.GetAllIncluding(n => n.Room)
                                                          .Where(x => x.Patient.Id == checkPatient.Id)
                                                          .FirstOrDefaultAsync();
            if (checkAdmission != null)
            {
                var room = await roomRepository.GetAll().Where(n => n.Id == checkAdmission.Room.Id).FirstOrDefaultAsync();
                room.AvailableBeds += 1;
                await roomRepository.UpdateAsync(room);
                checkAdmission.DischargeDate = DateTime.Now;
                await admissionRepository.UpdateAsync(checkAdmission);
            }
        }
        /// Toggling the employee availability exam 
        public async Task ToggleEmployeeAvailabilityExam(Examination checkExam)
        {
            if (checkExam != null)
            {
                var checkEmployeeLog = await employeeLogRepository.GetAllIncluding(n => n.Employee)
                                                                  .Where(x => x.Employee.Id == checkExam.Employee.Id)
                                                                  .FirstOrDefaultAsync();
                checkEmployeeLog.Availability = true;
                await employeeLogRepository.UpdateAsync(checkEmployeeLog);
            }
        }
        /// Toggling the employee availability test
        public async Task ToggleEmployeeAvailabilityTest(PrescribedTest checkTest)
        {
            if (checkTest != null)
            {
                var checkEmployeeLog = await employeeLogRepository.GetAllIncluding(n => n.Employee)
                                                                  .Where(x => x.Employee.Id == checkTest.Employee.Id)
                                                                  .FirstOrDefaultAsync();
                checkEmployeeLog.Availability = true;
                await employeeLogRepository.UpdateAsync(checkEmployeeLog);
            }
        }
    }
}
