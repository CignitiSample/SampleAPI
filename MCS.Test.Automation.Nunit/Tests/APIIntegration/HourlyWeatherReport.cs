using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using MCS.Test.Automation.Common;
using MCS.Test.Automation.Common.Helpers;
using MCS.Test.Automation.Tests.NUnit.DataDriven;
using NUnit.Framework;
using RestSharp;

namespace MCS.Test.Automation.Tests.NUnit.Tests
{
    [TestFixture]
    public class HourlyWeatherReport : ProjectTestBase
    {

        [Test]
        public async Task HourlyWeatherReportTest()
        {

            var client = new RestClient("https://samples.openweathermap.org/data/2.5/forecast?id=524901&appid=b1b15e88fa797225412429c1c50c122a1");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
        }
    }
}