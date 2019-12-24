using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp.Estimates.Dto
{
    [AutoMapFrom(typeof(Models.Images))]
    public class CreateImageDto: EntityDto
    {
        public int EstimateID { get; set; }
        public string ImageName { get; set; }
        public decimal ImageSize { get; set; }
        public string ImageUrl { get; set; }
    }
}
