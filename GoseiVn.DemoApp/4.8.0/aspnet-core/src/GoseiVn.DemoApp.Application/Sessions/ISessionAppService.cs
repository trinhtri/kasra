using System.Threading.Tasks;
using Abp.Application.Services;
using GoseiVn.DemoApp.Sessions.Dto;

namespace GoseiVn.DemoApp.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
