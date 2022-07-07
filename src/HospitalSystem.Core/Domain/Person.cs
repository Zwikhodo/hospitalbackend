using Abp.Domain.Entities.Auditing;
using HospitalSystem.Authorization.Users;
using HospitalSystem.Domain.RefLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain
{
    public class Person:FullAuditedEntity<Guid>
    {
        public virtual string  FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string IdentificationNumber { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual RefListSex Sex { get; set; }
        public virtual string Address { get; set; }
        public virtual RefListEthnicity Ethnicity { get; set; }
    }

    public class Employee : Person
    {
        public virtual string EmployeeNumber { get; set; }
        public virtual RefListEmployeeType Type  { get; set; }
        public virtual RefListSpecialization? Specialization { get; set; }
        public User User { get; set; }

    }

    public class Patient: Person
    {
        public virtual string  PatientNumber { get; set; }
        public virtual RefListPatientType Type { get; set; }
    }
}
