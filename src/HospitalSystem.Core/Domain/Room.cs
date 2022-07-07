using Abp.Domain.Entities.Auditing;
using HospitalSystem.Domain.RefLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain
{
    public class Room:FullAuditedEntity<Guid>
    {
        public virtual string RoomNumber { get; set; }
        public virtual RefListRoomType Type { get; set; }
        public virtual int Capacity { get; set; }
        public virtual int AvailableBeds { get; set; }
        public virtual decimal CostPerNight { get; set; }

    }
}
