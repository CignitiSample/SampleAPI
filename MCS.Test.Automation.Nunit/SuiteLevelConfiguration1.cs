﻿// <copyright file="SuiteLevelConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.NUnit
{
    using System.Collections.Generic;
    using System.IO;
    using global::NUnit.Framework;
    using MCS.Test.Automation.Common;
    using MCS.Test.Automation.Common.Helpers;
    using MCS.Test.Automation.Common.Logger;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using RelevantCodes.ExtentReports;

    [SetUpFixture]
    public class SuiteLevelConfiguration
    {
        // private static IWebDriver driver;
        // private static ExtentTest test;
#pragma warning disable SA1401 // Fields must be private
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "Need  to supress")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Need  to supress")]
        public static List<object> JsonTestMethods = new List<object>();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "Need a List")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Need a List")]
        public static List<string> JsonTestEvidenceFileNameKey = new List<string>();
        public static ExtentReports SuiteExtent;
        public string Filename;
#pragma warning restore SA1401 // Fields must be private
        private readonly DriverContext driverContext = new DriverContext();

        public static string JsonFile { get; set; }

        public ExtentTest GetTestInstanceOfReport
        {
            get
            {
                return DriverContext.ExtentStepTest;
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

        public static void PostToJIRA()
        {
            RestXrayAPI.RestJson(JsonFile);
        }

        public static void PostToJIRAUsingNunit()
        {
            RestXrayAPI.RestNUnit();
        }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            JsonContext.DefectAttachmentFilePath = new List<string>();
            this.Filename = TestContext.CurrentContext.TestDirectory;

            if (BaseConfiguration.ReportOutsideBin == "Yes")
            {
                if (BaseConfiguration.JiraResult == "SuiteLevel")
                {
                    DirectoryInfo direc = Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.Parent;
                    string dirPath = direc.FullName + "\\SuiteLevelReport\\";
                    ////DirectoryInfo direc = Directory.GetParent(TestContext.CurrentContext.TestDirectory);
                    ////string dirPath = direc.FullName + BaseConfiguration.ExtentReportFolder;
                    foreach (var file in Directory.EnumerateFiles(dirPath, "*.html"))
                    {
                        File.Delete(file);
                    }

                    this.Filename = dirPath + "SuiteExecutionReport_" + DateHelper.CurrentTimeStamp + ".html";
                    SuiteLevelConfiguration.SuiteExtent = new ExtentReports(this.Filename, true);
                }
                else
                {
                    DirectoryInfo direc = Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.Parent;
                    string dirPath = direc.FullName + "\\MethodLevelReport\\";
                    ////DirectoryInfo direc = Directory.GetParent(TestContext.CurrentContext.TestDirectory);
                    ////string dirPath = direc.FullName + BaseConfiguration.ExtentReportFolder;
                    foreach (var file in Directory.EnumerateFiles(dirPath, "*.html"))
                    {
                        File.Delete(file);
                    }
                }
            }
            else
            {
                DirectoryInfo direc = Directory.GetParent(TestContext.CurrentContext.TestDirectory);
                string dirPath = direc.FullName + BaseConfiguration.ExtentReportFolder;
                foreach (var file in Directory.EnumerateFiles(dirPath, "*.html"))
                {
                    File.Delete(file);
                }

                this.Filename = dirPath + "SuiteExecutionReport_" + DateHelper.CurrentTimeStamp + ".html";
                SuiteLevelConfiguration.SuiteExtent = new ExtentReports(this.Filename, true);
            }

            JsonContext.InfoStartDate = DateHelper.CurrentDateTimeZoneStamp;
            this.DriverContext.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            FilesDeletionExtension.DeleteFilesWithExtension(this.DriverContext.ExtentReportFolder, "*.html");
            FilesDeletionExtension.DeleteFilesWithExtension(this.DriverContext.DownloadFolder, "*.pdf");
            FilesDeletionExtension.DeleteFilesWithExtension(this.DriverContext.MethodJsonFolder, "*.json", true);
            FilesDeletionExtension.DeleteFilesWithExtension(this.DriverContext.SuiteJsonFolder, "*.json", true);
            FilesDeletionExtension.DeleteFilesWithExtension(this.DriverContext.DownloadFolder, "*.*", true);
        }

        [OneTimeTearDown]
        public void RunAfterAnyTestsAsync()
        {
            if (BaseConfiguration.JiraResult == "SuiteLevel")
            {
                SuiteExtent.EndTest(DriverContext.ExtentStepTest);
                SuiteExtent.Flush();
                TestContext.AddTestAttachment(this.Filename);
                JsonContext.InfoFinishDate = DateHelper.CurrentDateTimeZoneStamp;
                this.GetJSONStringforSuite();
                if (BaseConfiguration.PublishJira == "Yes")
                {
                    SuiteLevelConfiguration.PostToJIRA();
                }
            }
        }

        private void GetJSONStringforSuite()
        {
            string json = File.ReadAllText(TestContext.CurrentContext.TestDirectory + "\\DataDriven\\XrayUpdated.json");
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            jsonObj["info"]["description"] = JsonContext.InfoDescription;
            jsonObj["info"]["startDate"] = JsonContext.InfoStartDate;
            jsonObj["info"]["finishDate"] = JsonContext.InfoFinishDate;
            JArray arrJson = new JArray();
            foreach (object obj in SuiteLevelConfiguration.JsonTestMethods)
            {
                arrJson.Add(obj);
            }

            jsonObj["tests"] = arrJson;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            string fileName = TestContext.CurrentContext.TestDirectory + "\\DataDriven\\XrayTestSuiteOutput\\SuiteOutput" + DateHelper.CurrentTimeStamp + ".json";
            File.Create(fileName).Dispose();
            File.WriteAllText(fileName, output);
            SuiteLevelConfiguration.JsonFile = fileName;
        }
    }
}
