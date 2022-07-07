using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HospitalSystem.EntityFrameworkCore;
using HospitalSystem.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace HospitalSystem.Web.Tests
{
    [DependsOn(
        typeof(HospitalSystemWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class HospitalSystemWebTestModule : AbpModule
    {
        public HospitalSystemWebTestModule(HospitalSystemEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HospitalSystemWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(HospitalSystemWebMvcModule).Assembly);
        }
    }
}