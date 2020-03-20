using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using MCS.Test.Automation.Common;
using MCS.Test.Automation.Common.Helpers;
using MCS.Test.Automation.Tests.NUnit.DataDriven;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RelevantCodes.ExtentReports;
using RestSharp;

namespace MCS.Test.Automation.Tests.NUnit.Tests
{
    [TestFixture]
    public class CurrentWeatherUpdateAPIClass : ProjectTestBase
    {
        public static ExtentTest ExtentStepTest { get; set; }

        private static ExtentReports extent;

        [Test]
        public async Task CurrentWeatherUpdateAPITest()
        {
            extent = new ExtentReports("..\\Reports", true);
            ExtentStepTest = extent.StartTest("TestName");
            ExtentStepTest.Log(LogStatus.Info, "Test started");
            string url = ConfigurationManager.AppSettings["BaseUrl"] + XmlReading.CurrentWEeatherUrl;
            //var client = new RestClient("https://samples.openweathermap.org/data/2.5/weather?id=2172797&appid=b6907d289e10d714a6e88b30761fae22");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;
            response = client.GetAsync(url).Result;
            string responseString = await response.Content.ReadAsStringAsync();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //Weather objResp1 = JsonConvert.DeserializeObject<Weather>(responseString);

            testClass objweather = serializer.Deserialize<testClass>(responseString);
            
            //foreach (Weather item in objweather.weather)
            //{
            //    string main = item.main;
            //}

            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            // string jsonObject = JsonCo;nvert.DeserializeObject(response.Content.ReadAsStringAsync().);
            // jsonObject.to
            //dynamic weatherdata = JObject.Parse(responseString);
            if (response.IsSuccessStatusCode)
            {
                //ExtentStepTest.Log(LogStatus.Pass, "Received response ", "Response: " + responseString + "\n" + "HTTP Status: " + response.StatusCode + " Reason :" + response.ReasonPhrase);

                ExtentStepTest.Log(LogStatus.Pass, "Received response ", "Response: " + responseString + "\n");
                //ExtentStepTest.Log(LogStatus.Pass, "Weather Report ", "Response: " + objweather.weather + "\n"); this place we neeed to bind
                ExtentStepTest.Log(LogStatus.Pass, "HTTP Status: " + response.StatusCode + " Reason :" + response.ReasonPhrase + "\n");
            }
            else
            {
                ExtentStepTest.Log(LogStatus.Fail, "Received response as API validation failed, Please try to reprocess with correct information", "Response: " + responseString + "\n" + "HTTP Status: " + response.StatusCode + " Reason :" + response.ReasonPhrase);
            }
            extent.EndTest(ExtentStepTest);
            extent.Flush();
        }
    }

    public class testClass
    {
        public List<Weather> weather { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}
