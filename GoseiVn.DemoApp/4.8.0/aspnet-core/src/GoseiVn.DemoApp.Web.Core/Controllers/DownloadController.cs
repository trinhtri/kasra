using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp.Controllers
{
  public class DownloadController : ProfileControllerBase
    {
        public DownloadController(IAppFolders appFolders)
           : base(appFolders)
        {
        }
    }
}
