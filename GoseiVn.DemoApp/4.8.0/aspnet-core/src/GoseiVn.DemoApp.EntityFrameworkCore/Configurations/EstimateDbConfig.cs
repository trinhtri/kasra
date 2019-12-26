using GoseiVn.DemoApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoseiVn.DemoApp.Configurations
{
    public class EstimateDbConfig
    {
        public static void Configure(EntityTypeBuilder<Estimates> entity)
        {
            entity.ToTable("Estimates");
            entity.Property(e => e.Firstname).IsRequired().HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.LastName).IsRequired().HasColumnType("NVARCHAR(20)");
            entity.Property(e => e.Mobile).IsRequired().HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.Email).HasColumnType("NVARCHAR(50)");
            entity.Property(e => e.AddressLine1).HasColumnType("NVARCHAR(100)");
            entity.Property(e => e.AddressLine2).HasColumnType("NVARCHAR(100)");
            entity.Property(e => e.City).HasColumnType("NVARCHAR(25)");
            entity.Property(e => e.ZipCode).HasColumnType("NVARCHAR(10)");
            entity.Property(x => x.With).HasColumnType("decimal(10,2)");
            entity.Property(x => x.Height).HasColumnType("decimal(10,2)");
            entity.Property(x => x.Length).HasColumnType("decimal(10,2)");
            entity.Property(e => e.Color).HasColumnType("NVARCHAR(10)");
            entity.Property(e => e.ImportantNote).HasColumnType("NVARCHAR(500)");
            entity.Property(x => x.WorkHours).HasColumnType("decimal(10,2)");
            entity.Property(x => x.Rate).HasColumnType("decimal(10,2)");
            entity.Property(x => x.TotalAmount).HasColumnType("decimal(10,2)");
        }
    }
}