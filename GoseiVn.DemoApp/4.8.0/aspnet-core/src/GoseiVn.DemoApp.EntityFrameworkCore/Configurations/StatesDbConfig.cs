using GoseiVn.DemoApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp.Configurations
{
   public class StatesDbConfig
    {
        public static void Configure(EntityTypeBuilder<States> entity)
        {
            entity.ToTable("States");
            entity.Property(e => e.StateName).IsRequired().HasColumnType("NVARCHAR(20)");
        }
    }
}
