using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HospitalSystem.Domain
{
    public class Prescription : FullAuditedEntity<Guid> 
    {
        public virtual PatientReport PatientReport { get; set; }
        public virtual  Medicine  Medicine{ get; set; }
    }
}
