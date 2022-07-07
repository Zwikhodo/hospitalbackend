using HospitalSystem.Domain.RefLists;
using HospitalSystem.Services.Persons.DTO;

namespace HospitalSystem.Services.Patients.DTO
{
    public class PatientDTO : PersonDTO
    {
        public virtual string PatientNumber { get; set; }
        public virtual RefListPatientType Type { get; set; }
    }
}
