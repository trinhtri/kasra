using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IO;
using Abp.UI;
using GoseiVn.DemoApp.Estimates.Dto;
using GoseiVn.DemoApp.Estimates.Exporting;
using GoseiVn.DemoApp.IO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        private readonly EstimateExcelExporter _estimateExcelExporter;
        private readonly IAppFolders _appFolders;

        public EstimateAppService(
            IRepository<Models.Estimates> estimateRepository,
            IRepository<Models.Images> imageRepository,
            IRepository<Models.States> stateRepository,
            EstimateExcelExporter estimateExcelExporter,
            IAppFolders appFolders
                     )
        {
            _estimateRepository = estimateRepository;
            _imageRepository = imageRepository;
            _stateRepository = stateRepository;
            _estimateExcelExporter = estimateExcelExporter;
        }
        public async Task<int> Create(CreateEstimateDto input)
        {
            var estimate = ObjectMapper.Map<Models.Estimates>(input);
            await _estimateRepository.InsertAsync(estimate);
            await CurrentUnitOfWork.SaveChangesAsync();
            //image
            if(input.ListFileName.Count > 0)
            {
                foreach(var item in input.ListFileName)
                {
                    var image = ObjectMapper.Map<Models.Images>(item);
                    if (item.ImageName != null)
                    {
                        //Delete old document file
                        AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.AttachmentsFolder, item.ImageName);
                        var sourceFile = Path.Combine(_appFolders.TempFileUploadFolder, item.ImageName);
                        var destFile = Path.Combine(_appFolders.AttachmentsFolder, item.ImageName);
                        System.IO.File.Move(sourceFile, destFile);
                        var filePath = Path.Combine(_appFolders.AttachmentsFolder, item.ImageName);
                        image.ImageUrl = filePath;
                        image.ImageName = filePath;
                        image.ImageSize = item.ImageSize;
                        image.Estimates.Id = estimate.Id;
                    }

                    await _imageRepository.InsertAsync(image);
                }
            }

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
                input.LastName = Regex.Replace(input.LastName.Trim(), @"\s+", " ");
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

        public async Task<FileDto> GetEstimateToExcel(EstimateInput input)
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
            return _estimateExcelExporter.ExportToFile(estimates);
        }
    }
}
