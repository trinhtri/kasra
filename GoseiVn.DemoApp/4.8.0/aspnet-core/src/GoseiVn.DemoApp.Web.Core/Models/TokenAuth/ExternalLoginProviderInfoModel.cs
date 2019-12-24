using Abp.AutoMapper;
using GoseiVn.DemoApp.Authentication.External;

namespace GoseiVn.DemoApp.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
