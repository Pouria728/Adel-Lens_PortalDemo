using System.ComponentModel;

namespace HamrahanSystem.Presntation
{
    public enum IndexDocument
    {
		[Description("عدسی سفارشی")]
		LnsOrder = 5001,
		[Description("عدسی آماده")]
		LnsReadyOrder = 5002,
		[Description("عدسی اماده با گارانتی")]
		LnsOrderGranty = 5003,

	}
}
