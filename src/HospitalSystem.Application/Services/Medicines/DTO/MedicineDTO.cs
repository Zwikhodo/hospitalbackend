using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HospitalSystem.Domain;
using System;

namespace HospitalSystem.Services.Medicines.DTO
{
    [AutoMap(typeof(Medicine))]
    public class MedicineDTO : EntityDto<Guid>
    {
        //public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Cost { get; set; }
    }
}
