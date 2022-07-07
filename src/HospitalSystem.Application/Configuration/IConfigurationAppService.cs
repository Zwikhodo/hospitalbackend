using System.Threading.Tasks;
using HospitalSystem.Configuration.Dto;

namespace HospitalSystem.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
