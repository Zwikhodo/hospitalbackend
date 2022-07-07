using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using HospitalSystem.Domain;
using HospitalSystem.Services.Rooms.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Rooms
{
    ///Endpoints for assigning rooms
    public class RoomAppService: ApplicationService
    {
        private readonly IRepository<Room, Guid> _roomRepository;

        /// Injecting repositories
        public RoomAppService(IRepository<Room, Guid> roomRepository)
        {
            _roomRepository = roomRepository;
        }

        /// Creating rooms
        public async Task<RoomDTO> CreateAsync(RoomDTO input)
        {
            var checkExists = _roomRepository.FirstOrDefault(n => n.RoomNumber == input.RoomNumber);
            if (checkExists != null) throw new UserFriendlyException("Room Already Exists In the Hospital Database");
            var mapped = ObjectMapper.Map<Room>(input);
            var result = await _roomRepository.InsertAsync(mapped);
            return ObjectMapper.Map<RoomDTO>(result);
        }

        /// Deleting entries
        public async Task DeleteAsync(Guid id)
        {
            await _roomRepository.DeleteAsync(id);
        }

        /// Getting all entity entries
        public async Task<List<RoomDTO>> GetAllAsync()
        {
            var entries = await _roomRepository.GetAll().ToListAsync();
            return ObjectMapper.Map<List<RoomDTO>>(entries);
        }

        /// Filtering entities
        [HttpGet("room-number/{roomNumber}/room-type/{roomType}")]
        public async Task<List<RoomDTO>> GetRoomsAsync(string roomNumber, int roomType)
        {
            var entityFilter = await _roomRepository.GetAll()
                                                    .Where(x => (int)x.Type == roomType || x.RoomNumber == roomNumber)
                                                    .ToListAsync();
            return ObjectMapper.Map<List<RoomDTO>>(entityFilter);
        }

        /// Updating entity entry
        [HttpPut("room-number/{roomNumber}")]
        public async Task<RoomDTO> UpdateAsync(RoomDTO input, string roomNumber)
        {
            var entity = await _roomRepository.GetAllIncluding(n=> n.Id)
                                              .Where(n => n.RoomNumber == roomNumber)
                                              .FirstOrDefaultAsync();
            if (entity == null) throw new UserFriendlyException("Data not found");
            ObjectMapper.Map(input, entity);
            await _roomRepository.UpdateAsync(entity);
            return ObjectMapper.Map<RoomDTO>(entity);
        }

    }
}
