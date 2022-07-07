using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain
{
    public class PatientReport:FullAuditedEntity<Guid>
    {
        public virtual string Diagnosis { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Examination Examination { get; set; }
        public virtual PrescribedTest PrescribedTest { get; set; }
        public virtual Procedure Procedure { get; set; }
    }
}
