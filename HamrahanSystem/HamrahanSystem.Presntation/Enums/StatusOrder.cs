using System.ComponentModel;

namespace HamrahanSystem.Presntation
{
    public enum StatusOrder
    {
		


		[Description("ثبت موقت")]
		ToDO = 1,
		[Description("در حال اجرا")]
		Inprogress = 2,
		[Description("تکمیل شده")]
		Complete = 3,
		[Description("کنسل شده ")]
		Cancel = 4,
	}
}
