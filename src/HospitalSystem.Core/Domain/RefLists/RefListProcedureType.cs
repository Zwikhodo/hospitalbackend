using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain.RefLists
{
    public enum RefListProcedureType: int
    {
        Appendectomy = 1,
        Cholecystectomy = 2,
        Hemorrhoidectomy = 3,
        Hysterectomy = 4,
        Hysteroscopy = 5,
        Mastectomy = 6,
        Prostatectomy = 7,
        Tonsillectomy = 8,
        CesareanSection = 9
    }
}
