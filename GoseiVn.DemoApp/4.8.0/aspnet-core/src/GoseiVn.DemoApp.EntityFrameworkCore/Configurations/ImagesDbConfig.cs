using GoseiVn.DemoApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp.Configurations
{
  public class ImagesDbConfig
    {
        public static void Configure(EntityTypeBuilder<Images> entity)
        {
            entity.ToTable("Images");
            entity.Property(e => e.ImageName).IsRequired().HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.ImageUrl).IsRequired().HasColumnType("NVARCHAR(500)");
            entity.Property(x => x.ImageSize).HasColumnType("decimal(10,2)");
            
        }
    }
}
