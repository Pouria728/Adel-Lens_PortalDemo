using System.ComponentModel;

namespace HamrahanSystem.Presntation
{
    public enum StatusRequest
    {
		


		[Description("در انتظار بررسی")]
		Inprogress = 1,
		[Description("انجام شده")]
		Complete = 2,
		[Description("رد شده")]
		Reject = 3,
		[Description("ارجاع به مرحله قبل ")]
		Cancel = 4,
	}
}
