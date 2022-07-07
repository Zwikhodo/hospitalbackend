using Abp.Application.Services;
using HospitalSystem.Services.Procedures.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Procedures
{
    public interface IProcedureAppService : IApplicationService
    {
        Task<ProcedureDTO> Create(ProcedureDTO input);
        Task<ProcedureDTO> Update(ProcedureDTO input);
        Task<List<ProcedureDTO>> GetAll();
        Task<List<ProcedureDTO>> Get(Guid id);
        void DeleteAsync(Guid id);
    }
}
