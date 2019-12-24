using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace GoseiVn.DemoApp.Controllers
{
    public abstract class DemoAppControllerBase: AbpController
    {
        protected DemoAppControllerBase()
        {
            LocalizationSourceName = DemoAppConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
