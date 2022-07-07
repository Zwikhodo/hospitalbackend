using Abp.Application.Services;
using HospitalSystem.Services.PrescribedTests.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalSystem.Services.PrescribedTests
{
    public interface IPrescribedTestAppService: IApplicationService
    {
        Task<PrescribedTestDTO> Create(PrescribedTestDTO input);
        Task<PrescribedTestDTO> Update(PrescribedTestDTO input);
        Task<List<PrescribedTestDTO>> GetAll();
        Task<List<PrescribedTestDTO>> Get(Guid id);
        void DeleteAsync(Guid id);
    }
}
