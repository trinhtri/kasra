using Abp.Application.Services;
using GoseiVn.DemoApp.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoseiVn.DemoApp.Shared
{
    public interface ICommonService: IApplicationService
    {
        Task<List<CRMComboboxItem>> GetLookups(string type, int? parentId = null);
    }
}
