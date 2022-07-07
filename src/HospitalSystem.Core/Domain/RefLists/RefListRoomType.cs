using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain.RefLists
{
    public enum RefListRoomType: int
    {
        ConsultingRoom = 1,
        Casualty = 2,
        DeliveryRoom = 3,
        HighDependencyUnit = 4,
        IntensiveCareUnit = 5,
        MaternityWard = 6,
        OperatingRoom = 7,
        PaddedCell = 8,
        Surgery = 9,
        Ward = 10
    }
}
