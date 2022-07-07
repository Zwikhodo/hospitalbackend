using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using HospitalSystem.Authorization;
using HospitalSystem.Domain;
using HospitalSystem.Domain.RefLists;
using HospitalSystem.Services.Procedures.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Procedures
{
    /// Endpoints for doctor procedures
    [AbpAuthorize(PermissionNames.Pages_Procedures)]
    public class ProcedureAppService : ApplicationService
    {
        private readonly IRepository<PrescribedTest, Guid> _prescribedTestRepository;
        private readonly IRepository<Procedure, Guid> _procedureRepository;

        /// Injecting repositories
        public ProcedureAppService(IRepository<Procedure, Guid> procedureRepository, 
                                   IRepository<PrescribedTest, Guid> prescribedTestRepository)
        {
            _prescribedTestRepository = prescribedTestRepository;
            _procedureRepository = procedureRepository;
        }

        /// Creating procedures
        public async Task<ProcedureDTO> CreateAsync(ProcedureDTO input)
        {
            var checkPrescribedTest = await _prescribedTestRepository.GetAll()
                                                                     .Where(n => n.Id == input.PrescribedTestId)
                                                                     .FirstOrDefaultAsync();

            switch (checkPrescribedTest.Outcome)
            {
                case RefListOutcome.Conclusive:
                    throw new UserFriendlyException(@"No procedure is required for this patient as 
                                                    the results of the last test were conclusive.");
                case RefListOutcome.MoreTestsRequired:
                    throw new UserFriendlyException("More tests need to be conducted before a procedure is considered.");
                default:
                    break;
            }
            var entity = ObjectMapper.Map<Procedure>(input);
            entity.PrescribedTest = checkPrescribedTest;
            await _procedureRepository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<ProcedureDTO>(entity);
        }

        /// Deleting Entries 
        public async Task DeleteAsync(Guid id)
        {
            await _prescribedTestRepository.DeleteAsync(id);
        }

        /// Filter entity entries
       
        public async Task<List<ProcedureDTO>> GetProceduresAsync(string patientNumber, int outcome, int type)
        {
            var entityFilter = await _procedureRepository.GetAllIncluding(n => n.PrescribedTest)
                                                       .Where(x => x.PrescribedTest.Admission.Patient.PatientNumber == patientNumber 
                                                                   ||(int)x.Outcome == outcome 
                                                                   ||(int)x.Type == type)
                                                       .ToListAsync();

            return ObjectMapper.Map<List<ProcedureDTO>>(entityFilter);
        }

        /// Get all entity entries
        public async Task<List<ProcedureDTO>> GetAllAsync()
        {
            var entries = await _procedureRepository.GetAllIncluding(n => n.PrescribedTest).ToListAsync();
            return ObjectMapper.Map<List<ProcedureDTO>>(entries);
        }

        /// Update entity entries
        public async Task<ProcedureDTO> UpdateAsync(ProcedureDTO input, string patientNumber)
        {
            var entity = await _procedureRepository.GetAllIncluding(n => n.PrescribedTest)
                                                      .Where(n => n.PrescribedTest.Admission.Patient.PatientNumber == patientNumber)
                                                      .FirstOrDefaultAsync();
            if (entity == null)throw new UserFriendlyException("Data not found");
            ObjectMapper.Map(input, entity);
            await _procedureRepository.UpdateAsync(entity);
            return ObjectMapper.Map<ProcedureDTO>(entity);
        }
    }
}
