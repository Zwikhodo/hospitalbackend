using Abp.Domain.Entities.Auditing;
using HospitalSystem.Domain.RefLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain
{
    public class Appointment:FullAuditedEntity<Guid>
    {
        public virtual string AppointmentNumber { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual RefListAppointmentStatus Status { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Employee Employee{ get; set; }


    }
}
 