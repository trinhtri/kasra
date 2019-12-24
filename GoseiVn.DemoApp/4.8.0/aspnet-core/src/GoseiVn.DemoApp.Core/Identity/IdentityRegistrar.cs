using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using GoseiVn.DemoApp.Authorization;
using GoseiVn.DemoApp.Authorization.Roles;
using GoseiVn.DemoApp.Authorization.Users;
using GoseiVn.DemoApp.Editions;
using GoseiVn.DemoApp.MultiTenancy;

namespace GoseiVn.DemoApp.Identity
{
    public static class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant, User, Role>()
                .AddAbpTenantManager<TenantManager>()
                .AddAbpUserManager<UserManager>()
                .AddAbpRoleManager<RoleManager>()
                .AddAbpEditionManager<EditionManager>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>()
                .AddAbpLogInManager<LogInManager>()
                .AddAbpSignInManager<SignInManager>()
                .AddAbpSecurityStampValidator<SecurityStampValidator>()
                .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddPermissionChecker<PermissionChecker>()
                .AddDefaultTokenProviders();
        }
    }
}
