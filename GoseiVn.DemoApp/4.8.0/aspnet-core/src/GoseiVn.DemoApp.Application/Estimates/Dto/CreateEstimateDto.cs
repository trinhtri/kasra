using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp.Estimates.Dto
{
    [AutoMapFrom(typeof(Models.Estimates))]
    public class CreateEstimateDto: EntityDto
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string ZipCode { get; set; }
        public decimal With { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public int NoOfShingles { get; set; }
        public string Color { get; set; }
        public string ImportantNote { get; set; }
        public decimal WorkHours { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CreateImageDto> ListFileName { get; set; }
    }
}
