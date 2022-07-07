using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using HospitalSystem.Authorization;
using HospitalSystem.Domain;
using HospitalSystem.Services.Medicines.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Medicines
{
    /// Endpoints for the medicine inventory
    [AbpAuthorize(PermissionNames.Pages_Overview)]
    public class MedicineAppService : ApplicationService
    {
        private readonly IRepository<Medicine, Guid> _medicineRepository;

        /// Injecting repository
        public MedicineAppService(IRepository<Medicine, Guid> medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        /// Creating medicine inventory
        public async Task<MedicineDTO> CreateAsync(MedicineDTO input)
        {
            var checkExists = _medicineRepository.GetAll().Where(n => n.Name == input.Name).FirstOrDefault();
            if (checkExists != null) throw new UserFriendlyException("Medicine with this Code already exists");
            var medicine = ObjectMapper.Map<Medicine>(input);   
            medicine.Code = $"MED{new Random().Next(10000)}_{medicine.Name.Substring(0,3)}";
            return ObjectMapper.Map<MedicineDTO>(await _medicineRepository
                .InsertAsync(ObjectMapper.Map<Medicine>(medicine)));
        }

        /// Deleting entries
        public async Task DeleteAsync(Guid id)
        {
          await _medicineRepository.DeleteAsync(id);
            
        }

        /// Get all entity entries
        public async Task<List<MedicineDTO>> GetAllAsync()
        {
            var entity = await _medicineRepository.GetAll().ToListAsync();
            return ObjectMapper.Map<List<MedicineDTO>>(entity);
        }

        /// Filter entries
        [HttpGet("name/{name}/code{code}")]
        public async Task<List<MedicineDTO>> GetMedicinesAsync(string name, string code)
        {
            var entryFilter = await _medicineRepository.GetAll()
                                                       .Where(x => x.Name == name || x.Code == code)
                                                       .ToListAsync();
            return ObjectMapper.Map<List<MedicineDTO>>(entryFilter);
        }

        /// Updating entity entries
        [HttpGet("code/{code}")]
        public async Task<MedicineDTO> UpdateAsync(MedicineDTO input, string code)
        {
            var entity = await _medicineRepository.GetAll()
                                                  .Where(n => n.Code == code)
                                                  .FirstOrDefaultAsync();
            if (entity == null) throw new UserFriendlyException("Data not found");
            ObjectMapper.Map(input, entity);
            await _medicineRepository.UpdateAsync(entity);
            return ObjectMapper.Map<MedicineDTO>(entity);
        }
    }
}
