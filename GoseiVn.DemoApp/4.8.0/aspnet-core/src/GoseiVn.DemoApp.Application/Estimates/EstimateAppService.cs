using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using GoseiVn.DemoApp.Estimates.Dto;
using GoseiVn.DemoApp.Estimates.Exporting;
using GoseiVn.DemoApp.IO;
using GoseiVn.DemoApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            _appFolders = appFolders;
        }

        public async Task Create(CreateEstimateDto input)
        {
            try
            {
                var estimate = ObjectMapper.Map<Models.Estimates>(input);
                var estimatesId = await _estimateRepository.InsertAndGetIdAsync(estimate);
                //image
                if (input.ListFileName.Count > 0)
                {
                    foreach (var item in input.ListFileName)
                    {
                        var image = ObjectMapper.Map<Models.Images>(item);
                        if (item.ImageName != null)
                        {
                            //Delete old document file
                            AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.AttachmentsFolder, item.ImageUrl);
                            var sourceFile = Path.Combine(_appFolders.TempFileDownloadFolder, item.ImageUrl);
                            var destFile = Path.Combine(_appFolders.AttachmentsFolder, item.ImageUrl);
                            System.IO.File.Move(sourceFile, destFile);
                            var filePath = Path.Combine(_appFolders.AttachmentsFolder, item.ImageUrl);
                            image.ImageUrl = filePath;
                            image.ImageName = item.ImageName;
                            image.ImageSize = item.ImageSize;
                            image.EstimatesId = estimatesId;
                            await _imageRepository.InsertAsync(image);
                        }
                    }
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw;
            }
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
            var tess = _estimateRepository.GetAll().Include(x => x.State).ToList();
            var estimates = _estimateRepository.GetAll().Include(x => x.State)
                .Select(x => new EstimateListDto
                {
                    Id = x.Id,
                    LastName = x.LastName,
                    Firstname = x.Firstname,
                    Address = (x.AddressLine1 == null ? "" : x.AddressLine1 + "-") + (x.AddressLine2 == null ? "" : x.AddressLine2 + "-") + (x.City == null ? "" : x.City + "-") + (x.State.StateName == null ? "" : x.State.StateName),
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

            var estimates = _estimateRepository.GetAll().Include(x => x.State)
                .Select(x => new EstimateListForExcelDto
                {
                    Id = x.Id,
                    LastName = x.LastName,
                    Firstname = x.Firstname,
                    Mobile = x.Mobile,
                    Rate = x.Rate,
                    WorkHours = x.WorkHours,
                    ImportantNote = x.ImportantNote,
                    Color = x.Color,
                    NoOfShingles = x.NoOfShingles,
                    ZipCode = x.ZipCode,
                    State = x.State.StateName,
                    AddressLine1 = x.AddressLine1,
                    AddressLine2 = x.AddressLine2,
                    City = x.City,
                    Email = x.Email,
                    Height = x.Height,
                    Length = x.Length,
                    With = x.With,
                    TotalAmount = x.TotalAmount
                }).WhereIf(input.Firstname != null, x => x.Firstname.Contains(input.Firstname))
                .WhereIf(input.LastName != null, x => x.LastName.Contains(input.LastName))
                .ToList();
            return _estimateExcelExporter.ExportToFile(estimates);
        }

        public async Task<List<StateDto>> GetAllState()
        {
            var result = await _stateRepository.GetAll()
                .Select(c => new StateDto
                {
                    StateId = c.Id,
                    StateName = c.StateName
                }).ToListAsync();
            return result;
        }

        public async Task<CreateEstimateDto> GetDataForEdit(int id)
        {
            var estimate = await _estimateRepository.GetAll().Include(x => x.ListImg).Where(x => x.Id == id).FirstOrDefaultAsync();
            var dto = ObjectMapper.Map<CreateEstimateDto>(estimate);
            dto.ListFileName = ObjectMapper.Map<List<CreateImageDto>>(estimate.ListImg);
            return dto;
        }

        public Images GetImageById(int id)
        {
            var image = _imageRepository.GetAll().FirstOrDefault(t => t.Id == id);

            return image;
        }
    }
}