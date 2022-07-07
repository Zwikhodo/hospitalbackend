using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain.RefLists
{
    public enum RefListOutcome: int
    {
        Conclusive = 1,
        MoreTestsRequired = 2,
        ProcedureRequired = 3
    }
}
