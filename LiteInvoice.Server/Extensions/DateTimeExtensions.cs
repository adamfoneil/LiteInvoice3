namespace LiteInvoice.Data.Extensions;

public static class DateTimeExtensions
{
	public static DateOnly AsDateOnly(this DateTime dateTime) => new(dateTime.Year, dateTime.Month, dateTime.Day);	
}
