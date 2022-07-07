using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HospitalSystem.Domain;
using HospitalSystem.Domain.RefLists;
using System;

namespace HospitalSystem.Services.Persons.DTO
{
    [AutoMapFrom(typeof(Person))]
    public class PersonDTO: EntityDto<Guid>
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string IdentificationNumber { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual RefListSex Sex { get; set; }
        public virtual string Address { get; set; }
        public virtual RefListEthnicity Ethnicity { get; set; }
    }
}
