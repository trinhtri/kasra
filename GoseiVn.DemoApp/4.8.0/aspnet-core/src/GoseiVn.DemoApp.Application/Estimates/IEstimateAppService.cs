using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GoseiVn.DemoApp.Estimates.Dto;
using GoseiVn.DemoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoseiVn.DemoApp.Estimates
{
    public interface IEstimateAppService : IApplicationService
    {
        Task Create(CreateEstimateDto input);

        Task<PagedResultDto<EstimateListDto>> GetAll(EstimateInput input);

        Task Edit(CreateEstimateDto input);

        Task Delete(int id);

        Task<CreateEstimateDto> Get(int id);

        Task<FileDto> GetEstimateToExcel(EstimateInput input);

        Task<List<StateDto>> GetAllState();

        Task<CreateEstimateDto> GetDataForEdit(int id);

        Images GetImageById(int id);

        Task Update(CreateEstimateDto input);
    }
}