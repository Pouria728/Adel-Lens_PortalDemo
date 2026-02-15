using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsCustomCylConfiguration : IEntityTypeConfiguration<TblLnsCustomCyl>
    {
        public void Configure(EntityTypeBuilder<TblLnsCustomCyl> builder)
        {
            builder.ToTable(@"Tbl_Lns_CustomCyl", @"dbo");
            builder.Property(x => x.CustomCylId).HasColumnName(@"CustomCylId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.Code).HasColumnName(@"Code").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType(@"nvarchar(500)").ValueGeneratedNever().HasMaxLength(500);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0).HasDefaultValueSql(@"1");

            builder.HasKey(@"CustomCylId");

            CustomizeConfiguration(builder);
        }

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsCustomCyl> builder);
    }
}
