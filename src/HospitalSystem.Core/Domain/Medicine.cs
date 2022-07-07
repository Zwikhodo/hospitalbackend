using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain
{
    public class Medicine:FullAuditedEntity<Guid>
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Cost { get; set; }
    }
}
