using System.Threading.Tasks;
using GoseiVn.DemoApp.Configuration.Dto;

namespace GoseiVn.DemoApp.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
