using Abp.Domain.Entities.Auditing;
using HospitalSystem.Domain.RefLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain
{

    public class Procedure : FullAuditedEntity<Guid>
    {
        public virtual RefListProcedureType? Type { get; set; }
        public virtual string Description { get; set; }
        public virtual RefListOutcome? Outcome { get; set; }
        public virtual PrescribedTest PrescribedTest { get; set; }

    }
}
