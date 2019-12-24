using Abp.Domain.Repositories;
using GoseiVn.DemoApp.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoseiVn.DemoApp.Shared
{
    public class CommonService : DemoAppAppServiceBase, ICommonService
    {
        private readonly IRepository<Models.States> _stateRepository;
        public CommonService(IRepository<Models.States> stateRepository)
        {
            _stateRepository = stateRepository;
        }
        public async Task<List<CRMComboboxItem>> GetLookups(string type, int? parentId = null)
        {
            try
            {
                var result = new List<CRMComboboxItem>();

                switch (type)
                {
                    case "States":
                        result = await _stateRepository.GetAll()
                            .Select(x => new CRMComboboxItem { Value = x.Id, DisplayText = x.StateName })
                            .ToListAsync();
                        break;
                    default:
                        break;
                }
                return result;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}
