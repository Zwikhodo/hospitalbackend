using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HospitalSystem.Authorization;

namespace HospitalSystem
{
    [DependsOn(
        typeof(HospitalSystemCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class HospitalSystemApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<HospitalSystemAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(HospitalSystemApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
