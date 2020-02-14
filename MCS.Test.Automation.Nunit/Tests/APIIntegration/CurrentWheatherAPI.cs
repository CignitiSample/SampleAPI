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
    public class CurrentWheatherUpdateAPIClass : ProjectTestBase
    {

        [Test]
        public async Task CurrentWheatherUpdateAPITest()
        {

            var client = new RestClient("https://samples.openweathermap.org/data/2.5/weather?id=2172797&appid=b6907d289e10d714a6e88b30761fae22");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

        }
    }
}