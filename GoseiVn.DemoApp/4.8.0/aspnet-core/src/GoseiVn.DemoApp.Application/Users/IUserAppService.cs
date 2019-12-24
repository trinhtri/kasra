using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GoseiVn.DemoApp.Roles.Dto;
using GoseiVn.DemoApp.Users.Dto;

namespace GoseiVn.DemoApp.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
