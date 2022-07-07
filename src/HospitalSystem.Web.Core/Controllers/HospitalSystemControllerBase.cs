using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystem.Controllers
{
    public abstract class HospitalSystemControllerBase: AbpController
    {
        protected HospitalSystemControllerBase()
        {
            LocalizationSourceName = HospitalSystemConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
