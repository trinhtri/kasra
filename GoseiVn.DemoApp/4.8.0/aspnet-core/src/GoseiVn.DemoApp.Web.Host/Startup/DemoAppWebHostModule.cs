using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using GoseiVn.DemoApp.Configuration;

namespace GoseiVn.DemoApp.Web.Host.Startup
{
    [DependsOn(
       typeof(DemoAppWebCoreModule))]
    public class DemoAppWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public DemoAppWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DemoAppWebHostModule).GetAssembly());
        }
    }
}
