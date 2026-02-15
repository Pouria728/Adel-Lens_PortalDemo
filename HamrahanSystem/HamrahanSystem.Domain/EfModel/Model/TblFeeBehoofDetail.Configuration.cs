
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HamrahanSystem.Domain.Entity
{
    /// <summary>
    /// There are no comments for TblFeeBehoofDetailConfiguration in the schema.
    /// </summary>
    public partial class TblFeeBehoofDetailConfiguration : IEntityTypeConfiguration<TblFeeBehoofDetail>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<TblFeeBehoofDetail> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<TblFeeBehoofDetail> builder)
        {
            builder.ToTable(@"Tbl_Fee_BehoofDetail", @"dbo", x => {
              x.HasTrigger(@"Tr_Fee_BehoofDetail_After_Update");
              x.HasTrigger(@"Tr_Fee_BehoofDetail_HistoryLogMaker");
            });
            builder.Property(x => x.Aggregation).HasColumnName(@"Aggregation").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.AggregationScore).HasColumnName(@"AggregationScore").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.BehoofCode).HasColumnName(@"BehoofCode").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.CalculateNO).HasColumnName(@"CalculateNO").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.ChangeDate).HasColumnName(@"ChangeDate").HasColumnType(@"datetime").IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql(@"getdate()");
            builder.Property(x => x.ChangeIndex).HasColumnName(@"ChangeIndex").HasColumnType(@"tinyint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0).HasDefaultValueSql(@"0");
            builder.Property(x => x.ChangeUser).HasColumnName(@"ChangeUser").HasColumnType(@"nvarchar(256)").IsRequired().ValueGeneratedOnAdd().HasMaxLength(256).HasDefaultValueSql(@"suser_sname()");
            builder.Property(x => x.CodeInsurance).HasColumnName(@"CodeInsurance").HasColumnType(@"tinyint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType(@"tinyint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.DayCounter).HasColumnName(@"DayCounter").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.Depository).HasColumnName(@"Depository").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.EmployeeCode).HasColumnName(@"EmployeeCode").HasColumnType(@"tinyint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.FiatPrintView).HasColumnName(@"FiatPrintView").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.FiatView).HasColumnName(@"FiatView").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.FishView).HasColumnName(@"FishView").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.FishViewNO).HasColumnName(@"FishViewNO").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.FixCostProject).HasColumnName(@"FixCostProject").HasColumnType(@"bit").ValueGeneratedNever().HasDefaultValueSql(@"0");
            builder.Property(x => x.FormulCeil).HasColumnName(@"FormulCeil").HasColumnType(@"nvarchar(1000)").ValueGeneratedNever().HasMaxLength(1000);
            builder.Property(x => x.FormulCeilDes).HasColumnName(@"FormulCeilDes").HasColumnType(@"nvarchar(1000)").ValueGeneratedNever().HasMaxLength(1000);
            builder.Property(x => x.FormulCeilMerge).HasColumnName(@"FormulCeilMerge").HasColumnType(@"nvarchar(1000)").ValueGeneratedNever().HasMaxLength(1000);
            builder.Property(x => x.FormulDesList).HasColumnName(@"FormulDesList").HasColumnType(@"nvarchar(1000)").ValueGeneratedNever().HasMaxLength(1000);
            builder.Property(x => x.FormulList).HasColumnName(@"FormulList").HasColumnType(@"nvarchar(1000)").ValueGeneratedNever().HasMaxLength(1000);
            builder.Property(x => x.FormulMerge).HasColumnName(@"FormulMerge").HasColumnType(@"nvarchar(1000)").ValueGeneratedNever().HasMaxLength(1000);
            builder.Property(x => x.FormViewNO).HasColumnName(@"FormViewNO").HasColumnType(@"smallint").ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.FromDate).HasColumnName(@"FromDate").HasColumnType(@"nvarchar(10)").IsRequired().ValueGeneratedNever().HasMaxLength(10);
            builder.Property(x => x.HasCoef).HasColumnName(@"HasCoef").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.IncomeNo).HasColumnName(@"IncomeNo").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.Insurancable).HasColumnName(@"Insurancable").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.Marriage).HasColumnName(@"Marriage").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.NotCash).HasColumnName(@"NotCash").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.ONOFF).HasColumnName(@"ON_OFF").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.SelectAccount).HasColumnName(@"SelectAccount").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.Subable).HasColumnName(@"Subable").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.Sumable).HasColumnName(@"Sumable").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.SumFishNoView).HasColumnName(@"SumFishNoView").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.Taxable).HasColumnName(@"Taxable").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.Timeshit).HasColumnName(@"Timeshit").HasColumnType(@"bit").ValueGeneratedNever();
            builder.Property(x => x.YearsBase).HasColumnName(@"YearsBase").HasColumnType(@"bit").ValueGeneratedNever();
            builder.HasKey(@"BehoofCode", @"Company", @"EmployeeCode", @"CodeInsurance", @"FromDate");
            builder.HasOne(x => x.TblFeeBehoofMaster).WithMany(op => op.TblFeeBehoofDetails).HasForeignKey(@"BehoofCode", @"Company").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<TblFeeBehoofDetail> builder);

        #endregion
    }

}
