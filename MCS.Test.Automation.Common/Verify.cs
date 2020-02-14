// <copyright file="Verify.cs" company="MCS">
// Copyright (c) MCS. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Common   
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using MCS.Test.Automation.Common.Extensions;
    using MCS.Test.Automation.Common.Helpers;
    using MCS.Test.Automation.Common.Types;
    using MCS.Test.Automation.Common.WebElements;
    using NLog;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using RelevantCodes.ExtentReports;
    using Assert = NUnit.Framework.Assert;
    
    /// <summary>
    /// Class for assert without stop testsw
    /// </summary>
    public static class Verify
    {
        private static readonly NLog.Logger Logger = LogManager.GetLogger("TEST");

        /// <summary>
        /// Verify group of assets
        /// </summary>
        /// <param name="driverContext">Container for driver</param>
        /// <param name="myAsserts">Group asserts</param>
        /// <example>How to use it: <code>
        /// Verify.That(
        ///     this.DriverContext,
        ///     () => Assert.AreEqual(5 + 7 + 2, forgotPassword.EnterEmail(5, 7, 2)),
        ///     () => Assert.AreEqual("Your e-mail's been sent!", forgotPassword.ClickRetrievePassword));
        /// </code></example>
        public static void That(DriverContext driverContext, params Action[] myAsserts)
        {
            That(driverContext, false, false, myAsserts);
        }

        /// <summary>
        /// Verify group of assets
        /// </summary>
        /// <param name="driverContext">Container for driver</param>
        /// <param name="enableScreenShot">Enable screenshot</param>
        /// <param name="enableSavePageSource">Enable save page source</param>
        /// <param name="myAsserts">Group asserts</param>
        /// <example>How to use it: <code>
        /// Verify.That(
        ///     this.DriverContext,
        ///     true,
        ///     false,
        ///     () => Assert.AreEqual(5 + 7 + 2, forgotPassword.EnterEmail(5, 7, 2)),
        ///     () => Assert.AreEqual("Your e-mail's been sent!", forgotPassword.ClickRetrievePassword));
        /// </code></example>
        public static void That(DriverContext driverContext, bool enableScreenShot, bool enableSavePageSource, params Action[] myAsserts)
        {
            foreach (var myAssert in myAsserts)
            {
                That(driverContext, myAssert, false, false);
            }

            if (!driverContext.VerifyMessages.Count.Equals(0) && enableScreenShot)
            {
                driverContext.TakeAndSaveScreenshot();
            }

            if (!driverContext.VerifyMessages.Count.Equals(0) && enableSavePageSource)
            {
                driverContext.SavePageSource(driverContext.TestTitle);
            }
        }

        /// <summary>
        /// Verify assert conditions
        /// </summary>
        /// <param name="driverContext">Container for driver</param>
        /// <param name="myAssert">Assert condition</param>
        /// <param name="enableScreenShot">Enabling screenshot</param>
        /// <param name="enableSavePageSource">Enable save page source</param>
        /// <example>How to use it: <code>
        /// Verify.That(this.DriverContext, () => Assert.IsFalse(flag), true);
        /// </code></example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "removed ref to unit test")]
        public static void That(DriverContext driverContext, Action myAssert, bool enableScreenShot, bool enableSavePageSource)
        {
            try
            {
                myAssert();
            }
            catch (Exception e)
            {
                if (enableScreenShot)
                {
                    driverContext.TakeAndSaveScreenshot();
                }

                if (enableSavePageSource)
                {
                    driverContext.SavePageSource(driverContext.TestTitle);
                }

                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));

                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
            }
        }

        /// <summary>
        /// Verify assert conditions
        /// </summary>
        /// <param name="driverContext">Container for driver</param>
        /// <param name="myAssert">Assert condition</param>
        /// <example>How to use it: <code>
        /// Verify.That(this.DriverContext, () => Assert.AreEqual(parameters["number"], links.CountLinks()));
        /// </code></example>
        public static void That(DriverContext driverContext, Action myAssert)
        {
            That(driverContext, myAssert, false, false);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "removed ref to unit test")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements must be documented
        public static void That(DriverContext driverContext, Action myAssert, string verifyMsg, string passMsg, string failMsg)
#pragma warning restore SA1600 // Elements must be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            try
            {
                myAssert();
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, verifyMsg, passMsg);
            }
            catch (Exception e)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, verifyMsg, failMsg);
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                TakeScreenShotAndADDToReports(driverContext);
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
            }
        }

        /// <summary>
        /// To Get the Message Content in the Table
        /// </summary>
        /// <param name="driverContext"></param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="name">Element Name.</param>
        /// <param name="index">Element Index text to be returned.</param>
        public static string GetTextFromListOfElementsBasedOnIndexSoftAssertion(DriverContext driverContext, ElementLocator locator, string name, int index)
        {
            try
            {
                IList<IWebElement> lstTableElements = driverContext.Driver.GetElements(locator);
                IWebElement e = lstTableElements[index];
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is visible ", name + " is " + e.Text + " visible successfully");
                Logger.Info(name + " is visible successfully");
                return e.Text;
            }
            catch (Exception e)
            {
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                Logger.Error("An exception occured while waiting for the elements to become visible " + e.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " are visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
                return string.Empty;
            }
        }

        public static string GetSingleValueFromDBCompareWithExpectedValue(DriverContext driverContext, string Query, string expectedValue, string message)
        {
            string result = string.Empty;
            try
            {
                result = SqlHelper.GetCountOnSQlQuery(Query);
                Assert.AreEqual(result, expectedValue);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify  " + message.Split('|')[0], message.Split('|')[0]);
                Logger.Info("Database Value are matching with expected result");
            }
            catch (Exception e)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + message.Split('|')[0], message.Split('|')[1]);
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                TakeScreenShotAndADDToReports(driverContext);
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
            }
            return result;
        }

        public static List<string> GetSingleColumnValuesAndCompareWithExpectedListFromDBWithSoftAssertion(DriverContext driverContext, string Query, string columnName, List<string> expectedList, string message)
        {
            List<string> colList = new List<string>();
            try
            {
                colList = SqlHelper.GetSingleColumnValue(Query, columnName);
                Assert.AreEqual(colList, expectedList);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify  " + message + "values matching with Database values", message + " values matching with Database values");
                Logger.Info("Database Value are matching with expected result");
            }
            catch (Exception e)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + message.Split('|')[0], message.Split('|')[1]);
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                TakeScreenShotAndADDToReports(driverContext);
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
            }
            return colList;
        }

        public static void CompareActualExpectedListWithSoftAssertion(DriverContext driverContext, List<string> actualList, List<string> expectedList,string passMessage,string errorMessage)
        {
            List<string> colList = new List<string>();
            try
            {
                Assert.That(actualList.OrderBy(t => t), Is.EqualTo(expectedList.OrderBy(t => t)));
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + passMessage.Split('|')[0], passMessage.Split('|')[1]);
                Logger.Info("Database Values are matching with expected result");
            }
            catch (Exception e)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + errorMessage.Split('|')[0], errorMessage.Split('|')[1]);
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                TakeScreenShotAndADDToReports(driverContext);
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
            }
        }


        public static List<string> GetSingleColumnValuesElementsMatchingwithFromDBIrrespectiveOfOrderWithSoftAssertion(DriverContext driverContext, string Query, string columnName, List<string> expectedList, string message)
        {
            List<string> colList = new List<string>();
            try
            {
                colList = SqlHelper.GetSingleColumnValue(Query, columnName);
                Assert.That(colList.OrderBy(t => t), Is.EqualTo(expectedList.OrderBy(t => t)));
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify  " + message + "values matching with Database values", message + " values matching with Database values");
                Logger.Info("Database Value are matching with expected result");
            }
            catch (Exception e)
            {
                var errorInBothObjects = colList.Except(expectedList).Concat(expectedList.Except(colList));
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify  " + message + "values matching with Database values", "Both expected and actual are not matching , here are the differences" + string.Join(", ", errorInBothObjects.Select(kvp => kvp)));
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                TakeScreenShotAndADDToReports(driverContext);
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
            }
            return colList;
        }

        /// <summary>
        /// Verify assert conditions
        /// </summary>
        /// <param name="webDriver">Webdriver</param>
        /// <param name="driverContext">Container for driver</param>
        /// <param name="locator">element locator</param>
        /// <param name="name"> element name</param>
        /// <example>How to use it: <code>
        /// Verify.That(this.DriverContext, () => Assert.AreEqual(parameters["number"], links.CountLinks()));
        /// </code></example>
        /// <returns>bool</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "removed ref to unit test")]
        public static bool AreElementsVisibleWithSoftAssertion(this IWebDriver webDriver, DriverContext driverContext, ElementLocator locator, string name)
        {
            try
            {
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(collection));
#pragma warning restore CS0618 // Type or member is obsolete
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " are visible", name + " are visible successfully");
                Logger.Info(name + " are displayed/Enabled successfully");
                return true;
            }
            catch (Exception e)
            {
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                Logger.Error("An exception occured while waiting for the elements to become visible " + e.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " are visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
                return false;
            }
        }

        public static string getTextIfElementIsVisibleWithSoftAssertionFromListOfElement(DriverContext driverContext, ElementLocator locator, string name)
        {
            try
            {
                var webElementLocator = driverContext.Driver.GetElements(locator);
                foreach (var item in webElementLocator)
                {

                    if (item.Text.Trim().Equals(name))
                    {
                        return item.Text;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                TakeScreenShotAndADDToReports(driverContext);
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                return string.Empty;
            }
            return string.Empty;
        }

        public static string getTextIfElementIsVisibleWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name)
        {
            try
            {

                if (IsElementVisibleWithSoftAssertion(driverContext.Driver, locator, name))
                {
                    var webElementLocator = driverContext.Driver.GetElement(locator);
                    driverContext.Driver.HighlightingWebElement(webElementLocator);
                    return driverContext.Driver.GetText(locator);
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                TakeScreenShotAndADDToReports(driverContext);
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                return string.Empty;
            }
        }


        /// <summary>
        /// Verify assert conditions
        /// </summary>
        /// <param name="webDriver">Webdriver</param>
        /// <param name="locator">element locator</param>
        /// <param name="name"> element name</param>
        /// <example>How to use it: <code>
        /// Verify.That(this.DriverContext, () => Assert.AreEqual(parameters["number"], links.CountLinks()));
        /// </code></example>
        /// <returns>bool</returns>
        public static bool IsElementVisibleWithSoftAssertion(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                webDriver.WaitForPageLoad();
                var webElementLocator = webDriver.GetElement(locator);
                if (webElementLocator.Displayed)
                {
                    webDriver.HighlightingWebElement(webElementLocator);

                }

                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is visible", name + " is visible successfully");

                Logger.Info(name + " is displayed/Enabled successfully");
                return webElementLocator.Displayed;
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                return false;
            }
        }

        /// <summary>
        /// Validate Text Present on Element with SoftAssertion
        /// </summary>
        /// <param name="driverContext">This is webdriver</param>
        /// <param name="locator">Name of the Locator</param>
        /// <param name="name">Element Name</param>
        /// <param name="textTobeVerified"> text to be verified</param>
        /// <returns>bool..</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "removed ref to unit test")]
        public static bool ValiadteTextPresentOnElementWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name, string textTobeVerified)
        {
            bool flag = false;
            string actualText = null;
            try
            {
                if (IsElementVisibleWithSoftAssertion(driverContext, locator, name))
                {
                    var webElementLocator = driverContext.Driver.GetElement(locator);
                    actualText = webElementLocator.Text;

                    // Assert.Contains("Verifying expected text" + textTobeVerified + "is present or not on element " + name, actualText.Contains(textTobeVerified).ToString(), textTobeVerified.ToUpper(CultureInfo.CurrentCulture).Trim());
                    Assert.AreEqual(actualText.ToUpper(CultureInfo.CurrentCulture).Trim(), textTobeVerified.ToUpper(CultureInfo.CurrentCulture).Trim(), "Verifying expected text" + textTobeVerified + "is present or not on element " + name);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify expected text ' " + textTobeVerified + "' is visible on " + name, "Error Message '" + textTobeVerified + "' text is visible successfully");
                    Logger.Info(textTobeVerified + " is visible successfully");
                    flag = true;
                }

                return flag;
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify expected '" + textTobeVerified + "' is visible on " + name, "Expected text: '" + textTobeVerified + "' is not matching with actual text:'" + actualText + "'");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
                return flag;
            }
        }

        // public void IsHelpDropDownDisplayedWithSoftAssertion(string textToBeVerified)
        // {
        //    Verify.That(this.DriverContext, () => Assert.IsTrue(Verify.ValiadteTextPresentOnElementWithSoftAssertion(this.Driver, this.helpDropDown, this.nmhelpDropDown, textToBeVerified), "Verifying " + textToBeVerified + " is displayed"));
        // }

        /// <summary>
        /// Enter Text into a Text Box
        /// </summary>
        /// <param name="driverContext">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="text">Text to be Entered into the text Box</param>
        /// <param name="name">Element name.</param>
        public static void EnterTextWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string text, string name)
        {
            try
            {
                var txtBox = driverContext.Driver.GetElement(locator);
                if (txtBox.Enabled)
                {
                    Logger.Info(name + " is viewed successfully");
                    HighlightingWebElementWithSoftAssertion(driverContext, txtBox);
                    txtBox.Click();
                    txtBox.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                    txtBox.Clear();
                    txtBox.SendKeys(text);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify the User is able to Enter (" + text + ") in the " + name, "(" + text + ") is Entered successfully in " + name);
                    Logger.Info(text + " is Entered successfully in " + name);
                }
            }
            catch (Exception e)
            {
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify the User is able to Enter " + text + " in the " + name, "Exception occured while entering text in " + name);
            }
        }

        /// <summary>
        /// Enable synchronization with angular.
        /// </summary>
        /// <param name="driverContext">The WebDriver.</param>
        /// <param name="webElementLocator">WebElementLocator</param>
        public static void HighlightingWebElementWithSoftAssertion(DriverContext driverContext, IWebElement webElementLocator)
        {
            try
            {
                var javaScriptExecutor = driverContext.Driver as IJavaScriptExecutor;
                javaScriptExecutor.ExecuteScript("arguments[0].style.border='3px solid red'", webElementLocator);
            }
            catch (Exception e)
            {
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);

            }
        }

        /// <summary>
        /// Enable synchronization with angular.
        /// </summary>
        /// <param name="driverContext">The WebDriver.</param>
        /// <param name="locator">Enable or disable synchronization.</param>
        /// <param name="name">Name of the Element</param>
        /// <returns>bool.</returns>
        public static bool IsElementVisibleWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var webElementLocator = driverContext.Driver.GetElement(locator);
                if (webElementLocator.Displayed)
                {
                    HighlightingWebElementWithSoftAssertion(driverContext, webElementLocator);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is visible", name + " is visible successfully");
                    Logger.Info(name + " is displayed/Enabled successfully");
                    return true;
                }
                else
                {
                    throw new ElementNotVisibleException(name + " is not displayed/Enabled but present in the Dom");
                }
            }
            catch (Exception e)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + e.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
                TakeScreenShotAndADDToReports(driverContext);
                return false;
            }

        }

        public static void TakeScreenShotAndADDToReports(DriverContext driverContext)
        {
            var filePaths = driverContext.TakeAndSaveScreenshot();
            if (filePaths == null || filePaths.Length == 0)
            {
            }
            else
            {
                TestContext.AddTestAttachment(filePaths[0]);
                byte[] imageArray = null;
                if (filePaths.Length >= 2)
                {
                    imageArray = System.IO.File.ReadAllBytes(filePaths[filePaths.Length - 2]);
                }
                else if (filePaths.Length == 1 && filePaths[0] != null)
                {
                    imageArray = System.IO.File.ReadAllBytes(filePaths[0]);
                }

                if (imageArray != null && imageArray.Length != 0)
                {
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                    string imgSrc = "data:image/png;base64," + base64ImageRepresentation.Trim();
                    DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Screenshot Taken while eror occured. Please see Screenshot for details  " + DriverContext.ExtentStepTest.AddBase64ScreenCapture(imgSrc));
                }
            }
        }

        public static int GetCountOfCheckedUnCheckedCheckBoxSoftAssertion(DriverContext driverContext, ElementLocator locator, string name, string ischecked)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var webElementLocator = driverContext.Driver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                return collection.Count();
            }
            catch (Exception e)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + e.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify count of the " + ischecked + " checkbox box", "An exception occurred waiting for " + name + " to become visible");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
                TakeScreenShotAndADDToReports(driverContext);
                return 0;
            }
        }

        /// <summary>
        /// Enable synchronization with angular.
        /// </summary>
        /// <param name="driverContext">The WebDriver.</param>
        /// <param name="locator">Enable or disable synchronization.</param>
        /// <param name="name">Name of the Element</param>
        public static void IsElementClickableWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var webElementLocator = driverContext.Driver.GetElement(locator);
                WaitForClickableWithSoftAssertion(driverContext, webElementLocator, name);
                if (webElementLocator.Displayed && webElementLocator.Enabled)
                {
                    webElementLocator.Click();
                    Thread.Sleep(1000);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is clicked ", name + " is clicked successfully");
                    Logger.Info(name + " is clicked successfully");
                }
            }
            catch (Exception e)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + e.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
                TakeScreenShotAndADDToReports(driverContext);

            }
        }


        /// <summary>
        /// Wait for the page load to complete.
        /// </summary>
        /// <param name="driverContext">The WebDriver.</param>
        public static void WaitForPageLoadWithSoftAssertion(DriverContext driverContext)
        {
            new WebDriverWait(driverContext.Driver, TimeSpan.FromSeconds(BaseConfiguration.LongTimeout)).Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
        /// <summary>
        /// This is for waitforelementclickable
        /// </summary>
        /// <param name="driverContext">this is webdiver</param>
        /// <param name="element">Iweb element</param>
        /// <param name="name">name of element</param>
        public static void WaitForClickableWithSoftAssertion(DriverContext driverContext, IWebElement element, string name)
        {
            try
            {
                var wait = new WebDriverWait(driverContext.Driver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.ElementToBeClickable(element));
                Logger.Info(wait + "  waited successfully ");
                /*
                 * ExtentTestManager.getTest().log(LogStatus.PASS, "To verify " + webElementName
                 * + " is clickable", webElementName + " to become clickable.");
                 */
            }
            catch (Exception e)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clickable", "An exception occurred waiting for " + name + " to become clickable.");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
                TakeScreenShotAndADDToReports(driverContext);

            }
        }

        /// Validate Text Contains on Element with SoftAssertion
        /// <summary>
        /// <param name="driverContext">This is webdriver</param>
        /// <param name="locator">Name of the Locator</param>
        /// <param name="name">Element Name</param>
        /// <param name="textTobeVerified"> text to be verified</param>
        /// <returns>bool..</returns>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "removed ref to unit test")]
        public static bool ValidateTextContainsOnElementWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name, string textTobeVerified)
        {
            bool flag = false;
            string actualText = null;
            try
            {
                if (IsElementVisibleWithSoftAssertion(driverContext.Driver, locator, name))
                {
                    var webElementLocator = driverContext.Driver.GetElement(locator);
                    actualText = webElementLocator.Text;

                    Assert.IsTrue(actualText.ToUpper(CultureInfo.CurrentCulture).Trim().Contains(textTobeVerified.ToUpper(CultureInfo.CurrentCulture).Trim()));
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify expected text " + textTobeVerified + " is visible on " + name, textTobeVerified + " text is visible successfully");
                    Logger.Info(textTobeVerified + " is visible successfully");
                    flag = true;
                }

                return flag;
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify expected " + textTobeVerified + " is visible on " + name, "Expected text: " + textTobeVerified + " is not contained in actual text:" + actualText);
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
                return flag;
            }

        }

        public static void CompareTwoDictionaryFromPageAndDBWithSoftAssertion(DriverContext driverContext, Dictionary<string, string> pageobject, Dictionary<string, string> dbObject)
        {
            var errorInBothObjects = pageobject.Except(dbObject).Concat(pageobject.Except(dbObject));
            string verifyMessage = "To verify  entered values : " + string.Join(", ", pageobject.Select(kvp => kvp.Key + "_" + kvp.Value)) + " are  saved successfully in respective columns in database";
            string passMessage = "Entered values are  saved successfully in respective columns in database";
            string failMessage = "Error occured !! both expected and actual dictionary not matching , here are the Differences: " + string.Join(", ", errorInBothObjects.Select(kvp => kvp.Key + "-" + kvp.Value));
            try
            {
                Assert.AreEqual(pageobject, dbObject);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, verifyMessage, passMessage);
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, verifyMessage, failMessage);
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
            }

        }

        /// <summary>
        /// Handler for simple use isAlertPresent.
        /// </summary>
        /// <param name="driverContext">The web driver.</param>
        /// <param name="expectedAlert">Alert Message</param>
        /// <returns>JavaScriptAlert Handle</returns>
        public static bool IsAlertPresentwithExpectedTextWithSoftAssertion(DriverContext driverContext, string expectedAlert)
        {
            bool presentFlag = false;

            try
            {
                // Check the presence of alert
                IAlert alert = driverContext.Driver.SwitchTo().Alert();

                // Alert present; set the flag
                presentFlag = true;
                Assert.AreEqual(expectedAlert, alert.Text);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Alert is present ", expectedAlert + " Alert " + " is present successfully");
                Logger.Info(expectedAlert + " Alert is present successfully");

                // if present consume the alert
                alert.Accept();

                // ( Now, click on ok or cancel button )
            }
            catch (NoAlertPresentException ex)
            {

                presentFlag = false;

                Logger.Error("Failed to click on Alert Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + expectedAlert + " Alert is clicked ", "An exception occurred while clicking on " + expectedAlert);
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }

            return presentFlag;
        }

        public static void CheckElementPresentOrNotWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name, string SearchText)
        {
            try
            {
                if (SearchText == string.Empty)
                {
                    SearchText = "No Text for this Element";
                }

                WaitForPageLoadWithSoftAssertion(driverContext);
                var by = locator.ToBy();
                IWebElement element = driverContext.Driver.FindElement(by);
                HighlightingWebElementWithSoftAssertion(driverContext, element);
                Logger.Info(name + " : element is found successfully, Text Found :" + SearchText);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " element is visible ", name + " : element is found successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.MediumTimeout, "An exception occurred waiting for " + name + " to become clickable");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// Waits the until element is found or loaded.
        /// </summary>
        /// <example>Sample code to check page title: <code>
        /// this.Driver.WaitUntilElementIsFound(AppearingInfo, BaseConfiguration.ShortTimeout);
        /// </code></example>
        /// <param name="driverContext">The web driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="elementName"></param>
        /// <param name="timeout">The timeout.</param>

        public static bool WaitUntilElementIsFoundWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string elementName, double timeout)
        {
            try
            {
                var wait = new WebDriverWait(driverContext.Driver, TimeSpan.FromSeconds(timeout));
                wait.Until(driver => driverContext.Driver.GetElements(locator).Count >= 1);
                Logger.Info(timeout + "  waited successfully ");
                return true;
            }
            catch (Exception e)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + e.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + elementName + " is visible with in provided time " + timeout, "An exception occurred waiting for " + elementName + " to become visible on provided time (" + timeout + ")");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
                TakeScreenShotAndADDToReports(driverContext);
                return false;
            }
        }

        public static void IsElementClickableFromListofElementWithTextJavaScriptWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string link)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var webElementLocator = driverContext.Driver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(driverContext.Driver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(collection));
#pragma warning restore CS0618 // Type or member is obsolete
                foreach (var item in collection)
                {
                    if (item.Text.Contains(link))
                    {
                        item.JavaScriptClick();
                        break;
                    }
                }
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + link + " is visible", link + " is Clicked successfully");
                Logger.Info(link + " are enable/Clickable successfully");

            }
            catch (Exception ex)
            {

                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + link + " is visible with in provided time " + BaseConfiguration.MediumTimeout, "An exception occurred waiting for " + link + " to become clickable");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }
        /// <summary>
        /// Waits the until element is no longer found.
        /// </summary>
        /// <example>Sample code to check page title: <code>
        /// this.Driver.WaitUntilElementIsNoLongerFound(dissapearingInfo, BaseConfiguration.ShortTimeout);
        /// </code></example>
        /// <param name="driverContext">The web driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="elementName"></param>
        /// <param name="timeout">The timeout.</param>
        public static void WaitUntilElementIsNoLongerFoundWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string elementName, double timeout)
        {
            try
            {
                var wait = new WebDriverWait(driverContext.Driver, TimeSpan.FromSeconds(timeout));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));
                wait.Until(driver => driverContext.Driver.GetElements(locator).Count == 0);
                Logger.Info(timeout + " waited successful  ");
            }
            catch (Exception e)
            {
                Logger.Error("An exception occured while waiting for the element to become disappear " + e.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + elementName + " is not visible with in provided time  " + timeout, "An exception occurred waiting for " + elementName + " to become disappeared on provided time (" + timeout + ")");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, e));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", e);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// Checking Alphabets from list of strings
        /// </summary>
        /// <param name="driverContext">this is webdriver</param>
        /// <param name="locator">element locator</param>
        /// <param name="link">name of element</param>
        /// <param name="ascordesc"></param>
        public static void AreElementsSortedInAlphabeticalOrderWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string link, string ascordesc = "")
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var webElementLocator = driverContext.Driver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var x = collection.Select(item => item.Text.Replace(System.Environment.NewLine, ""));
                var sorted = new List<string>();
                if (ascordesc == "desc")
                {
                    sorted.AddRange(x.OrderByDescending(o => o));
                }
                else
                {
                    sorted.AddRange(x.OrderBy(o => o));
                }

                Assert.IsTrue(x.SequenceEqual(sorted));
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + link + " is visible", link + " is visible successfully");
                Logger.Info(link + " are enable/Clickable successfully");
            }
            catch (Exception ex)
            {

                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + link + " is visible with in provided time " + BaseConfiguration.MediumTimeout, "An exception occurred waiting for " + link + " to become clickable");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        public static void AreElementsSortedInorderForDateTimeWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string link, string ascordesc = "", string Isdatetime = "", string replacetext = "")
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var webElementLocator = driverContext.Driver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);

                if (string.IsNullOrEmpty(Isdatetime))
                {
                    if (ascordesc == "desc")
                    {
                        Assert.IsTrue(collection.OrderByDescending(c => Convert.ToInt32(c.Text)).SequenceEqual(collection));
                    }
                    else
                    {
                        Assert.IsTrue(collection.OrderBy(c => Convert.ToInt32(c.Text)).SequenceEqual(collection));
                    }
                }
                else
                {
                    if (ascordesc == "desc")
                    {
                        if (!string.IsNullOrEmpty(replacetext))
                        {
                            List<string> elementlist = new List<string>();

                            foreach (var element in collection)
                            {
                                string addelement = element.Text;
                                foreach (string replace in replacetext.Split(','))
                                {
                                    addelement = addelement.Replace(replace, " ");
                                }
                                elementlist.Add(addelement);
                            }
                            Assert.IsTrue(elementlist.OrderByDescending(c => Convert.ToDateTime(c)).SequenceEqual(elementlist));
                        }
                        else
                        {
                            Assert.IsTrue(collection.OrderByDescending(c => Convert.ToDateTime(c.Text)).SequenceEqual(collection));
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(replacetext))
                        {

                            List<string> elementlist = new List<string>();

                            foreach (var element in collection)
                            {
                                string addelement = element.Text;
                                foreach (string replace in replacetext.Split(','))
                                {
                                    addelement = addelement.Replace(replace, " ");
                                }
                                elementlist.Add(addelement);
                            }
                            Assert.IsTrue(elementlist.OrderBy(c => Convert.ToDateTime(c)).SequenceEqual(elementlist));
                        }
                        else
                        {
                            Assert.IsTrue(collection.OrderBy(c => Convert.ToDateTime(c.Text)).SequenceEqual(collection));
                        }
                    }
                }
                Logger.Info(link + " are enable/Clickable successfully");
            }

            catch (Exception ex)
            {

                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + link + " is visible with in provided time " + BaseConfiguration.MediumTimeout, "An exception occurred waiting for " + link + " to become clickable");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// IsGrayedOutLinksDisplayed
        /// </summary>
        /// <param name="driverContext">This is webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="name">Name of the Element</param>
        /// <param name="linksName">link name</param>
        public static void IsGrayedOutLinksClickableWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name, string linksName)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                IList<IWebElement> webElementLocator = driverContext.Driver.GetElements(locator);
                foreach (var item in webElementLocator)
                {
                    if (item.Text.Contains(linksName))
                    {
                        item.Click();
                        break;
                    }

                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is clickable ", name + " " + " is Clicked successfully");
                    Logger.Info(name + " is clicked successfully");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.MediumTimeout, "An exception occurred waiting for " + name + " to become clickable");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// This method is used to click on selecte  element from list 
        /// </summary>
        /// <param name="driverContext">This is webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="link">name of the link</param>
        public static void IsElementClickableFromListofElementWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string link)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var webElementLocator = driverContext.Driver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(driverContext.Driver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(collection));
#pragma warning restore CS0618 // Type or member is obsolete
                collection.First().Click();
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + link + " is visible", link + " is Clicked successfully");
                Logger.Info(link + " are enable/Clickable successfully");

            }
            catch (Exception ex)
            {

                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + link + " is visible with in provided time " + BaseConfiguration.MediumTimeout, "An exception occurred waiting for " + link + " to become clickable");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// This method is used to click on selecte  element from list 
        /// </summary>
        /// <param name="driverContext">This is webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="link">name of the link</param>
        public static void IsElementClickableFromListofElementWithTextWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string link)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var webElementLocator = driverContext.Driver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(driverContext.Driver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(collection));
#pragma warning restore CS0618 // Type or member is obsolete
                foreach (var item in collection)
                {
                    if (item.Text.Contains(link))
                    {
                        item.Click();
                        break;
                    }
                }
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + link + " is visible", link + " is Clicked successfully");
                Logger.Info(link + " are enable/Clickable successfully");

            }
            catch (Exception ex)
            {

                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + link + " is visible with in provided time " + BaseConfiguration.MediumTimeout, "An exception occurred waiting for " + link + " to become clickable");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// User Menu Items
        /// </summary>
        /// <param name="driverContext">Webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="link">Element type</param>
        public static void IsUserMenuItemClickableWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string link)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var webElementLocator = driverContext.Driver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(driverContext.Driver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(collection));
#pragma warning restore CS0618 // Type or member is obsolete
                foreach (var item in collection)
                {
                    if (item.Text.Contains(link))
                    {
                        item.Click();
                    }
                }

                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + link + " are visible", link + " are visible successfully");
                Logger.Info(link + " are displayed/Enabled successfully");

            }
            catch (Exception ex)
            {

                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + link + " is visible with in provided time " + BaseConfiguration.MediumTimeout, "An exception occurred waiting for " + link + " to become clickable");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// Click on element from the list of records
        /// </summary>
        /// <param name="driverContext">this is webdriver</param>
        /// <param name="locator">element locator</param>
        /// <param name="name">name of an element</param>
        /// <param name="link">name of link</param>
        public static void ClickOnMembershipTypeFromListofRecordsWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name, string link)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var webElementLocator = driverContext.Driver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(driverContext.Driver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(collection));
#pragma warning restore CS0618 // Type or member is obsolete
                foreach (var item in collection)
                {
                    string value = item.Text;
                    string[] arrValue = value.Split(' ');
                    string finalValue = arrValue[0];
                    if (finalValue.Trim().Contains(link))
                    {
                        item.Click();
                        break;
                    }
                }

                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + link + " are visible", link + " are visible successfully");
                Logger.Info(link + " are displayed/Enabled successfully");

            }
            catch (Exception ex)
            {

                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + link + " is visible with in provided time " + BaseConfiguration.MediumTimeout, "An exception occurred waiting for " + link + " to become clickable");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// Scrolling To WebElement.
        /// </summary>
        /// <param name="driverContext">The WebDriver.</param>
        /// <param name="webElementLocator">WebElementLocator</param>
        public static void ScrollToWebElementWithSoftAssertion(DriverContext driverContext, IWebElement webElementLocator)
        {
            try
            {
                var javaScriptExecutor = driverContext as IJavaScriptExecutor;
                javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView();", webElementLocator);
            }
            catch (Exception ex)
            {
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// To Verify element from list oif elements
        /// </summary>
        /// <param name="driverContext">This is webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="name">name of element</param>
        /// <returns>bool..</returns>
        public static bool IsElementVisibleFromListOfElementWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name)
        {
            bool elementVisible = false;
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var webElementLocator = driverContext.Driver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(driverContext.Driver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(collection));
#pragma warning restore CS0618 // Type or member is obsolete
                foreach (var item in collection)
                {
                    if (item.Text.Equals(name))
                    {
                        HighlightingWebElementWithSoftAssertion(driverContext, item);
                        ScrollToWebElementWithSoftAssertion(driverContext, item);
                        Logger.Info(item.Text + " element visible successfully");
                        DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " are visible", name + " are visible successfully");
                        Logger.Info(name + " are displayed/Enabled successfully");
                        elementVisible = true;
                        break;

                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
            return elementVisible;
        }

        /// <summary>
        /// This Method is used to get the actual text and match with expected text
        /// </summary>
        /// <param name="driverContext">this is webdiver</param>
        /// <param name="locator">Locator of Webelement</param>
        /// <param name="expectedText">Expected from Text from source File</param>
        /// <param name="actualText">Actual text </param>
        public static void IsExpectedTextMatchWithActualTextWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string expectedText, string actualText = "")
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var webElementLocator = driverContext.Driver.GetElement(locator);
                actualText = webElementLocator.Text.Trim();
                Assert.AreEqual(actualText, expectedText);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Expected Value (" + expectedText + ") is matching with Actual text ", "The expected Value is (" + expectedText + ") and  actual value is (" + actualText + ") matching successfully");
                Logger.Info("Expected text " + expectedText + " and Actual text is " + actualText);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to verify text on (" + expectedText + ") with Actual Text (" + actualText + ") Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify text on (" + expectedText + ")with Actual Text (" + actualText + ")", "An exception occurred while finding text on (" + expectedText + ")");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// This Method is used to get the actual text and match with expected text
        /// </summary>
        /// <param name="driverContext">this is webdiver</param>
        /// <param name="locator">Locator of Webelement</param>
        /// <param name="expectedText">Expected from Text from source File To Lower</param>
        /// <param name="actualText">Actual text </param>
        public static void IsExpectedTextMatchWithActualTextToLowerWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string expectedText, string actualText)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var webElementLocator = driverContext.Driver.GetElement(locator);

                actualText = webElementLocator.Text.Trim();
                string actual = actualText.ToLower();
                string expected = expectedText.ToLower();
                Assert.AreEqual(expected, actual);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Expected Value is matching with Actual text ", "The expected Value is " + expectedText + " and  actual value is " + actualText + " matching successfully");
                Logger.Info("Expected text " + expectedText + " and Actual text is " + actualText);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to verify text on " + expectedText + "with Actual Text " + actualText + " Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify expectedText ' " + expectedText + " ' with Actual Text " + actualText, "An exception occurred while verifying  actualtext ' " + actualText + " ' with expectedtext " + expectedText);
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        public static void IsElementVisibleHiddenModeWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var by = locator.ToBy();
                var webElement = driverContext.Driver.FindElement(by);
                HighlightingWebElementWithSoftAssertion(driverContext, webElement);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is visible ", name + " is visible successfully");
                Logger.Info(name + " is visible successfully");

            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element is visible" + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is not visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become present in hidden mode");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        public static void IsElementClickableinHiddenModeWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var by = locator.ToBy();
                var webElement = driverContext.Driver.FindElement(by);
                webElement.Click();
                Thread.Sleep(1000);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is clicked ", name + " is clicked successfully");
                Logger.Info(name + " is clicked successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }
        /// <summary>
        /// Click an Element Based on Index
        /// </summary>
        /// <param name="driverContext"></param>
        /// <param name="locator"></param>
        /// <param name="name"></param>
        /// <param name="index"></param>
        public static void IsElementClickableBasedonIndexWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name, int index)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                IList<IWebElement> lstTableElements = driverContext.Driver.GetElements(locator);
#pragma warning disable CS0162 // Unreachable code detected
                for (int i = 0; i <= lstTableElements.Count; i++)
#pragma warning restore CS0162 // Unreachable code detected
                {
                    WaitForPageLoadWithSoftAssertion(driverContext);

                    IWebElement e = lstTableElements[index];
                    e.JavaScriptClick();
                    break;
                }
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is clicked ", name + " is clicked successfully");
                Logger.Info(name + " is clicked successfully");
            }
            catch (TimeoutException ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// Handler for simple use isAlertPresent.
        /// </summary>
        /// <param name="driverContext">The web driver.</param>
        /// <returns>JavaScriptAlert Handle</returns>
        public static bool IsAlertPresentWithSoftAssertion(DriverContext driverContext)
        {
            bool presentFlag = false;

            try
            {
                // Check the presence of alert
                IAlert alert = driverContext.Driver.SwitchTo().Alert();

                // Alert present; set the flag
                presentFlag = true;

                // if present consume the alert
                alert.Accept();

                // ( Now, click on ok or cancel button )
            }
            catch (NoAlertPresentException)
            {
                // Alert not present
                // throw;
                presentFlag = false;
            }

            return presentFlag;
        }

        /// <summary>
        /// Perform a Click through JavaScript
        /// </summary>
        /// <param name="name">name of the element.</param>
        /// <param name="driverContext">The web driver</param>
        /// <param name="element">The web element.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = " Need to handle exception ")]
        public static void JavaScriptClickWithSoftAssertion(DriverContext driverContext, IWebElement element, string name)
        {
            try
            {
                var javaScriptExecutor = driverContext as IJavaScriptExecutor;
                javaScriptExecutor.ExecuteScript("arguments[0].click();", element);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is clicked ", name + " is clicked successfully");
                Logger.Info(name + " is clicked successfully");
            }
            catch (UnhandledAlertException)
            {
                if (IsAlertPresentWithSoftAssertion(driverContext))
                {
                    driverContext.Driver.SwitchTo().Alert().Dismiss();
                    var javaScriptExecutor = driverContext as IJavaScriptExecutor;
                    javaScriptExecutor.ExecuteScript("arguments[0].click();", element);
                    DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                    Logger.Error("Failed to click on " + name);
                    throw;
                }
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// Perform a enter text through JavaScript
        /// </summary>
        /// <param name="driverContext">The web driver</param>
        /// <param name="locator">ElementLocator.</param>
        /// <param name="name">name of the element.</param>
        /// <param name="testdata">Text to be written.</param>
        public static void ClearAndTypeInEditBoxUsingJSWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name, string testdata)
        {
            try
            {
                var element = driverContext.Driver.GetElement(locator);
                element.Clear();
                element.Click();
                var javaScriptExecutor = driverContext as IJavaScriptExecutor;
                javaScriptExecutor.ExecuteScript("arguments[0].value='" + testdata, element);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify user able clear and type test data  as " + testdata + " in " + name, "User able to type/Enter test data as " + testdata + " in " + name + " successfully");
                Logger.Info(testdata + " is entered in " + name);
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify user able to enter/type required data " + testdata + " in " + name, "An exception occurred waiting for " + name + " to enter any value");
                Logger.Error("Failed tp enter " + testdata + " in " + name);
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// To mouse hover on given webelement
        /// </summary>
        /// <param name="driverContext">The web driver.</param>
        /// <param name="locator">ElementLocator.</param>
        /// <param name="webelementname">webelementname.</param>
        public static void MouseOverOnWebElementAndClickWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string webelementname)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var web_Element_To_Be_Hovered = driverContext.Driver.GetElement(locator);
                HighlightingWebElementWithSoftAssertion(driverContext, web_Element_To_Be_Hovered);
                Actions builder = new Actions(driverContext.Driver);
                builder.MoveToElement(web_Element_To_Be_Hovered).Build().Perform();
                builder.Click().Perform();
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify mouse hover on " + webelementname, "Mouse hovered on " + webelementname + " successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while mouse hovering on  " + webelementname + " " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify mouse hover on " + webelementname, "An exception occured while mouse hovering on  " + webelementname);
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// To mouse hover on given webelement
        /// </summary>
        /// <param name="driverContext">The web driver.</param>
        /// <param name="locator">ElementLocator.</param>
        public static void MouseOverOnWebElementWithSoftAssertion(DriverContext driverContext, ElementLocator locator)
        {
            try
            {
                WaitForPageLoadWithSoftAssertion(driverContext);
                var web_Element_To_Be_Hovered = driverContext.Driver.GetElement(locator);
                HighlightingWebElementWithSoftAssertion(driverContext, web_Element_To_Be_Hovered);
                Actions builder = new Actions(driverContext.Driver);
                builder.MoveToElement(web_Element_To_Be_Hovered).Build().Perform();
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify user able to select " + " is mouseover successfully");
                Logger.Info(locator + " is mouseover successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while mouse hovering on  " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify mouse hover on ", "An exception occured while mouse hovering on  ");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <param name="driverContext">The WebDriver.</param>
        /// <param name="locator">Enable or Disable</param>
        /// <param name="name">Name of the Element</param>
        public static void IsElementcontainsGivenattributeWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name)
        {
            try
            {
                driverContext.Driver.WaitForPageLoad();
                var element = driverContext.Driver.GetElement(locator);
                bool expectedstatus = false;
                bool status;
                try
                {
                    status = element.GetAttribute("type").Contains("text");
                }
                catch (Exception)
                {
                    status = false;
                }
                Assert.IsFalse(status);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify type attribute for " + name + " is not available", "Type attribute for" + name + " is not available");
                Logger.Info("Expected bool value" + expectedstatus + " and Actual bool value is " + status);
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify type attribute for " + name + " is not available", "Type attribute for" + name + " is  available");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        public static void IsElementGivenAttributePresentWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string name, string attributeName, string attributeText, string isReadOnly = "")
        {
            try
            {
                driverContext.Driver.WaitForPageLoad();
                bool expectedstatus = false;
                bool status = true;
                if (isReadOnly != string.Empty)
                {
                    var by = locator.ToBy();
                    var readOnlyElement = driverContext.Driver.FindElement(by);
                    try
                    {
                        status = readOnlyElement.GetAttribute(attributeName).Contains(attributeText);
                    }
                    catch (Exception)
                    {
                        status = false;
                    }
                }
                else
                {
                    var element = driverContext.Driver.GetElement(locator);
                    try
                    {
                        status = element.GetAttribute(attributeName).Contains(attributeText);
                    }
                    catch (Exception)
                    {
                        status = false;
                    }
                }
                Assert.IsTrue(status);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify type attribute for " + name + " is not available", "Type attribute for" + name + " is not available");
                Logger.Info("Expected bool value" + expectedstatus + " and Actual bool value is " + status);
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify type attribute for " + name + " is not available", "Type attribute for" + name + " is  available");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driverContext"></param>
        /// <param name="locator"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public static void IsElementContainGivenAttributeWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string text, string value)
        {
            try
            {
                driverContext.Driver.WaitForPageLoad();
                var element = driverContext.Driver.GetElement(locator);
                bool expectedstatus = false;
                bool status;
                try
                {
                    status = element.GetAttribute(value).Contains(text);
                }
                catch (Exception)
                {
                    status = false;
                }
                Assert.IsFalse(status);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify type attribute for " + text + " is not available", "Type attribute for" + text + " is not available");
                Logger.Info("Expected bool value" + expectedstatus + " and Actual bool value is " + status);
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify type attribute for " + text + " is not available", "Type attribute for" + text + " is  available");
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// Select an element by index with Timeout
        /// </summary>
        /// <param name="driverContext">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="index">Index</param>
        /// <param name="timeout">Time Out</param>
        public static void SelectByIndexWithCustomTimeoutWithSoftAssertion(DriverContext driverContext, ElementLocator locator, int index, int timeout)
        {
            try
            {
                Select select = driverContext.Driver.GetElement<Select>(locator, timeout);
                select.SelectByIndex(index, timeout);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify user able to select " + index + ":" + index + " is selected successfully");
                Logger.Info(index + " is selected successfully");
            }

            catch (Exception ex)
            {
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }

        }

        /// <summary>
        /// Select an element by index
        /// </summary>
        /// <param name="driverContext">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="index">Index</param>
        public static void SelectByIndexWithSoftAssertion(DriverContext driverContext, ElementLocator locator, int index)
        {
            try
            {

                Select select = driverContext.Driver.GetElement<Select>(locator, 300);
                select.SelectByIndex(index);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify user able to select " + index + ":" + index + " is selected successfully");
                Logger.Info(index + " is selected successfully");
            }
            catch (Exception ex)
            {
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }


        }

        /// <summary>
        /// Select an element by value with timeout
        /// </summary>
        /// <param name="driverContext">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="value">Value</param>
        /// <param name="timeout">Timeout</param>
        public static void SelectByValueWithCustomTimeoutWithSoftAsertion(DriverContext driverContext, ElementLocator locator, string value, int timeout)
        {
            try
            {
                Select select = driverContext.Driver.GetElement<Select>(locator, 300);
                select.SelectByValue(value, timeout);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify user able to select " + value + ":" + value + " is selected successfully");
                Logger.Info(value + " is selected successfully");
            }
            catch (Exception ex)
            {
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// Select an element by text
        /// </summary>
        /// <param name="driverContext">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="textToBeSelected">Text</param>
        /// <param name="locatorName">Locator Name</param>
        public static void SelectByTextWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string textToBeSelected, string locatorName)
        {
            try
            {
                Select select = driverContext.Driver.GetElement<Select>(locator);
                if (select.IsSelectOptionAvailable(textToBeSelected) == false)
                {
                    throw new NoSuchElementException("Option with text " + textToBeSelected + " is not present");
                }


                select.SelectByText(textToBeSelected);

                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify user able to select " + textToBeSelected + " option in " + locatorName + "DropDown", textToBeSelected + " option is selected successfully");
                Logger.Info("Option with text " + textToBeSelected + " is selected in " + locatorName + "DropDown");
            }
            catch (Exception ex)
            {
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }


        /// <summary>
        /// Select an element by text with timeout
        /// </summary>
        /// <param name="driverContext">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="text">Text</param>
        /// <param name="timeout">Timeout</param>
        public static void SelectByTextWithSoftAssertion(DriverContext driverContext, ElementLocator locator, string text, int timeout)
        {
            try
            {
                Select select = driverContext.Driver.GetElement<Select>(locator);

                select.SelectByText(text, timeout);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify user able to select " + text + ":" + text + " is selected successfully");
                Logger.Info("Option with text " + text + " is selected ");
            }
            catch (Exception ex)
            {
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }
        }

        /// <summary>
        /// To Select the checked status of the Check Box
        /// </summary>
        /// <param name="driverContext">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        public static void SelectCheckBoxifUnselectedWithSoftAssertion(DriverContext driverContext, ElementLocator locator)
        {
            try
            {
                var chkBoxWebElement = driverContext.Driver.GetElement(locator);
                if (chkBoxWebElement.Selected)
                {
                }
                else
                {
                    chkBoxWebElement.Click();
                }
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify user able to check " + ":" + " successfully");
                Logger.Info("check box checked " + " " + " is selected ");
            }
            catch (Exception ex)
            {
                driverContext.VerifyMessages.Add(new ErrorDetail(null, DateTime.Now, ex));
                Logger.Error(CultureInfo.CurrentCulture, "<VERIFY FAILS>\n{0}\n</VERIFY FAILS>", ex);
                TakeScreenShotAndADDToReports(driverContext);
            }

        }
    }
}

