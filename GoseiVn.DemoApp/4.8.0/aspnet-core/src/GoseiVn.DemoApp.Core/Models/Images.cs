using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp.Models
{
   public class Images: FullAuditedEntity
    {
        public string ImageName { get; set; }
        public decimal ImageSize { get; set; }
        public string ImageUrl { get; set; }

        public int EstimatesId { get; set; }
        public virtual Estimates Estimates { get; set; }

    }
}
