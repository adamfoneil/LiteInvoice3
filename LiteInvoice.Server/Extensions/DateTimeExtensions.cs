namespace LiteInvoice.Data.Extensions;

public static class DateTimeExtensions
{
	public static DateOnly AsDateOnly(this DateTime dateTime) => new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);	
}
