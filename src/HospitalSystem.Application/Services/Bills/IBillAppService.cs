using Abp.Application.Services;
using HospitalSystem.Services.Bills.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Bills
{
    public interface IBillAppService : IApplicationService
    {
        Task<BillDTO> Create(BillDTO input);
        Task<BillDTO> Update(BillDTO input);
        Task<List<BillDTO>> GetAll();
        Task<List<BillDTO>> Get(Guid id);
        void DeleteAsync(Guid id);
    }
}
