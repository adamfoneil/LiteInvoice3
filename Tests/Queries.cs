using Dapper.QX;
using LiteInvoice.Data.Queries;

namespace Tests;

[TestClass]
public class Queries
{
    [TestMethod]
    public void GetHourlyRate() => QueryHelper.Test<GetHourlyRate>(Util.GetConnection);    
}
