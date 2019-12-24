using GoseiVn.DemoApp.Estimates.Dto;
using GoseiVn.DemoApp.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp.Estimates.Exporting
{
    public interface IEstimateExcelExporter
    {
        FileDto ExportToFile(List<EstimateListDto> estimates);
    }
}
