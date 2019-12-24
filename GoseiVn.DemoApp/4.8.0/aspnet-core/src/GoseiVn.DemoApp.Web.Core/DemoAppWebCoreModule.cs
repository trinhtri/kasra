using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using GoseiVn.DemoApp.Authentication.JwtBearer;
using GoseiVn.DemoApp.Configuration;
using GoseiVn.DemoApp.EntityFrameworkCore;
using System.IO;
using Abp.IO;

namespace GoseiVn.DemoApp
{
    [DependsOn(
         typeof(DemoAppApplicationModule),
         typeof(DemoAppEntityFrameworkModule),
         typeof(AbpAspNetCoreModule)
        ,typeof(AbpAspNetCoreSignalRModule)
     )]
    public class DemoAppWebCoreModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public DemoAppWebCoreModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                DemoAppConsts.ConnectionStringName
            );

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(DemoAppApplicationModule).GetAssembly()
                 );

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DemoAppWebCoreModule).GetAssembly());
        }
        public override void PostInitialize()
        {
            SetAppFolders();
        }
        private void SetAppFolders()
        {
            var appFolders = IocManager.Resolve<AppFolders>();

            appFolders.TempFileDownloadFolder = Path.Combine(_env.WebRootPath, $"Temp{Path.DirectorySeparatorChar}Downloads");
            appFolders.TempFileUploadFolder = Path.Combine(_env.WebRootPath, $"Temp{Path.DirectorySeparatorChar}Uploads");
            appFolders.AttachmentsFolder = Path.Combine(_env.WebRootPath, $"Temp{Path.DirectorySeparatorChar}Images");
            DirectoryHelper.CreateIfNotExists(appFolders.TempFileDownloadFolder);
            DirectoryHelper.CreateIfNotExists(appFolders.TempFileUploadFolder);
            DirectoryHelper.CreateIfNotExists(appFolders.AttachmentsFolder);
        }
    }
}
