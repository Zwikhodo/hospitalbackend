using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain
{
    public class Bill:FullAuditedEntity<Guid>
    {
        public virtual decimal MedicineCharge { get; set; }
        public virtual decimal EmployeeCharge { get; set; }
        public virtual decimal PrescribedTestCharge { get; set; }
        public virtual decimal ProcedureCharge { get; set; }
        public virtual decimal CostPerNight { get; set; }
        public virtual decimal TotalBill { get; set; }
        public PatientReport PatientReport { get; set; }
    }
}
