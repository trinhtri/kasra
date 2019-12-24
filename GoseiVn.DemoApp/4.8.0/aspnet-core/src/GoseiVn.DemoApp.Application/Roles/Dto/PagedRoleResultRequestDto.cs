using Abp.Application.Services.Dto;

namespace GoseiVn.DemoApp.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

