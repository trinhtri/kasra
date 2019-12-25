using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GoseiVn.DemoApp.Estimates.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoseiVn.DemoApp.Estimates
{
    public interface IEstimateAppService:IApplicationService
    {
        Task<int> Create(CreateEstimateDto input);

        Task<PagedResultDto<EstimateListDto>> GetAll(EstimateInput input);

        Task Edit(CreateEstimateDto input);

        Task Delete(int id);

        Task<CreateEstimateDto> Get(int id);

        Task<FileDto> GetEstimateToExcel(EstimateInput input);
    }
}
