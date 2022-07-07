using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HospitalSystem.Configuration;
using HospitalSystem.EntityFrameworkCore;
using HospitalSystem.Migrator.DependencyInjection;

namespace HospitalSystem.Migrator
{
    [DependsOn(typeof(HospitalSystemEntityFrameworkModule))]
    public class HospitalSystemMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public HospitalSystemMigratorModule(HospitalSystemEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(HospitalSystemMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                HospitalSystemConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HospitalSystemMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
