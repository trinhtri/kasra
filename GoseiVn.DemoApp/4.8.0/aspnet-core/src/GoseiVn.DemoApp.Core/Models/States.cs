using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp.Models
{
   public class States: FullAuditedEntity
    {
        public string StateName { get; set; }
    }
}
