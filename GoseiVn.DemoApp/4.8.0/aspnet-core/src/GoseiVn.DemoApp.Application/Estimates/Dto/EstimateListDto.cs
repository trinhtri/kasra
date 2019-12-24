using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
namespace GoseiVn.DemoApp.Estimates.Dto
{
    [AutoMapFrom(typeof(Models.Estimates))]
    public class EstimateListDto : EntityDto
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
