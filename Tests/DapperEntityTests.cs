namespace Tests
{
    [TestClass]
    public class DapperEntityTests
    {
        [TestMethod]
        public async Task SetTimeZone()
        {
            var db = Util.DapperEntities;
            db.CurrentUser = new() { UserName = "nobody" };
            await db.SetTimeZoneAsync("anything");
        }
    }
}