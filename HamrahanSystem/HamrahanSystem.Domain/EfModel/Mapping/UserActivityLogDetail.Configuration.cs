using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HamrahanSystem.Domain.Entity
{
    public partial class UserActivityLogDetailConfiguration : IEntityTypeConfiguration<UserActivityLogDetail>
    {
        public void Configure(EntityTypeBuilder<UserActivityLogDetail> builder)
        {
            builder.ToTable(@"UserActivityLogDetails", @"dbo");

            builder.Property(x => x.ActivityLogDetailId).HasColumnName(@"ActivityLogDetailId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.CorrelationId).HasColumnName(@"CorrelationId").HasColumnType(@"uniqueidentifier").IsRequired();
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType(@"int").HasPrecision(10, 0);
            builder.Property(x => x.UserName).HasColumnName(@"UserName").HasColumnType(@"nvarchar(200)").HasMaxLength(200);
            builder.Property(x => x.EntityName).HasColumnName(@"EntityName").HasColumnType(@"nvarchar(200)").HasMaxLength(200);
            builder.Property(x => x.EntityId).HasColumnName(@"EntityId").HasColumnType(@"nvarchar(100)").HasMaxLength(100);
            builder.Property(x => x.Operation).HasColumnName(@"Operation").HasColumnType(@"nvarchar(20)").HasMaxLength(20);
            builder.Property(x => x.Changes).HasColumnName(@"Changes").HasColumnType(@"nvarchar(max)");
            builder.Property(x => x.CreatedAt).HasColumnName(@"CreatedAt").HasColumnType(@"datetime").IsRequired().ValueGeneratedNever();

            builder.HasKey(x => x.ActivityLogDetailId);

            CustomizeConfiguration(builder);
        }

        partial void CustomizeConfiguration(EntityTypeBuilder<UserActivityLogDetail> builder);
    }
}
