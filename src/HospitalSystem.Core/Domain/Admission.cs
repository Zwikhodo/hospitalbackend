using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HospitalSystem.Domain
{
    public class Admission : FullAuditedEntity<Guid>
    {
        public virtual DateTime Date{ get; set; }
        public virtual DateTime DischargeDate { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Room Room{ get; set; }

    }
}
