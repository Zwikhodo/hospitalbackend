using Abp.Authorization;
using HospitalSystem.Authorization.Roles;
using HospitalSystem.Authorization.Users;

namespace HospitalSystem.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
