using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp.Estimates.Dto
{
    public class EstimateInput: PagedResultRequestDto
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
        //public string Sorting { get; set; }

        //public void Normalize()
        //{
        //    if (string.IsNullOrEmpty(Sorting))
        //    {
        //        Sorting = "ContractNo";
        //    }
        //}
    }
}
