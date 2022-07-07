using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HospitalSystem.Services.Rooms.DTO;
using System;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Rooms
{
    public interface IRoomAppService : IApplicationService
    {
        Task<RoomDTO> CreateAsync(RoomDTO input);
        Task<RoomDTO> UpdateRoom(RoomDTO input);
        Task<PagedResultDto<RoomDTO>> GetAllAsync(PagedAndSortedResultRequestDto input);
        Task<PagedResultDto<RoomDTO>> GetAsync(PagedAndSortedResultRequestDto input, Guid id);
        void DeleteAsync(Guid id);
    }
}
