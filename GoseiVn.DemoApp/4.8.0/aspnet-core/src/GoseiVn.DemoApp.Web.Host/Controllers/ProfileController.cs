using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoseiVn.DemoApp.Controllers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoseiVn.DemoApp.Web.Host.Controllers
{
    public class ProfileController : ProfileControllerBase
    {
        public ProfileController(IAppFolders appFolders) : base(appFolders)
        {
        }
    }
}
