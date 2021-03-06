﻿using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace GoseiVn.DemoApp.Models
{
    public class Estimates : FullAuditedEntity
    {
        public Estimates()
        {
            ListImg = new HashSet<Images>();
        }

        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }

        public int? StateId { get; set; }
        public virtual States State { get; set; }

        public string ZipCode { get; set; }
        public decimal With { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public int NoOfShingles { get; set; }
        public string Color { get; set; }
        public string ImportantNote { get; set; }
        public decimal WorkHours { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual ICollection<Images> ListImg { get; set; }
    }
}