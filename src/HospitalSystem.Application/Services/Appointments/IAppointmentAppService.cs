using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HospitalSystem.Services.Appointments.DTO;
using System;
using System.Threading.Tasks;

namespace HospitalSystem.Services.Appointments
{
    public interface IAppointmentAppService: IApplicationService
    {
        Task<AppointmentDTO> CreateAppointment(AppointmentDTO input);
        Task<AppointmentDTO> UpdateAppointment(AppointmentDTO input);
        Task<PagedResultDto<AppointmentDTO>> GetAll(PagedAndSortedResultRequestDto input);
        Task<PagedResultDto<AppointmentDTO>> GetPatientAppointment(PagedAndSortedResultRequestDto input, Guid id);
        void DeleteAsync(Guid id);
    }
}
