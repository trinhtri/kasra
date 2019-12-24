using Abp.MultiTenancy;
using GoseiVn.DemoApp.Authorization.Users;

namespace GoseiVn.DemoApp.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
