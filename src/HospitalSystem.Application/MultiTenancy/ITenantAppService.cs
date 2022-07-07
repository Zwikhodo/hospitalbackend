using Abp.Application.Services;
using HospitalSystem.MultiTenancy.Dto;

namespace HospitalSystem.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

