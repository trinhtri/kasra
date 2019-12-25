using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using GoseiVn.DemoApp.Authorization;

namespace GoseiVn.DemoApp
{
    [DependsOn(
        typeof(DemoAppCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class DemoAppApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<DemoAppAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(DemoAppApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}
