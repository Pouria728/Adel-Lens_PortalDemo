using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HamrahanSystem.Domain.Entity
{
    public partial class UserActivityLogConfiguration : IEntityTypeConfiguration<UserActivityLog>
    {
        public void Configure(EntityTypeBuilder<UserActivityLog> builder)
        {
            builder.ToTable(@"UserActivityLogs", @"dbo");

            builder.Property(x => x.ActivityLogId).HasColumnName(@"ActivityLogId").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.CorrelationId).HasColumnName(@"CorrelationId").HasColumnType(@"uniqueidentifier").IsRequired();
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType(@"int").HasPrecision(10, 0);
            builder.Property(x => x.UserName).HasColumnName(@"UserName").HasColumnType(@"nvarchar(200)").HasMaxLength(200);
            builder.Property(x => x.ActionType).HasColumnName(@"ActionType").HasColumnType(@"nvarchar(50)").HasMaxLength(50);
            builder.Property(x => x.Controller).HasColumnName(@"Controller").HasColumnType(@"nvarchar(100)").HasMaxLength(100);
            builder.Property(x => x.Action).HasColumnName(@"Action").HasColumnType(@"nvarchar(100)").HasMaxLength(100);
            builder.Property(x => x.FormName).HasColumnName(@"FormName").HasColumnType(@"nvarchar(200)").HasMaxLength(200);
            builder.Property(x => x.HttpMethod).HasColumnName(@"HttpMethod").HasColumnType(@"nvarchar(10)").HasMaxLength(10);
            builder.Property(x => x.Path).HasColumnName(@"Path").HasColumnType(@"nvarchar(400)").HasMaxLength(400);
            builder.Property(x => x.QueryString).HasColumnName(@"QueryString").HasColumnType(@"nvarchar(max)");
            builder.Property(x => x.RequestBody).HasColumnName(@"RequestBody").HasColumnType(@"nvarchar(max)");
            builder.Property(x => x.StatusCode).HasColumnName(@"StatusCode").HasColumnType(@"int").HasPrecision(10, 0);
            builder.Property(x => x.DurationMs).HasColumnName(@"DurationMs").HasColumnType(@"int").HasPrecision(10, 0);
            builder.Property(x => x.IpAddress).HasColumnName(@"IpAddress").HasColumnType(@"nvarchar(64)").HasMaxLength(64);
            builder.Property(x => x.UserAgent).HasColumnName(@"UserAgent").HasColumnType(@"nvarchar(512)").HasMaxLength(512);
            builder.Property(x => x.CreatedAt).HasColumnName(@"CreatedAt").HasColumnType(@"datetime").IsRequired().ValueGeneratedNever();

            builder.HasKey(x => x.ActivityLogId);
            builder.HasAlternateKey(x => x.CorrelationId);

            builder.HasMany(x => x.Details)
                .WithOne()
                .HasForeignKey(x => x.CorrelationId)
                .HasPrincipalKey(x => x.CorrelationId)
                .OnDelete(DeleteBehavior.NoAction);

            CustomizeConfiguration(builder);
        }

        partial void CustomizeConfiguration(EntityTypeBuilder<UserActivityLog> builder);
    }
}
