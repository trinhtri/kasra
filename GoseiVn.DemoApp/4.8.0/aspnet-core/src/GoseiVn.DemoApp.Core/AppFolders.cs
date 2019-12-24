﻿using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp
{
    public class AppFolders: IAppFolders, ISingletonDependency
    {
        public string TempFileDownloadFolder { get; set; }

        public string SampleProfileImagesFolder { get; set; }

        public string WebLogsFolder { get; set; }

        public string AttachmentsFolder { get; set; }

        public string TempFileUploadFolder { get; set; }
    }
}
