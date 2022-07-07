using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace HospitalSystem.Authorization
{
    public class HospitalSystemAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Overview, L("Overview"));
            context.CreatePermission(PermissionNames.Pages_Examination, L("Examination"));
            context.CreatePermission(PermissionNames.Pages_Admission, L("Admission"));
            context.CreatePermission(PermissionNames.Pages_PatientReport, L("PatientReport"));
            context.CreatePermission(PermissionNames.Pages_BillCreate, L("BillCreate"));
            context.CreatePermission(PermissionNames.Pages_UpdateInventory, L("UpdateInventory"));
            context.CreatePermission(PermissionNames.Pages_CreateAppointment, L("CreateAppointment"));
            context.CreatePermission(PermissionNames.Pages_PrescribedTests, L("PrescribedTests"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, HospitalSystemConsts.LocalizationSourceName);
        }
    }
}
