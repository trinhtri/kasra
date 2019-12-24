using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using GoseiVn.DemoApp.Configuration.Dto;

namespace GoseiVn.DemoApp.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : DemoAppAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
