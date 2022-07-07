using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HospitalSystem.Domain;
using HospitalSystem.Domain.RefLists;
using System;

namespace HospitalSystem.Services.Rooms.DTO
{
    [AutoMap(typeof(Room))]
    public class RoomDTO : EntityDto<Guid>
    {
        public string RoomNumber { get; set; }
        public RefListRoomType Type { get; set; }
        public int Capacity { get; set; }
        public int AvailableBeds { get; set; }
        public decimal CostPerNight { get; set; }
    }
}
