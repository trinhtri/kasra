using Abp.Authorization;
using GoseiVn.DemoApp.Authorization.Roles;
using GoseiVn.DemoApp.Authorization.Users;

namespace GoseiVn.DemoApp.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
