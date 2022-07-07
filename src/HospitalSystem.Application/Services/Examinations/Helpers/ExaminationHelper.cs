using Abp.Dependency;
using Abp.Domain.Repositories;
using HospitalSystem.Domain;
using HospitalSystem.Domain.RefLists;
using HospitalSystem.Services.Examinations.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Examinations.Helpers
{
    /// contains extensions and helpers which assist the Examination app services 
    public class ExaminationHelper : IExaminationHelper
    {
        private readonly IRepository<Admission, Guid> admissionRepository;
        private readonly IRepository<Room, Guid> roomRepository;
        private readonly IRepository<Appointment, Guid> appointmentRepository;
        private readonly IRepository<EmployeeLog, Guid> employeeLogRepository;



        /// injecting required repositories
        public ExaminationHelper()
        {
            admissionRepository = IocManager.Instance.Resolve<IRepository<Admission, Guid>>();
            roomRepository = IocManager.Instance.Resolve<IRepository<Room, Guid>>();
            employeeLogRepository = IocManager.Instance.Resolve<IRepository<EmployeeLog, Guid>>();
            appointmentRepository = IocManager.Instance.Resolve<IRepository<Appointment, Guid>>();

        }

        /// assigning an employee to conduct the examination
        public async Task AssignEmployee(ExaminationDTO input, Examination vitals)
        {
            var checkAppointment = await appointmentRepository.GetAllIncluding(x => x.Employee)
                                                                .Where(n => n.Patient.Id == input.PatientId)
                                                                .FirstOrDefaultAsync();
            if (checkAppointment != null)
            {
                vitals.Employee = checkAppointment.Employee;
            }
            else
            {
                var checkEmployee = employeeLogRepository.GetAllIncluding(n => n.Employee)
                                                         .Where(n => n.Availability == true && n.Employee.Type == RefListEmployeeType.Nurse)
                                                         .FirstOrDefault();
                vitals.Employee = checkEmployee.Employee;
                checkEmployee.Availability = false;
                await employeeLogRepository.UpdateAsync(checkEmployee);
            }
        }

        /// Posting an admission entry based on the examination test results
        public async Task PostAdmission(ExaminationDTO input, Patient patient)
        {
            if (input.Outcome == RefListOutcome.MoreTestsRequired)
            {
                var admission = new Admission()
                {
                    Date = DateTime.Now,
                    Patient = patient,
                    Room = await roomRepository.GetAll().Where(n => n.AvailableBeds > 0).FirstOrDefaultAsync()
                };
                await admissionRepository.InsertAsync(admission);
            }
        }
    }
}
