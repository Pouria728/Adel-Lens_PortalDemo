using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblLnsCustomSphConfiguration : IEntityTypeConfiguration<TblLnsCustomSph>
    {
        public void Configure(EntityTypeBuilder<TblLnsCustomSph> builder)
        {
            builder.ToTable(@"Tbl_Lns_CustomSph", @"dbo");
            builder.Property(x => x.CustomSphId).HasColumnName(@"CustomSphId").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd().HasPrecision(10, 0);
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.Code).HasColumnName(@"Code").HasColumnType(@"nvarchar(254)").ValueGeneratedNever().HasMaxLength(254);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType(@"nvarchar(500)").ValueGeneratedNever().HasMaxLength(500);
            builder.Property(x => x.OrderId).HasColumnName(@"OrderId").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0).HasDefaultValueSql(@"1");
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0).HasDefaultValueSql(@"1");

            builder.HasKey(@"CustomSphId");

            CustomizeConfiguration(builder);
        }

        partial void CustomizeConfiguration(EntityTypeBuilder<TblLnsCustomSph> builder);
    }
}
