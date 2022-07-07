using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain
{
    public class EmployeeLog: FullAuditedEntity<Guid>
    {
        public virtual Employee Employee { get; set; }
        public virtual DateTime CheckIn { get; set; }
        public virtual DateTime? CheckOut { get; set; }
        public virtual bool Availability { get; set; }
    }
}
