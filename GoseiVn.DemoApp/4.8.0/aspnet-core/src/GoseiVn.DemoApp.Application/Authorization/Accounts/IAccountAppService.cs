using System.Threading.Tasks;
using Abp.Application.Services;
using GoseiVn.DemoApp.Authorization.Accounts.Dto;

namespace GoseiVn.DemoApp.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
