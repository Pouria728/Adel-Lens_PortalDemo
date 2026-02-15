

using System.Text.Json;


	public static class DateTimeExtensions
	{
		public static string ToPersianDateTime(this DateTime str)
		{
			//var item =Convert.ToDateTime(Persia.Number.ConvertToLatin(str));
			var persian= Persia.Calendar.ConvertToPersian(str);
			
			return $"{persian.Simple.ToString()} {str.ToShortTimeString()}"; 


		}

	}

