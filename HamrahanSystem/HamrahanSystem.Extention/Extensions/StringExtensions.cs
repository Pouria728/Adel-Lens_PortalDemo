

using System.Text.Json;


	public static class StringExtensions
	{
		public static DateTime ToPersianDateTime(this string str)
		{
			var item =Convert.ToDateTime(Persia.Number.ConvertToLatin(str));
			var persian= Persia.Calendar.ConvertToGregorian(item.Year, item.Month, item.Day);
			persian.Add(new TimeSpan( item.Hour,item.Minute,item.Second));
			return persian.Add(new TimeSpan(item.Hour, item.Minute, item.Second)); 


		}

	}

