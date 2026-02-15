using System.ComponentModel;

namespace HamrahanSystem.Presntation
{
    public enum Procedure
    {
		[Description("ندارد")]
		LnsOrder = 0,
		[Description("خروج از انبار کالا")]
		StockOut = 1,
		[Description("تامین جهت ساخت")]
		CreateLens = 2,
		

	}
}
