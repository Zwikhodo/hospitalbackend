using Abp.Application.Services.Dto;
using HospitalSystem.Domain.RefLists;
using System;


namespace HospitalSystem.Services.Appointments.DTO
{
    public class AppointmentDTO: EntityDto<Guid>
    {
        public virtual string AppointmentNumber { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual int Duration { get; set; }
        public virtual RefListAppointmentStatus Status { get; set; }
        public virtual Guid PatientId { get; set; }
        public virtual Guid EmployeeId { get; set; }
    }
}
