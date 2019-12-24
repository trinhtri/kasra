using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using GoseiVn.DemoApp.Estimates.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoseiVn.DemoApp.Estimates
{
    public class EstimateAppService : DemoAppAppServiceBase, IEstimateAppService
    {
        private readonly IRepository<Models.Estimates> _estimateRepository;
        private readonly IRepository<Models.Images> _imageRepository;
        private readonly IRepository<Models.States> _stateRepository;
        public EstimateAppService(
            IRepository<Models.Estimates> estimateRepository,
             IRepository<Models.Images> imageRepository,
        IRepository<Models.States> stateRepository
                     )
        {
            _estimateRepository = estimateRepository;
            _imageRepository = imageRepository;
            _stateRepository = stateRepository;
        }
        public async Task<int> Create(CreateEstimateDto input)
        {
            var estimate = ObjectMapper.Map<Models.Estimates>(input);
            await _estimateRepository.InsertAsync(estimate);
            await CurrentUnitOfWork.SaveChangesAsync();
            return estimate.Id;
        }

        public async Task Delete(int id)
        {
            await _estimateRepository.DeleteAsync(id);
        }

        public async Task Edit(CreateEstimateDto input)
        {
            var estimate = await _estimateRepository.FirstOrDefaultAsync(input.Id);
            ObjectMapper.Map(input, estimate);
        }

        public async Task<CreateEstimateDto> Get(int id)
        {
            var estimate = await _estimateRepository.FirstOrDefaultAsync(id);
            var dto = ObjectMapper.Map<CreateEstimateDto>(estimate);
            return dto;
        }

        public async Task<PagedResultDto<EstimateListDto>> GetAll(EstimateInput input)
        {
            if (!input.Firstname.IsNullOrEmpty())
            {
                input.Firstname = Regex.Replace(input.Firstname.Trim(), @"\s+", " ");
            }

            if (!input.LastName.IsNullOrEmpty())
            {
                input.Firstname = Regex.Replace(input.LastName.Trim(), @"\s+", " ");
            }

            var estimates = _estimateRepository.GetAll().Include(x => x.States)
                .Select(x => new EstimateListDto
                {
                    Id = x.Id,
                    LastName = x.LastName,
                    Firstname = x.Firstname,
                    Address = x.AddressLine1 + "-" + x.AddressLine2 + "-" + x.City + "-" + x.States.StateName,
                    Mobile = x.Mobile,
                    TotalAmount = x.TotalAmount
                }).WhereIf(input.Firstname != null, x => x.Firstname.Contains(input.Firstname))
                .WhereIf(input.LastName != null, x => x.LastName.Contains(input.LastName))
                .ToList();
            var pageOfResults = estimates
              .Skip(input.SkipCount)
              .Take(input.MaxResultCount)
              .ToList();

            return new PagedResultDto<EstimateListDto>(estimates.Count, pageOfResults);
        }
    }
}
