using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GoseiVn.DemoApp.MultiTenancy.Dto;

namespace GoseiVn.DemoApp.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

