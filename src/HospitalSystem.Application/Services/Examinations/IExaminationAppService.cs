using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HospitalSystem.Services.Examinations.DTO;
using System;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Examinations
{
    public interface IExaminationAppService:IApplicationService
    {
        Task<ExaminationDTO> CreateAsync(ExaminationDTO input);
        Task<ExaminationDTO> UpdateTest(ExaminationDTO input);
        Task<PagedResultDto<ExaminationDTO>> GetAllAsync(PagedAndSortedResultRequestDto input);
        Task<PagedResultDto<ExaminationDTO>> GetAsync(PagedAndSortedResultRequestDto input, Guid id);
        void DeleteAsync(Guid id);
    }
}
