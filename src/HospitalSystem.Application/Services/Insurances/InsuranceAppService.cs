using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using HospitalSystem.Authorization;
using HospitalSystem.Domain;
using HospitalSystem.Services.Insurances.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Insurances
{
    /// Endpoints for insurance schemes/policies
    [AbpAuthorize(PermissionNames.Pages_BillCreate)]
    public class InsuranceAppService : ApplicationService
    {
        private readonly IRepository<Insurance, Guid> _insuranceRepository;

        /// Injecting repositories
        public InsuranceAppService(IRepository<Insurance, Guid> insuranceRepository)
        {
            _insuranceRepository = insuranceRepository;

        }

        /// Creating insurance schemes
        public async Task<InsuranceDTO> Create(InsuranceDTO input)
        {
            var checkExists = _insuranceRepository.GetAll().Where(n => n.RegistrationNumber == input.RegistrationNumber).FirstOrDefault();
            if (checkExists != null) throw new UserFriendlyException("Insurance scheme already exists");
            return ObjectMapper.Map<InsuranceDTO>(await _insuranceRepository
                .InsertAsync(ObjectMapper.Map<Insurance>(input)));
        }

        /// Deleting entity entries
        public async Task DeleteAsync(Guid id)
        {
            await _insuranceRepository.DeleteAsync(id);
            
        }

        /// Getting all entity entries
        public async Task<List<InsuranceDTO>> GetAllAsync()
        {
            var entity = await _insuranceRepository.GetAll().ToListAsync();
            return ObjectMapper.Map<List<InsuranceDTO>>(entity);
        }

        /// filtering entity entries
        [HttpGet("registration-number/{registrationNumber}")]
        public async Task<List<InsuranceDTO>> GetInsurancesAsync(string registrationNumber)
        {
            var getFilter = await _insuranceRepository.GetAll()
                                                      .Where(x => x.RegistrationNumber == registrationNumber)
                                                      .ToListAsync();
            return ObjectMapper.Map<List<InsuranceDTO>>(getFilter);
        }

        /// Updating entity entries
        [HttpPut("registration-number/{registrationNumber}")]
        public async Task<InsuranceDTO> UpdateAsync(InsuranceDTO input, string registrationNumber)
        {
            var entity = await _insuranceRepository.GetAll()
                                                   .Where(x => x.RegistrationNumber == registrationNumber)
                                                   .FirstOrDefaultAsync();
            if (entity == null) throw new UserFriendlyException("Data not found.");
            ObjectMapper.Map(input, entity);
            await _insuranceRepository.UpdateAsync(entity);
            return ObjectMapper.Map<InsuranceDTO>(entity);
        }
    }
}
