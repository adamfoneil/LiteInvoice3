namespace LiteInvoice.Entities.Static;

public static class DateTimeHelper
{
	public static DateTime Now(string? timeZoneId)
	{
		if (timeZoneId == null) return DateTime.UtcNow;

		try
		{
			var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
			return TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);
		}
		catch
		{
			return DateTime.UtcNow;
		}
	}
}
