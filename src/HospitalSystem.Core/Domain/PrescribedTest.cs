using Abp.Domain.Entities.Auditing;
using HospitalSystem.Domain.RefLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain
{
    public class PrescribedTest:FullAuditedEntity<Guid>
    {
        public virtual RefListPrescribedTest? Type { get; set; }
        public virtual RefListOutcome? Outcome { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Admission Admission { get; set; }
    }
}
