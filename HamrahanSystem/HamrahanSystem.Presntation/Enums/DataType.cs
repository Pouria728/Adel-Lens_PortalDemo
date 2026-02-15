using System.ComponentModel;

namespace HamrahanSystem.Presntation
{
    public enum DataType
    {
        [Description("عدد")]
        Int = 1,
        [Description("رشته")]
        String = 2,
        [Description("تاریخ")]
        Date = 3,
        [Description("زمان")]
        Time = 4,
        [Description("تاریخ و زمان")]
        DateTime = 5,
        [Description("گروه داده")]
        Group = 6,



    }
}
