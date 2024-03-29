﻿// <copyright file="ProjectTestBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.NUnit
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using global::NUnit.Framework;
    using global::NUnit.Framework.Interfaces;
    using MCS.Test.Automation.Common;
    using MCS.Test.Automation.Common.Helpers;
    using MCS.Test.Automation.Common.Logger;
    using MCS.Test.Automation.Common.Types;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenQA.Selenium;
    using RelevantCodes.ExtentReports;

    /// <summary>
    /// The base class for all tests.
    /// </summary>
    public class APIProjectTestBase : TestBase
    {
        // private static IWebDriver driver;
        // private static ExtentTest test;
        private static ExtentReports extent;
        private readonly DriverContext driverContext = new DriverContext();
        private string dir;
        private string filename;
        private BrowserType browserType2;

        public ExtentTest GetTestInstanceOfReport
        {
            get
            {
                SuiteLevelConfiguration suiteLevelConfiguration = new SuiteLevelConfiguration();
                return suiteLevelConfiguration.GetTestInstanceOfReport;
            }
        }

        /// <summary>
        /// Gets or sets logger instance for driver.
        /// </summary>
        public TestLogger LogTest
        {
            get
            {
                return this.DriverContext.LogTest;
            }

            set
            {
                this.DriverContext.LogTest = value;
            }
        }

        /// <summary>
        /// Gets or Sets the driver context.
        /// </summary>
        protected DriverContext DriverContext
        {
            get
            {
                return this.driverContext;
            }
        }

        /// <summary>
        /// Before the class.
        /// </summary>
       // [OneTimeSetUp]
        public void BeforeClass()
        {
        }

        /// <summary>
        /// After the class.
        /// </summary>
        // [OneTimeTearDown]
        // [TearDown]
        public void AfterClass()
        {
            PrintPerformanceResultsHelper.PrintAverageDurationMillisecondsInAppVeyor(this.DriverContext.PerformanceMeasures);
            PrintPerformanceResultsHelper.PrintPercentiles90DurationMillisecondsInAppVeyor(this.DriverContext.PerformanceMeasures);
            PrintPerformanceResultsHelper.PrintAverageDurationMillisecondsInTeamcity(this.DriverContext.PerformanceMeasures);
            PrintPerformanceResultsHelper.PrintPercentiles90DurationMillisecondsinTeamcity(this.DriverContext.PerformanceMeasures);

            // this.DriverContext.Stop();
        }

        /// <summary>
        /// Before the test.
        /// </summary>
        [SetUp]
        public void BeforeTest()
        {
            var categoryName = this.GetType().GetMethod(TestContext.CurrentContext.Test.MethodName).GetCustomAttributes(true).OfType<CategoryAttribute>().First().Name;
            categoryName = TestContext.Parameters.Get("Cat", categoryName);
            var descriptionName = this.GetType().GetMethod(TestContext.CurrentContext.Test.MethodName).GetCustomAttributes(true).OfType<TestCaseDescriptionAttribute>().FirstOrDefault().Name;
            var descriptionId = this.GetType().GetMethod(TestContext.CurrentContext.Test.MethodName).GetCustomAttributes(true).OfType<TestCaseDescriptionAttribute>().FirstOrDefault().Id;
            var pdfDownload = this.GetType().GetMethod(TestContext.CurrentContext.Test.MethodName).GetCustomAttributes(true).OfType<TestCaseDescriptionAttribute>().FirstOrDefault().PDFDownload;

            this.DriverContext.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            List<string> listofMethodstoDownloadAutomatically = new List<string>();
            listofMethodstoDownloadAutomatically.Add("VerifyingAccessForActiveStandards_PDF");
            this.dir = TestContext.CurrentContext.TestDirectory + "\\";

            var fileName1 = this.dir;
            if (BaseConfiguration.ReportOutsideBin == "Yes")
            {
                if (BaseConfiguration.JiraResult == "SuiteLevel")
                {
                    DirectoryInfo direc = Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.Parent;
                    string dirPath = direc.FullName + "\\SuiteLevelReport\\";

                    this.filename = dirPath + categoryName + "_" + descriptionId + "_" + TestContext.CurrentContext.Test.MethodName + DateHelper.CurrentTimeStamp + ".html";
                    string finalPath = this.filename;
                    fileName1 = this.dir + "extent-config.xml";
                }
                else
                {
                    DirectoryInfo direc = Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.Parent;
                    string dirPath = direc.FullName + "\\MethodLevelReport\\";

                    ////DirectoryInfo direc = Directory.GetParent(TestContext.CurrentContext.TestDirectory);
                    ////string dirPath = direc.FullName + BaseConfiguration.ExtentReportFolder;
                    this.filename = dirPath + categoryName + "_" + descriptionId + "_" + TestContext.CurrentContext.Test.MethodName + DateHelper.CurrentTimeStamp + ".html";
                    string finalPath = this.filename;
                    fileName1 = this.dir + "extent-config.xml";
                }
            }
            else
            {
                if (BaseConfiguration.JiraResult == "SuiteLevel")
                {
                    DirectoryInfo direc = Directory.GetParent(TestContext.CurrentContext.TestDirectory);
                    string dirPath = direc.FullName + BaseConfiguration.ExtentReportFolder;
                    this.filename = dirPath + "Suite_" + categoryName + "_" + descriptionId + "_" + TestContext.CurrentContext.Test.MethodName + DateHelper.CurrentTimeStamp + ".html";
                    string finalPath = this.filename;
                    fileName1 = this.dir + "extent-config.xml";
                }
                else
                {
                    DirectoryInfo direc = Directory.GetParent(TestContext.CurrentContext.TestDirectory);
                    string dirPath = direc.FullName + BaseConfiguration.ExtentReportFolder;
                    this.filename = dirPath + "Method_" + categoryName + "_" + descriptionId + "_" + TestContext.CurrentContext.Test.MethodName + DateHelper.CurrentTimeStamp + ".html";
                    string finalPath = this.filename;
                    TestContext.AddTestAttachment(finalPath);
                    fileName1 = this.dir + "extent-config.xml";
                }
            }

            extent = new ExtentReports(this.filename, true);

            // Add QA system info to html report
            extent.AddSystemInfo("Host Name", "QA Host")
                .AddSystemInfo("Target Browser", BaseConfiguration.TestBrowser.ToString())
                .AddSystemInfo("Environment", "QA")
                .AddSystemInfo("Username", "QA User");

            // Adding config.xml file
            extent.LoadConfig(fileName1);

            JsonContext.InfoDescription = descriptionName;
            JsonContext.TestsStart = DateHelper.CurrentDateTimeZoneStamp;
            JsonContext.TestsTestKey = descriptionId;
            JsonContext.TestsEvidencesFileName = TestContext.CurrentContext.Test.MethodName + ".html";
            SuiteLevelConfiguration.JsonTestEvidenceFileNameKey.Add(TestContext.CurrentContext.Test.MethodName + "-" + JsonContext.TestsTestKey);
            this.DriverContext.TestTitle = TestContext.CurrentContext.Test.MethodName;
            //var browserType = TestContext.Parameters.Get("Browser", BaseConfiguration.TestBrowser.ToString());

            // Parse the browser Type, since its Enum
            //this.browserType2 = (BrowserType)Enum.Parse(typeof(BrowserType), browserType);
            //this.DriverContext.Start(this.browserType2, pdfDownload);
            if (BaseConfiguration.JiraResult == "MethodLevel")
            {
                DriverContext.ExtentStepTest = extent.StartTest(this.DriverContext.TestTitle);
            }
            else if (BaseConfiguration.JiraResult == "SuiteLevel")
            {
                SuiteLevelConfiguration.SuiteExtent.AddSystemInfo("Host Name", "QA USer")
               .AddSystemInfo("Target Browser", BaseConfiguration.TestBrowser.ToString())
               .AddSystemInfo("Environment", "QA")
               .AddSystemInfo("Username", "QA");
                DriverContext.ExtentStepTest = SuiteLevelConfiguration.SuiteExtent.StartTest(this.DriverContext.TestTitle);
                SuiteLevelConfiguration.SuiteExtent.LoadConfig(fileName1);
            }
            else
            {
                throw new KeyNotFoundException("There is no such key for Report");
            }

            DriverContext.ExtentStepTest.Description = descriptionId + descriptionName;
            DriverContext.ExtentStepTest.AssignCategory(categoryName.ToString());
            this.GetTestInstanceOfReport.Log(LogStatus.Info, "***starting of test case (" + this.DriverContext.TestTitle + ")***", "Started test case successfully ");
            this.LogTest.LogTestStarting(this.driverContext);
        }

        /// <summary>
        /// After the test.
        /// </summary>
        [TearDown]
        public void AfterTest()
        {
            try
            {
                this.AssertAll(this.driverContext.VerifyMessages);
                JsonContext.TestsFinish = DateHelper.CurrentDateTimeZoneStamp;
                this.DriverContext.IsTestFailed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed || !this.driverContext.VerifyMessages.Count.Equals(0);
                var filePaths = this.SaveTestDetailsIfTestFailed(this.driverContext);
                if (filePaths == null || filePaths.Length == 0)
                {
                }
                else
                {
                    byte[] imageArray = null;
                    if (filePaths.Count() >= 2)
                    {
                        imageArray = System.IO.File.ReadAllBytes(filePaths[filePaths.Count() - 2]);
                    }
                    else if (filePaths.Count() == 1 && filePaths[0] != null)
                    {
                        imageArray = System.IO.File.ReadAllBytes(filePaths[0]);
                    }

                    if (imageArray != null && imageArray.Length != 0)
                    {
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                        string imgSrc = "data:image/png;base64," + base64ImageRepresentation.Trim();
                        this.GetTestInstanceOfReport.Log(LogStatus.Fail, "Test Got Failed Due To Exception/Error", TestContext.CurrentContext.Result.Message);
                        this.GetTestInstanceOfReport.Log(LogStatus.Fail, "Screenshot Taken while eror occured " + DriverContext.ExtentStepTest.AddBase64ScreenCapture(imgSrc));
                        this.GetTestInstanceOfReport.Log(LogStatus.Fail, "***End  of test case (" + this.DriverContext.TestTitle + ")***", "Please see above Screenshot for more details ");
                    }
                }

                var javaScriptErrors = this.DriverContext.LogJavaScriptErrors();
                if (this.IsVerifyFailedAndClearMessages(this.driverContext) && TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
                {
                }

                if (javaScriptErrors)
                {
                    // Assert.Fail("JavaScript errors found. See the logs for details");
                }

                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
                {
                    JsonContext.TestsStatus = "PASS";
                    JsonContext.TestsComment = "Execution Completed Successfully.";
                }
                else
                {
                    JsonContext.TestsStatus = "FAIL";
                    JsonContext.TestsComment = TestContext.CurrentContext.Result.Message;
                }

                this.LogTest.LogTestEnding(this.driverContext);
                this.GetTestInstanceOfReport.Log(LogStatus.Info, "***End  of test case (" + this.DriverContext.TestTitle + ")***", "Test case Completed Successfully");

                if (BaseConfiguration.JiraResult == "MethodLevel")
                {
                    extent.EndTest(DriverContext.ExtentStepTest);
                    extent.Flush();
                    this.SaveAttachmentsToTestContextPipeLine(this.filename);
                }
                else
                {
                    SuiteLevelConfiguration.SuiteExtent.EndTest(DriverContext.ExtentStepTest);
                }
            }
            catch (Exception)
            {
                this.DriverContext.Stop();
                throw;
            }
            finally
            {
                this.DriverContext.Stop();
            }

            // if (BaseConfiguration.JiraResult == "MethodLevel")
            // {
            //    extent.EndTest(DriverContext.ExtentStepTest);
            //    extent.Flush();
            //    this.SetJSONStringforTest();
            //    this.UpdateJSONStringforInfoTestTemplate();
            //    if (BaseConfiguration.PublishJira == "Yes")
            //    {
            //        SuiteLevelConfiguration.PostToJIRA();
            //    }
            // }
            // else
            // {
            //    extent.EndTest(DriverContext.ExtentStepTest);
            //    extent.Flush();
            //    this.SetJSONStringforTest();
            // }
        }

        private void SaveAttachmentsToTestContext(string[] filePaths)
        {
            if (filePaths != null)
            {
                foreach (var filePath in filePaths)
                {
                    this.LogTest.Info("Uploading file [{0}] to test context", filePath);
                    if (filePath != null || filePath == string.Empty)
                    {
                        TestContext.AddTestAttachment(filePath);
                    }
                }
            }
        }

        private void SaveAttachmentsToTestContextPipeLine(string filePaths)
        {
            if (filePaths != null)
            {
                this.LogTest.Info("Uploading file [{0}] to test context", filePaths);
                if (filePaths != null || filePaths == string.Empty)
                {
                    TestContext.AddTestAttachment(filePaths);
                }
            }
        }

        private void UpdateJSONStringforInfoTestTemplate()
        {
            string json = File.ReadAllText(TestContext.CurrentContext.TestDirectory + "\\DataDriven\\XrayUpdated.json");
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            jsonObj["info"]["description"] = JsonContext.InfoDescription;
            jsonObj["info"]["startDate"] = JsonContext.InfoStartDate;
            jsonObj["info"]["finishDate"] = DateHelper.CurrentDateTimeZoneStamp;
            JArray arrJson = new JArray();
            arrJson.Add(this.SetJSONStringforTest());
            jsonObj["tests"] = arrJson;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            string fileName = TestContext.CurrentContext.TestDirectory + "\\DataDriven\\XrayTestMethodOutput\\" + TestContext.CurrentContext.Test.MethodName + DateHelper.CurrentTimeStamp + ".json";
            File.Create(fileName).Dispose();
            File.WriteAllText(fileName, output);
            SuiteLevelConfiguration.JsonFile = fileName;
        }

        private object SetJSONStringforTest()
        {
            string jsonTestTemp = File.ReadAllText(TestContext.CurrentContext.TestDirectory + "\\DataDriven\\TestTemplate.json");
            dynamic jsonObj = JsonConvert.DeserializeObject(jsonTestTemp);
            jsonObj["testKey"] = JsonContext.TestsTestKey;
            jsonObj["start"] = JsonContext.TestsStart;
            jsonObj["finish"] = JsonContext.TestsFinish;
            jsonObj["comment"] = JsonContext.TestsComment;
            jsonObj["status"] = JsonContext.TestsStatus;
            string base64str = this.ConvertHTMLToBase64String(this.filename);
            JsonContext.TestsEvidencesData = base64str;
            jsonObj["evidences"][0]["data"] = JsonContext.TestsEvidencesData;
            jsonObj["evidences"][0]["filename"] = JsonContext.TestsEvidencesFileName;
            SuiteLevelConfiguration.JsonTestMethods.Add(jsonObj);
            return jsonObj;
        }

        private string ConvertHTMLToBase64String(string filepath)
        {
            byte[] bytes = File.ReadAllBytes(filepath);
            string base64string = Convert.ToBase64String(bytes);
            return base64string;
        }

        private void AssertAll(Collection<ErrorDetail> verifyMessages)
        {
            List<string> errorlist = new List<string>();
            foreach (var s in verifyMessages)
            {
                if (s.Exception.InnerException != null)
                {
                    var p = s.Exception.InnerException.ToString();
                    var k = p.Substring(0, p.LastIndexOf("(Session info:")) + "\n";
                    errorlist.Add(k);
                }
                else
                {
                    var p = s.Exception.Message.ToString();
                    var k = p;
                    errorlist.Add(k);
                }
            }

            Verify.That(this.DriverContext, () => Assert.That(errorlist, Is.Empty, "Expected Soft assertions collection should be empty But It has following errors, for deatiled report check the html report"));
        }
    }
}