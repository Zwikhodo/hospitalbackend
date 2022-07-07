using System.Threading.Tasks;
using Abp.Application.Services;
using HospitalSystem.Sessions.Dto;

namespace HospitalSystem.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
