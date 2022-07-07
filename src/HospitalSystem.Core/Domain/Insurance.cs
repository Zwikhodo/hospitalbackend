using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain
{
    public class Insurance:FullAuditedEntity<Guid>
    {
        public string Company { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal InsuranceCapAmount { get; set; }
       

    }
}
