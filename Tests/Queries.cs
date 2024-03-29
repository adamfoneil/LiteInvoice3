﻿using Dapper.QX;
using LiteInvoice.Server.Queries;

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

    [TestMethod]
    public void MyPendingLineEntries() => QueryHelper.Test<MyPendingLineEntries>(Util.GetConnection);

    [TestMethod]
    public void MyInvoices() => QueryHelper.Test<MyInvoices>(Util.GetConnection);
}
