using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using HospitalSystem.Configuration.Dto;

namespace HospitalSystem.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : HospitalSystemAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
