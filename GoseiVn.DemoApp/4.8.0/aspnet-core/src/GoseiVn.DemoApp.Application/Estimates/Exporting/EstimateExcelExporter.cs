using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using GoseiVn.DemoApp.Estimates.Dto;
using GoseiVn.DemoApp.Roles.Dto;
using GoseiVn.DemoApp.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp.Estimates.Exporting
{
    public class EstimateExcelExporter : EpPlusExcelExporterBase, IEstimateExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public EstimateExcelExporter(
          ITimeZoneConverter timeZoneConverter,
          IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<EstimateListDto> estimates)
        {
            try
            {
                return CreateExcelPackage(
                    "Estimates" + ".xlsx",
                    excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add("Estimates");
                        sheet.OutLineApplyStyle = true;

                        AddHeader(
                            sheet,
                            "Firstname",
                            "LastName",
                            "Mobile",
                            "Address",
                            "TotalAmount"
                            );
                        AddObjects(
                            sheet, 2, estimates,
                            _ => _.Firstname,
                            _ => _.LastName,
                            _ => _.Mobile,
                            _ => _.Address,
                            _ => _.TotalAmount
                            );
                        for (int i = 1; i <= 5; i++)
                        {
                            sheet.Column(i).AutoFit();
                        }
                    });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
