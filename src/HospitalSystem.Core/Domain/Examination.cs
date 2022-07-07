using Abp.Domain.Entities.Auditing;
using HospitalSystem.Domain.RefLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain
{
    public class Examination:FullAuditedEntity<Guid>
    {
        public virtual float Weight { get; set; }
        public  virtual float Height { get; set; }
        public virtual string BloodPressure { get; set; }
        public virtual float Temperature { get; set; }
        public virtual string  PulseRate { get; set; }
        public virtual string  RespiratoryRate { get; set; }
        public virtual RefListOutcome Outcome { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Patient Patient{ get; set; }

    }
}
