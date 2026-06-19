using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HamrahanSystem.Domain.Entity
{
    public partial class TblWfwOrderRawMaterialConfiguration : IEntityTypeConfiguration<TblWfwOrderRawMaterial>
    {
        public void Configure(EntityTypeBuilder<TblWfwOrderRawMaterial> builder)
        {
            builder.ToTable(@"Tbl_Wfw_OrderRawMaterial", @"dbo");

            builder.Property(x => x.OrderRawMaterialId)
                .HasColumnName(@"OrderRawMaterialId")
                .HasColumnType(@"bigint")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasPrecision(19, 0);

            builder.Property(x => x.OrderId)
                .HasColumnName(@"OrderId")
                .HasColumnType(@"bigint")
                .IsRequired()
                .ValueGeneratedNever()
                .HasPrecision(19, 0);

            builder.Property(x => x.OrderProcessId)
                .HasColumnName(@"OrderProcessId")
                .HasColumnType(@"bigint")
                .IsRequired()
                .ValueGeneratedNever()
                .HasPrecision(19, 0);

            builder.Property(x => x.ProcessStepId)
                .HasColumnName(@"ProcessStepId")
                .HasColumnType(@"int")
                .IsRequired()
                .ValueGeneratedNever()
                .HasPrecision(10, 0);

            builder.Property(x => x.DefineObjectId)
                .HasColumnName(@"DefineObjectId")
                .HasColumnType(@"int")
                .IsRequired()
                .ValueGeneratedNever()
                .HasPrecision(10, 0);

            builder.Property(x => x.DefineObjectRecNo)
                .HasColumnName(@"DefineObjectRecNo")
                .HasColumnType(@"int")
                .ValueGeneratedNever()
                .HasPrecision(10, 0);

            builder.Property(x => x.WasterRNObject)
                .HasColumnName(@"WasterRNObject")
                .HasColumnType(@"int")
                .ValueGeneratedNever()
                .HasPrecision(10, 0);

            builder.Property(x => x.ProdRNObject)
                .HasColumnName(@"ProdRNObject")
                .HasColumnType(@"int")
                .ValueGeneratedNever()
                .HasPrecision(10, 0);

            builder.Property(x => x.Barcode)
                .HasColumnName(@"Barcode")
                .HasColumnType(@"nvarchar(100)")
                .HasMaxLength(100)
                .ValueGeneratedNever();

            builder.Property(x => x.MaterialName)
                .HasColumnName(@"MaterialName")
                .HasColumnType(@"nvarchar(254)")
                .HasMaxLength(254)
                .ValueGeneratedNever();

            builder.Property(x => x.Quantity)
                .HasColumnName(@"Quantity")
                .HasColumnType(@"int")
                .IsRequired()
                .ValueGeneratedNever()
                .HasPrecision(10, 0)
                .HasDefaultValueSql(@"1");

            builder.Property(x => x.DateCreate)
                .HasColumnName(@"DateCreate")
                .HasColumnType(@"datetime")
                .IsRequired()
                .ValueGeneratedNever()
                .HasDefaultValueSql(@"getdate()");

            builder.Property(x => x.DateUpdate)
                .HasColumnName(@"DateUpdate")
                .HasColumnType(@"datetime")
                .IsRequired()
                .ValueGeneratedNever()
                .HasDefaultValueSql(@"getdate()");

            builder.Property(x => x.CreatedBy)
                .HasColumnName(@"CreatedBy")
                .HasColumnType(@"int")
                .ValueGeneratedNever()
                .HasPrecision(10, 0);

            builder.Property(x => x.ModifiedBy)
                .HasColumnName(@"ModifiedBy")
                .HasColumnType(@"int")
                .ValueGeneratedNever()
                .HasPrecision(10, 0);

            builder.HasKey(x => x.OrderRawMaterialId);

            builder.HasIndex(x => new { x.OrderId, x.ProcessStepId, x.DefineObjectId, x.WasterRNObject, x.ProdRNObject })
                .HasDatabaseName(@"UX_Tbl_Wfw_OrderRawMaterial_OrderStepMaterial")
                .IsUnique();

            CustomizeConfiguration(builder);
        }

        partial void CustomizeConfiguration(EntityTypeBuilder<TblWfwOrderRawMaterial> builder);
    }
}
