using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceBro;
using FinanceBro.Service;
using System.Net.Http;
using FinanceBro.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceBroUniTests
{

    [TestClass]
    public class UnitTest2
    {
        private readonly HttpClient _client;
        private readonly SqlDbContext _context;
        [TestMethod]
        public void Test_AddMethod()
        {
            DbContextOptions<SqlDbContext> options = new DbContextOptions<SqlDbContext>();

            LatestDatesListService latestDatesListService = new LatestDatesListService(_context);

            MarketDataApiService marketDataApiService = new MarketDataApiService(_client, latestDatesListService);

            string JsonBody = "{  \"method\":\"chart\",  \"symbol\":\"pypl\",  \"span\":\"stock\",  \"interval\":\"1d\"}";

            string address = "https://localhost:5001/api/Trigger";

            var test = marketDataApiService.ApiPostMethod(address, JsonBody);

            string expected = "";

            //Assert.AreEqual(test, expected);
        }
    }
}
