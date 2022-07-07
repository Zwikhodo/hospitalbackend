using Abp.Dependency;
using HospitalSystem.Domain;
using HospitalSystem.Services.Examinations.DTO;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Examinations.Helpers
{
    public interface IExaminationHelper: ISingletonDependency
    {
        Task PostAdmission(ExaminationDTO input, Patient patient);
        Task AssignEmployee(ExaminationDTO input, Examination vitals);
    }
}
