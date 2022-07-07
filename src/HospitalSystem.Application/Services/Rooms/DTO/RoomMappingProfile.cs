using AutoMapper;
using HospitalSystem.Domain;

namespace HospitalSystem.Services.Rooms.DTO
{
    public class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {
            CreateMap<Room, RoomDTO>();
            CreateMap<RoomDTO, Room>();
        }
    }
}
