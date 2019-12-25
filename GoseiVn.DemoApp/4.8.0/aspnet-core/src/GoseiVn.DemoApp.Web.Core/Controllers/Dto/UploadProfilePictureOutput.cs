using Abp.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp.Controllers.Dto
{
   public class UploadProfilePictureOutput
    {
        public UploadProfilePictureOutput()
        {
        }

        public string FileName { get; set; }

        public decimal Size { get; set; }

        public ErrorInfo ErrorInfo { get; set; }
    }
}
