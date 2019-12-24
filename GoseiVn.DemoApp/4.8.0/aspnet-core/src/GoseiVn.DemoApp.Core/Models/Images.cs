﻿using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp.Models
{
   public class Images: FullAuditedEntity
    {
        public int EstimateID { get; set; }
        public virtual Estimates Estimates { get; set; }
        public string ImageName { get; set; }
        public decimal ImageSize { get; set; }
    }
}
