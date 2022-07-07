using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain.RefLists
{
    public enum RefListPrescribedTest: int
    {
        Biopsy = 1,
        Colonoscopy = 2,
        CTScan = 3,
        Electrocardiogram = 4,
        Elevtroencephalogram = 5,
        Gastroscopy = 6,
        EyeTest = 7,
        HearingTest = 8,
        MRIScan = 9,
        PETScan = 10,
        Ultrasound = 11,
        Xray = 12
    }
}
