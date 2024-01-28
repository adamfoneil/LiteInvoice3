using Dapper.QX;
using LiteInvoice.Data.Queries;

namespace Tests;

[TestClass]
public class Queries
{
    [TestMethod]
    public void MyBusinesses() => QueryHelper.Test<MyBusinesses>(Util.GetConnection);

    [TestMethod]
    public void SetCurrentBusiness() => QueryHelper.Test<SetCurrentBusiness>(Util.GetConnection);

    [TestMethod]
    public void MyCustomers() => QueryHelper.Test<MyCustomers>(Util.GetConnection);

    [TestMethod]
    public void MyProjects() => QueryHelper.Test<MyProjects>(Util.GetConnection);

    [TestMethod]
    public void MyPendingWorkEntries() => QueryHelper.Test<MyPendingWorkEntries>(Util.GetConnection);
}
