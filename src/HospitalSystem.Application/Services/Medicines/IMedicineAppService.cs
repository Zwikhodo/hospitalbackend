using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HospitalSystem.Services.Medicines.DTO;
using System;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Medicines
{
    public interface IMedicineAppService : IApplicationService
    {
        Task<MedicineDTO> Create(MedicineDTO input);
        Task<MedicineDTO> Update(MedicineDTO input);
        Task<PagedResultDto<MedicineDTO>> GetAllAsync(PagedAndSortedResultRequestDto input);
        Task<PagedResultDto<MedicineDTO>> GetAsync(PagedAndSortedResultRequestDto input, Guid id);
        void DeleteAsync(Guid id);
    }
}
