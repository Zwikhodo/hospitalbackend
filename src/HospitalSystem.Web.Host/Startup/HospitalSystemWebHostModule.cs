using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HospitalSystem.Configuration;

namespace HospitalSystem.Web.Host.Startup
{
    [DependsOn(
       typeof(HospitalSystemWebCoreModule))]
    public class HospitalSystemWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public HospitalSystemWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HospitalSystemWebHostModule).GetAssembly());
        }
    }
}
