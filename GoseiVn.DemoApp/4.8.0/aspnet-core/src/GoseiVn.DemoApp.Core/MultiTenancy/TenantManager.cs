using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using GoseiVn.DemoApp.Authorization.Users;
using GoseiVn.DemoApp.Editions;

namespace GoseiVn.DemoApp.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
