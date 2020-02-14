// <copyright file="WebDriverExtensions.cs" company="MCS">
// Copyright (c) MCS. All rights reserved.
// </copyright>
namespace MCS.Test.Automation.Common.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Data.SqlClient;
    using System.Threading;
    using System.Windows;
    using System.Windows.Forms;
    using MCS.Test.Automation.Common;
    using MCS.Test.Automation.Common.Helpers;
    using MCS.Test.Automation.Common.Types;
    using MCS.Test.Automation.Common.WebElements;

    // using AventStack.ExtentReports;
    // using AventStack.ExtentReports.Reporter;
    // using AventStack.ExtentReports.Reporter.Configuration;
    using NLog;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using RelevantCodes.ExtentReports;
    using Keys = OpenQA.Selenium.Keys;

    /// <summary>
    /// Extension methods for IWebDriver
    /// </summary>
    public static class WebDriverExtensions
    {
        private static readonly Logger Logger = LogManager.GetLogger("DRIVER");
        public static string Chrome = "chrome";
        public static string Firefox = "firefox";
        // private static readonly DriverContext TestStepLogger;

        // private static readonly DriverContext DriverContextTH = new DriverContext();

        public static string GetSingleValueFromDBCompareWithExpectedValue(this IWebDriver webDriver, string Query, string expectedValue, string message)
        {
            string result = string.Empty;
            try
            {
                result = SqlHelper.GetCountOnSQlQuery(Query);
                Assert.AreEqual(result, expectedValue);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify  " + message.Split('|')[0], message.Split('|')[0]);
                Logger.Info("Database Value are matching with expected result");
            }
            catch
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + message.Split('|')[0], message.Split('|')[1]);
                Logger.Info("Database Value are not matching with expected result");
                throw;
            }
            return result;
        }



        public static List<string> GetSingleColumnValuesAndCompareWithExpectedListFromDB(this IWebDriver webDriver, string Query, string columnName, List<string> expectedList, string message)
        {
            List<string> colList = new List<string>();
            try
            {
                colList = SqlHelper.GetSingleColumnValue(Query, columnName);
                Assert.AreEqual(colList, expectedList);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify  " + message + "values matching with Database values" , message + " values matching with Database values");
                Logger.Info("Database Value are matching with expected result");
            }
            catch
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + message.Split('|')[0], message.Split('|')[1]);
                Logger.Info("Database Value are not matching with expected result");
                throw;
            }
            return colList;
        }

        public static void CompareActualExpectedList(this IWebDriver webDriver, List<string> actualList,List<string> expectedList)
        {
            try
            {
                Assert.AreEqual(actualList, expectedList);
                //DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify  " + message + "values matching with Database values", message + " values matching with Database values");
                //Logger.Info("Database Value are matching with expected result");
            }
            catch
            {
                //DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + message.Split('|')[0], message.Split('|')[1]);
                //Logger.Info("Database Value are not matching with expected result");
                //throw;
            }
            
        }

        public static void CompareTwoDictionaryFromPageAndDB(this IWebDriver webDriver, Dictionary<string, string> pageobject, Dictionary<string, string> dbObject)
        {
            var errorInBothObjects = pageobject.Except(dbObject).Concat(dbObject.Except(pageobject));
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
                throw ex;
            }

        }
        public static void VerifyDictionaryContainsValuesFromPageInDB(this IWebDriver webDriver, Dictionary<string, string> pageobject, Dictionary<string, string> dbObject)
        {
            var errorInBothObjects = pageobject.Except(dbObject).Concat(pageobject.Except(dbObject));
            string verifyMessage = "To verify  entered values : " + string.Join(", ", pageobject.Select(kvp => kvp.Key + "_" + kvp.Value)) + " are  saved successfully in respective columns in database";
            string passMessage = "Entered values are  saved successfully in respective columns in database";
            string failMessage = "Error occured !! both expected and actual dictionary not matching , here are the Differences: " + string.Join(", ", errorInBothObjects.Select(kvp => kvp.Key + "-" + kvp.Value));
            try
            {
                Assert.Contains(dbObject, pageobject);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, verifyMessage, passMessage);
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, verifyMessage, failMessage);
                throw ex;
            }

        }

        /// <summary>
        /// Handler for simple use isAlertPresent.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <returns>JavaScriptAlert Handle</returns>
        public static bool IsAlertPresent(this IWebDriver webDriver)
        {
            bool presentFlag = false;

            try
            {
                // Check the presence of alert
                IAlert alert = webDriver.SwitchTo().Alert();

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
        /// Handler for simple use isAlertPresent.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="expectedAlert">Alert Message</param>
        /// <returns>JavaScriptAlert Handle</returns>
        public static bool IsAlertPresentwithExpectedText(this IWebDriver webDriver, string expectedAlert)
        {
            bool presentFlag = false;

            try
            {
                // Check the presence of alert
                IAlert alert = webDriver.SwitchTo().Alert();

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
                // Alert not present
                // throw;
                presentFlag = false;

                Logger.Error("Failed to click on Alert Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + expectedAlert + " Alert is clicked ", "An exception occurred while clicking on " + expectedAlert);
                throw;
            }

            return presentFlag;
        }

        /// <summary>
        /// This method is used to find the screen Resolution
        /// </summary>
        public static string GetScreenResolution(this IWebDriver webDriver)
        {
            string screenWidth = Screen.PrimaryScreen.Bounds.Width.ToString();
            string screenHeight = Screen.PrimaryScreen.Bounds.Height.ToString();

            return "width" + screenWidth + "height" + screenHeight;
        }

        /// <summary>
        /// Handler for simple use isAlertPresent.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        public static void AcceptAlert(this IWebDriver webDriver)
        {
            try
            {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.AlertIsPresent());
#pragma warning restore CS0618 // Type or member is obsolete
                IAlert alert = webDriver.SwitchTo().Alert();
                Logger.Info("accepted alert with text " + alert.Text);
                alert.Accept();
            }
            catch (TimeoutException)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Alert is displaying", "Time out Exception occurred while checking alert is displaying");
                throw;
            }
            catch (NoAlertPresentException)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Alert is displaying", "No Alert Present Exception occurred while checking alert is displaying");
                throw;
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Alert is accepted ", "An exception occurred while accepting Alert");
                throw;
            }
        }

        /// <summary>
        /// Navigates to Internet Explorer.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="url">The URL.</param>
        public static void NavigateToIE(this IWebDriver webDriver, Uri url)
        {
            try
            {
                webDriver.Navigate().GoToUrl(url);
                if (BaseConfiguration.Environment == "Local")
                {
                    //webDriver.SwitchTo().Alert().SetAuthenticationCredentials(BaseConfiguration.Domain + BaseConfiguration.Username, BaseConfiguration.Password);
                    //Thread.Sleep(500);
                    //webDriver.SwitchTo().Alert().Accept();
                    Thread.Sleep(500);
                    if (IsAlertPresent(webDriver))
                    {
                        webDriver.SwitchTo().Alert().Accept();
                    }
                }
                else
                {
                    if (IsAlertPresent(webDriver))
                    {
                        webDriver.SwitchTo().Alert().Accept();
                    }
                }

                ApproveCertificateForInternetExplorer(webDriver);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + url + " is launch Successfully", url + " is launch successfully");
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + url + " is launch Successfully", url + " is not launch successfully and some exception occured" + ex.ToString() + " The Url being navigated to is " + webDriver.Url.ToString());
                throw;
            }
        }

        /// <summary>
        /// Navigates to FireFox.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="url">The URL.</param>
        public static void NavigateToFirefox(this IWebDriver webDriver, Uri url)
        {
            try
            {
                webDriver.Navigate().GoToUrl(url);
                if (BaseConfiguration.Environment == "Local")
                {
                    //webDriver.SwitchTo().Alert().SetAuthenticationCredentials(BaseConfiguration.Username, BaseConfiguration.Password);
                    Thread.Sleep(500);
                    //webDriver.SwitchTo().Alert().Accept();
                    Thread.Sleep(500);
                    if (IsAlertPresent(webDriver))
                    {
                        webDriver.SwitchTo().Alert().Accept();
                        Thread.Sleep(500);
                    }
                }
                else
                {
                    if (IsAlertPresent(webDriver))
                    {
                        webDriver.SwitchTo().Alert().Accept();
                        Thread.Sleep(500);
                    }
                }

                ApproveCertificateForInternetExplorer(webDriver);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + url + " is launch Successfully", url + " is launch successfully");
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + url + " is launch Successfully", url + " is not launch successfully and some exception occured" + ex.ToString() + " The Url being navigated to is " + webDriver.Url.ToString());
                throw;
            }
        }

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="url">The URL.</param>
        public static void NavigateTo(this IWebDriver webDriver, Uri url)
        {
            try
            {
                webDriver.Navigate().GoToUrl(url);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + url + " is launch Successfully", url + " is launch successfully");
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + url + " is launch Successfully", url + " is not launch successfully and some exception occured" + ex.ToString() + " The Url being navigated to is " + webDriver.Url.ToString());
                throw;
            }
        }

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="url">The URL.</param>
        public static void NavigateToMOU(this IWebDriver webDriver, Uri url)
        {
            webDriver.Navigate().GoToUrl(url);

            // try
            // {
            //    webDriver.Navigate().GoToUrl(url);
            //    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + BaseConfiguration.GetMOUUrlValue + " is launch Successfully", BaseConfiguration.GetMOUUrlValue + " is launch successfully");
            // }
            // catch (Exception ex)
            // {
            //    DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + BaseConfiguration.GetMOUUrlValue + " is launch Successfully", BaseConfiguration.GetMOUUrlValue + " is not launch successfully and some exception occured" + ex.ToString() + " The Url being navigated to is " + webDriver.Url.ToString());
            //    throw;
            // }
        }

        /// <summary>
        /// Waits for all ajax actions to be completed.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        public static void WaitForAjax(this IWebDriver webDriver)
        {
            WaitForAjax(webDriver, BaseConfiguration.MediumTimeout);
        }

        /// <summary>
        /// Navigates Back to Previous Page
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        public static void NavigateBack(this IWebDriver webDriver)
        {
            webDriver.Navigate().Back();
        }

        /// <summary>
        /// Waits for all ajax actions to be completed.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="timeout">The timeout.</param>
        public static void WaitForAjax(this IWebDriver webDriver, double timeout)
        {
            try
            {
                new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout)).Until(
                    driver =>
                    {
                        var javaScriptExecutor = driver as IJavaScriptExecutor;
                        return javaScriptExecutor != null
                               && (bool)javaScriptExecutor.ExecuteScript("return jQuery.active == 0");
                    });
            }
            catch (InvalidOperationException)
            {
                Logger.Error(CultureInfo.CurrentCulture, "Invalid Operation Exception");
            }
        }

        /// <summary>
        /// This method is used to upload a file into application
        /// </summary>
        /// <param name="webDriver">this is webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="path">File Path</param>
        public static void UploadFile(this IWebDriver webDriver, ElementLocator locator, string path)
        {
            try
            {
                var webElementLocator = webDriver.GetElement(locator);
                HighlightingWebElement(webDriver, webElementLocator);
                webElementLocator.JavaScriptClick();
                Thread.Sleep(2000);
                SendKeys.SendWait(path);
                SendKeys.SendWait(@"{Enter}");

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="webDriver">This is webdriver</param>
        /// <param name="locator">this is locator</param>
        /// <param name="name">name of element</param>
        /// <param name="SearchText">search wit text </param>
        /// <returns></returns>
        public static bool IsElementPresentOrNotWithoutLog(this IWebDriver webDriver, ElementLocator locator, string name, string SearchText)
        {
            try
            {
                if (SearchText == string.Empty)
                {
                    SearchText = "No Text for this Element";
                }

                WaitForPageLoad(webDriver);
                var by = locator.ToBy();
                IWebElement element = webDriver.FindElement(by);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Info(name + " : element not found , Text Found :" + SearchText + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify " + name + " : element is not found", name + " : element is not successfully");
                return false;
            }
        }

        /// <summary>
        /// Check Is Element Present or not
        /// </summary>
        /// <param name="webDriver">Webdriver</param>
        /// <param name="locator">Locator</param>
        /// <param name="name">name</param>
        /// <param name="SearchText">Search Text</param>
        public static bool IsElementPresentOrNot(this IWebDriver webDriver, ElementLocator locator, string name, string SearchText)
        {
            try
            {
                if (SearchText == string.Empty)
                {
                    SearchText = "No Text for this Element";
                }

                WaitForPageLoad(webDriver);
                var by = locator.ToBy();
                IWebElement element = webDriver.FindElement(by);
                Logger.Info(name + " : element is found successfully, Text Found :" + SearchText);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify (" + name + " ) element is found", name + " element is found successfully");

                return true;
            }
            catch (Exception ex)
            {
                Logger.Info(name + " : element not found , Text Found :" + SearchText + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify " + name + " : element is not found", name + " : element is not successfully");
                return false;
            }
        }

        public static void CheckElementPresentOrNot(this IWebDriver webDriver, ElementLocator locator, string name, string SearchText)
        {
            try
            {
                if (SearchText == string.Empty)
                {
                    SearchText = "No Text for this Element";
                }

                WaitForPageLoad(webDriver);
                var by = locator.ToBy();
                IWebElement element = webDriver.FindElement(by);
                HighlightingWebElement(webDriver, element);
                Logger.Info(name + " : element is found successfully, Text Found :" + SearchText);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " element is visible ", name + " : element is found successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.MediumTimeout, "An exception occurred waiting for " + name + " to become clickable");
                throw;
            }
        }
        /// <summary>
        /// Wait for element to be displayed for specified time
        /// </summary>
        /// <example>Example code to wait for login Button: <code>
        /// this.Driver.IsElementPresent(this.loginButton, BaseConfiguration.ShortTimeout);
        /// </code></example>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="customTimeout">The timeout.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public static bool IsElementPresent(this IWebDriver webDriver, ElementLocator locator, double customTimeout)
        {
            try
            {
                webDriver.GetElement(locator, customTimeout, e => e.Displayed);

                // DriverContextTH.ExtentStepTest.Log(Status.Pass, "pass in openlandingpage of in common");
                return true;
            }
            catch (NoSuchElementException)
            {
                // DriverContextTH.ExtentStepTest.Log(Status.Fail, "failed in openlandingpage of in common");
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Waits the until element is found or loaded.
        /// </summary>
        /// <example>Sample code to check page title: <code>
        /// this.Driver.WaitUntilElementIsFound(AppearingInfo, BaseConfiguration.ShortTimeout);
        /// </code></example>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="timeout">The timeout.</param>
        public static void WaitUntilElementIsFound(this IWebDriver webDriver, ElementLocator locator, double timeout)
        {
            try
            {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
                wait.Until(driver => webDriver.GetElements(locator).Count >= 1);
            }
            catch (Exception e)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + e.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + locator + " is visible with in provided time " + timeout, "An exception occurred waiting for " + locator + " to become visible");
                throw;
            }
        }

        public static void IsElementClickableFromListofElementWithTextJavaScript(this IWebDriver webDriver, ElementLocator locator, string link)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
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
                throw;
            }
        }

        /// <summary>
        /// Determines whether [is page title] equals [the specified page title].
        /// </summary>
        /// <example>Sample code to check page title: <code>
        /// this.Driver.IsPageTitle(expectedPageTitle, BaseConfiguration.MediumTimeout);
        /// </code></example>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="pageTitle">The page title.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>
        /// Returns title of page
        /// </returns>
        public static bool IsPageTitle(this IWebDriver webDriver, string pageTitle, double timeout)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));

            try
            {
                wait.Until(d => d.Title.ToLower(CultureInfo.CurrentCulture) == pageTitle.ToLower(CultureInfo.CurrentCulture));
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Error(CultureInfo.CurrentCulture, "Actual page title is {0};", webDriver.Title);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Waits the until element is no longer found.
        /// </summary>
        /// <example>Sample code to check page title: <code>
        /// this.Driver.WaitUntilElementIsNoLongerFound(dissapearingInfo, BaseConfiguration.ShortTimeout);
        /// </code></example>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="name">The timeout.</param>
        public static void WaitUntilElementIsNoLongerFound(this IWebDriver webDriver, ElementLocator locator, double timeout, string name)
        {
            try
            {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));
                wait.Until(driver => webDriver.GetElements(locator).Count == 0);
            }
            catch (Exception e)
            {
                Logger.Error("An exception occured while waiting for the element " + name + " to become disappear with locator " + e.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is not visible with in provided time " + timeout + " sec", "An exception occurred waiting for " + name + " to become disappear with");
                throw;
            }
        }

        /// <summary>
        /// Switch to existing window using url.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="url">The url.</param>
        /// <param name="timeout">The timeout.</param>
        public static void SwitchToWindowUsingUrl(this IWebDriver webDriver, Uri url, double timeout)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
            wait.Until(
                driver =>
                {
                    foreach (var handle in webDriver.WindowHandles)
                    {
                        webDriver.SwitchTo().Window(handle);
                        if (driver.Url.Equals(url.ToString()))
                        {
                            return true;
                        }
                    }

                    return false;
                });
        }

        /// <summary>
        /// The scroll into middle.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">The locator.</param>
        public static void ScrollIntoMiddle(this IWebDriver webDriver, ElementLocator locator)
        {
            var js = (IJavaScriptExecutor)webDriver;
            var element = webDriver.ToDriver().GetElement(locator);

            var height = webDriver.Manage().Window.Size.Height;
            var hoverItem = (ILocatable)webDriver;
            var locationY = hoverItem.LocationOnScreenOnceScrolledIntoView.Y;
            //js.ExecuteScript(string.Format(CultureInfo.InvariantCulture, "javascript:window.scrollBy(0,{0})", locationY - (height/2)));
            // js.ExecuteScript(string.Format(CultureInfo.InvariantCulture, "javascript:window.scrollTo(0, document.body.scrollHeight);"));
            js.ExecuteScript(string.Format(CultureInfo.InvariantCulture, "javascript:window.scrollBy(0,{0})", locationY - (height / 2)));
        }

        /// <summary>
        /// Selenium Actions.
        /// </summary>
        /// <example>Simple use of Actions: <code>
        /// this.Driver.Actions().SendKeys(Keys.Return).Perform();
        /// </code></example>
        /// <param name="webDriver">The web driver.</param>
        /// <returns>Return new Action handle</returns>
        public static Actions Actions(this IWebDriver webDriver)
        {
            return new Actions(webDriver);
        }

        /// <summary>Checks that page source contains text for specified time.</summary>
        /// <param name="webDriver">The web webDriver.</param>
        /// <param name="text">The text.</param>
        /// <param name="timeoutInSeconds">The timeout in seconds.</param>
        /// <param name="isCaseSensitive">True if this object is case sensitive.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool PageSourceContainsCase(this IWebDriver webDriver, string text, double timeoutInSeconds, bool isCaseSensitive)
        {
            Func<IWebDriver, bool> condition;

            if (isCaseSensitive)
            {
                condition = drv => drv.PageSource.Contains(text);
            }
            else
            {
                condition = drv => drv.PageSource.ToUpperInvariant().Contains(text.ToUpperInvariant());
            }

            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(condition);
            }

            return condition.Invoke(webDriver);
        }

        /// <summary>Easy use for java scripts.</summary>
        /// <example>Sample use of java scripts: <code>
        /// this.Driver.JavaScripts().ExecuteScript("return document.getElementById("demo").innerHTML");
        /// </code></example>
        /// <param name="webDriver">The webDriver to act on.</param>
        /// <returns>An IJavaScriptExecutor Handle.</returns>
        public static IJavaScriptExecutor JavaScripts(this IWebDriver webDriver)
        {
            return (IJavaScriptExecutor)webDriver;
        }

        /// <summary>
        /// Waits for all angular actions to be completed.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        public static void WaitForAngular(this IWebDriver webDriver)
        {
            WaitForAngular(webDriver, BaseConfiguration.MediumTimeout);
        }

        /// <summary>
        /// Waits for all angular actions to be completed.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="timeout">The timeout.</param>
        public static void WaitForAngular(this IWebDriver webDriver, double timeout)
        {
            try
            {
                new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout)).Until(
                    driver =>
                    {
                        var javaScriptExecutor = driver as IJavaScriptExecutor;
                        return javaScriptExecutor != null
                               &&
                               (bool)javaScriptExecutor.ExecuteScript(
                                   "return window.angular != undefined && window.angular.element(document.body).injector().get('$http').pendingRequests.length == 0");
                    });
            }
            catch (InvalidOperationException)
            {
                Logger.Info("Wait for angular invalid operation exception.");
            }
        }

        /// <summary>
        /// Checkbox checked
        /// </summary>
        /// <param name="webDriver">Webdriver</param>
        /// <param name="locator">locator</param>
        public static void IsCheckBoxCheckedUsingJavaScript(this IWebDriver webDriver, ElementLocator locator)
        {
            try
            {
                var webElementLocator = webDriver.GetElement(locator);
                var javaScriptExecutor = webDriver as IJavaScriptExecutor;
                javaScriptExecutor.ExecuteScript("arguments[0].checked=false;", webElementLocator);
                Logger.Info("Wait for angular invalid operation exception.");
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Wait for the page load to complete.
        /// </summary>
        /// <param name="webDriver">The WebDriver.</param>
        public static void WaitForPageLoad(this IWebDriver webDriver)
        {
            new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.LongTimeout)).Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        /// <summary>
        /// Enable synchronization with angular.
        /// </summary>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="webElementLocator">WebElementLocator</param>
        public static void HighlightingWebElement(this IWebDriver webDriver, IWebElement webElementLocator)
        {
            try
            {
                var javaScriptExecutor = webDriver as IJavaScriptExecutor;
                javaScriptExecutor.ExecuteScript("arguments[0].style.border='3px solid red'", webElementLocator);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Scrolling To WebElement.
        /// </summary>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="webElementLocator">WebElementLocator</param>
        public static void ScrollToWebElement(this IWebDriver webDriver, IWebElement webElementLocator)
        {
            try
            {
                var javaScriptExecutor = webDriver as IJavaScriptExecutor;
                javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView();", webElementLocator);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Scrolling To WebElement.
        /// </summary>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="locator">WebElementLocator</param>
        public static void ScrollToWebElement(this IWebDriver webDriver, ElementLocator locator)
        {
            try
            {
                var webElementLocator = webDriver.GetElement(locator);
                var javaScriptExecutor = webDriver as IJavaScriptExecutor;
                javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView();", webElementLocator);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// JavaScript Execution
        /// </summary>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="locator">Enable or disable synchronization.</param>
        /// <param name="name">Name of the Element</param>
        /// <param name="textToType">Text to Enter</param>
        public static void JavaScriptExecution(this IWebDriver webDriver, ElementLocator locator, string name, string textToType)
        {
            var webElementLocator = webDriver.GetElement(locator);
            var js = webDriver as IJavaScriptExecutor;
            js.ExecuteScript("window.scrollBy(0,-200)");
            webDriver.IsElementClickable(locator, name);
            js.ExecuteScript("arguments[0].innerHTML = '" + textToType + "'", webElementLocator);
            webDriver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// IsElementTextDisplayed
        /// </summary>
        /// <param name="webDriver">this is webdriver</param>
        /// <param name="locator">element locator</param>
        /// <param name="name">name of element</param>
        /// <returns>string..</returns>
        public static string IsElementTextDisplayed(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElement(locator);
                HighlightingWebElement(webDriver, webElementLocator);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + "", name + " " + webElementLocator.Text + " is visible successfully");
                Logger.Info(name + " is displayed/Enabled successfully");
                return webElementLocator.Text;
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                throw;
            }
        }

        public static int GetCountOfCheckedUnCheckedCheckBox(this IWebDriver webDriver, ElementLocator locator, string name, string ischecked)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                return collection.Count();
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify count of the " + ischecked + " checkbox box", "An exception occurred waiting for " + name + " to become visible");
                throw;
            }
        }

        /// <summary>
        /// Checking Alphabets from list of strings
        /// </summary>
        /// <param name="webDriver">this is webdriver</param>
        /// <param name="locator">element locator</param>
        /// <param name="link">name of element</param>
        /// <param name="ascordesc"></param>
        public static void AreElementsSortedInAlphabeticalOrder(this IWebDriver webDriver, ElementLocator locator, string link, string ascordesc = "")
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var x = collection.Select(item => item.Text.Replace(System.Environment.NewLine, ""));
                var sorted = new List<string>();
                if (ascordesc == "desc")
                {
                    sorted.AddRange(x.OrderByDescending(o => o));
                    Assert.IsTrue(x.SequenceEqual(sorted));
                    DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + link + " is visible in descending order", link + " is visible in discending order successfully");
                    Logger.Info(link + " are visible/descending successfully");
                }
                else
                {
                    sorted.AddRange(x.OrderBy(o => o));
                    Assert.IsTrue(x.SequenceEqual(sorted));
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + link + " is visible in Ascending order", link + " is visible in Ascending ordersuccessfully");
                    Logger.Info(link + " are visible/ascending successfully");
                }          
            }
            catch (Exception ex)
            {

                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + link + " is visible with in provided time " + BaseConfiguration.MediumTimeout, "An exception occurred waiting for " + link + " to become clickable");
                throw;
            }
        }

        public static void AreElementsSortedInorderForDateTime(this IWebDriver webDriver, ElementLocator locator, string link, string ascordesc = "", string Isdatetime = "", string replacetext = "")
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
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
                throw;
            }
        }


        /// <summary>
        /// IsGrayedOutLinksDisplayed
        /// </summary>
        /// <param name="webDriver">This is webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="name">Name of the Element</param>
        /// <param name="linksName">link name</param>
        public static void IsGrayedOutLinksClickable(this IWebDriver webDriver, ElementLocator locator, string name, string linksName)
        {
            try
            {
                WaitForPageLoad(webDriver);
                IList<IWebElement> webElementLocator = webDriver.GetElements(locator);
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
                throw;
            }
        }

        /// <summary>
        /// IsPopUpTextDisplayed
        /// </summary>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="locator">Enable or disable synchronization.</param>
        /// <param name="name">Name of the Element</param>
        /// <returns>bool.</returns>
        public static string IsPopUpTextDisplayed(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElement(locator);
                if (webElementLocator.Displayed)
                {
                    HighlightingWebElement(webDriver, webElementLocator);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is visible", name + " is visible successfully");
                    Logger.Info(name + " is displayed/Enabled successfully");
                    return webElementLocator.Text;
                }
                else
                {
                    throw new ElementNotVisibleException(name + " is not displayed/Enabled but present in the Dom");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.MediumTimeout, "An exception occurred waiting for " + name + " to become clickable");
                throw;
            }
        }
        /// <summary>
        /// This method is used to click on selecte  element from list 
        /// </summary>
        /// <param name="webDriver">This is webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="link">name of the link</param>
        public static void IsElementClickableFromListofElement(this IWebDriver webDriver, ElementLocator locator, string link)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
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
                throw;
            }
        }

        /// <summary>
        /// This method is used to click on selecte  element from list 
        /// </summary>
        /// <param name="webDriver">This is webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="link">name of the link</param>
        public static void IsElementClickableFromListofElementWithText(this IWebDriver webDriver, ElementLocator locator, string link)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(collection));
#pragma warning restore CS0618 // Type or member is obsolete
                foreach (var item in collection)
                {
                    if (item.Text.Contains(link))
                    {
                        item.Click();
                        WaitForPageLoad(webDriver);
                        break;
                    }
                }
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + link + " is Clicked", link + " is Clicked successfully");
                Logger.Info(link + " are enable/Clickable successfully");

            }
            catch (Exception ex)
            {

                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + link + " is Clicked", "An exception occurred while clicking " + link);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="locator"></param>
        /// <param name="link"></param>
        /// <param name="name" ></param>
        public static void IsElementClickableFromListofValuesWithText(this IWebDriver webDriver, ElementLocator locator, string link, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(collection));
#pragma warning restore CS0618 // Type or member is obsolete
                foreach (var item in collection)
                {
                    if (item.Text.Contains(link))
                    {
                        item.Click();
                        WaitForPageLoad(webDriver);
                        break;
                    }
                }
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " +name+ " - " + link + " is Clicked", name+ " - "+ link + " is Clicked successfully");
                Logger.Info(link + " are enable/Clickable successfully");

            }
            catch (Exception ex)
            {

                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + link + " is Clicked", "An exception occurred while clicking " + link);
                throw;
            }
        }

        /// <summary>
        /// This method is used to click on selecte  element from list 
        /// </summary>
        /// <param name="webDriver">This is webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="link">name of the link</param>
        /// <param name="name">name of params</param>
        public static void IsElementClickableFromListofElementWithTextByIndex(this IWebDriver webDriver, ElementLocator locator, string name, string link)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(collection));
#pragma warning restore CS0618 // Type or member is obsolete
                for (int i = 0; i < collection.Count; i++)
                {
                    IWebElement ele = webElementLocator[i];

                    if (ele.Text.Contains(link))
                    {
                        ScrollToWebElement(webDriver, ele);
                        ele.JavaScriptClick();
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
                throw;
            }
        }

        /// <summary>
        /// This method is used to verify the text value from list of elements 
        /// </summary>
        /// <param name="webDriver">This is webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="link">name of the link</param>
        /// <param name="name">name of element</param>
        public static bool IsElementTextVisibleFromListOfValues(this IWebDriver webDriver, ElementLocator locator, string link, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(collection));
#pragma warning restore CS0618 // Type or member is obsolete
                foreach (IWebElement item in collection)
                {
                    if (item.Text.Equals(link))
                    {
                        ScrollToWebElement(webDriver, item);
                        HighlightingWebElement(webDriver, item);
                        Assert.AreEqual(item.Text, link);
                        DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " +name+ " "+ link + " is visible", name+ " "+ link + " is visible successfully");
                        Logger.Info(link + " are enable/visible successfully");
                        return true;
                    }
                }
              
            }
            catch (Exception ex)
            {

                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + link + " is visible with in provided time " + BaseConfiguration.MediumTimeout, "An exception occurred waiting for " + link + " to become visible");
                return false;
            }
            return true;
        }

        /// <summary>
        /// User Menu Items
        /// </summary>
        /// <param name="webDriver">Webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="link">Element type</param>
        public static void IsUserMenuItemClickable(this IWebDriver webDriver, ElementLocator locator, string link)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
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
                throw;
            }
        }

        /// <summary>
        /// Check Account Number existing in popup of grayed out link
        /// </summary>
        /// <param name="webDriver">this is webdiver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="link">Name oif Element</param>
        /// <returns>list...</returns>
        public static IList<IWebElement> IsGrayedoutlinkClickable(this IWebDriver webDriver, ElementLocator locator, string link)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
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
                return webElementLocator;
            }
            catch (Exception ex)
            {

                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + link + " is visible with in provided time " + BaseConfiguration.MediumTimeout, "An exception occurred waiting for " + link + " to become clickable");
                throw;
            }
        }

        /// <summary>
        /// Click on element from the list of records
        /// </summary>
        /// <param name="webDriver">this is webdriver</param>
        /// <param name="locator">element locator</param>
        /// <param name="name">name of an element</param>
        /// <param name="link">name of link</param>
        public static void ClickOnMembershipTypeFromListofRecords(this IWebDriver webDriver, ElementLocator locator, string name, string link)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
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
                throw;
            }
        }

        /// <summary>
        /// To verify page refresh
        /// </summary>
        /// <param name="webDriver">This is webdriver</param>
        /// <param name="locator">Element Locator</param>
        public static void PageRefresh(this IWebDriver webDriver, ElementLocator locator)
        {

            WaitForPageLoad(webDriver);
            var webElementLocator = webDriver.GetElement(locator);
            webElementLocator.SendKeys(Keys.F5);
        }
        /// <summary>
        /// To Verify element from list oif elements
        /// </summary>
        /// <param name="webDriver">This is webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="name">name of element</param>
        /// <returns>bool..</returns>
        public static bool IsElementVisibleFromListOfElement(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            bool elementVisible = false;
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(collection));
#pragma warning restore CS0618 // Type or member is obsolete
                foreach (var item in collection)
                {
                    if (item.Text.Equals(name))
                    {
                        HighlightingWebElement(webDriver, item);
                        ScrollToWebElement(webDriver, item);
                        Logger.Info(item.Text + " element visible successfully");
                        DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is visible", name + " is visible successfully");
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
                throw;
            }
            return elementVisible;
        }

        /// <summary>
        /// Move Slider
        /// </summary>
        /// <param name="webDriver">Webdriver.</param>
        /// <param name="locator">name of the Locators.</param>
        /// <param name="name">name of Element.</param>
        public static void SliderSecondSetAttribute(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
                Random rnd = new Random();
                string randomNo = Convert.ToString(rnd.Next(2, 8));
                var by = locator.ToBy();
                var a = webDriver.FindElement(by);
                js.ExecuteScript(string.Format("arguments[0].setAttribute('style', 'position: absolute; left:{0}%;')", randomNo), a);
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.LongTimeout, "An exception occurred waiting for " + name + " to become visible");
                throw;
            }

        }

        /// <summary>
        /// Move Slider
        /// </summary>
        /// <param name="webDriver">Webdriver.</param>
        /// <param name="locator">name of the Locators.</param>
        /// <param name="name">name of Element.</param>
        public static void SliderSetAttribute(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
                Random rnd = new Random();
                string randomNo = Convert.ToString(rnd.Next(5, 80));
                var by = locator.ToBy();
                var a = webDriver.FindElement(by);
                js.ExecuteScript(string.Format("arguments[0].setAttribute('style', 'position: absolute; left:{0}%;')", randomNo), a);
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.LongTimeout, "An exception occurred waiting for " + name + " to become visible");
                throw;
            }

        }

        /// <summary>
        /// Navigate TO Url
        /// </summary>
        /// <param name="webDriver"></param>
        /// <returns></returns>
        public static string GetCurrentUrl(this IWebDriver webDriver)
        {
            var url = webDriver.Url;
            return url;
        }

        public static void NavigateToUrl(this IWebDriver webDriver, String url)
        {
            webDriver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Enable synchronization with angular.
        /// </summary>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="locator">Enable or disable synchronization.</param>
        /// <param name="name">Name of the Element</param>
        /// <returns>bool.</returns>
        public static bool IsElementVisible(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElement(locator);
                if (webElementLocator.Displayed)
                {
                    HighlightingWebElement(webDriver, webElementLocator);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is visible", name + " is visible successfully");
                    Logger.Info(name + " is displayed/Enabled successfully");
                    return webElementLocator.Displayed;
                }
                else
                {
                    throw new ElementNotVisibleException(name + " is not displayed/Enabled but present in the Dom");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for field '" + name + "' to become visible");
                throw;
            }
        }


        public static void DragDropFromLocatorToLocator(this IWebDriver webDriver, ElementLocator dragFromLocator, ElementLocator dragToLocator)
        {
            try
            {
                WaitForPageLoad(webDriver);
                Actions action = new Actions(webDriver);
                action.ClickAndHold(webDriver.GetElement(dragFromLocator)).MoveByOffset(-10, 0).MoveToElement(webDriver.GetElement(dragToLocator));
                Thread.Sleep(900);
                action.Release().Perform();
                Logger.Info("Dragging From {" + dragFromLocator + "} drop to {" + dragToLocator + "} was successfull");         
            }
            catch (Exception ex)
            {
                Logger.Error("Dragging From { " + dragFromLocator + "}   drop to { " + dragToLocator + "} was not successfull , An exception occured while waiting for the element to become visible " + ex.ToString());
                throw;
            }
        }


        public static bool IsDragDropFromLocatorToLocator(this IWebDriver webDriver, ElementLocator dragFromLocator, ElementLocator dragToLocator, string name)
        {
            bool IsDraggableselection = false;
            try
            {
                WaitForPageLoad(webDriver);
                Actions action = new Actions(webDriver);
                action.ClickAndHold(webDriver.GetElement(dragFromLocator)).MoveByOffset(-10, 0).MoveToElement(webDriver.GetElement(dragToLocator));
                Thread.Sleep(900);
                action.Release().Perform();
                Logger.Info(name + " is a selection box");
                IsDraggableselection = true;
                return IsDraggableselection;

            }
            catch (Exception ex)
            {
                Logger.Error("Unable to Drag & Drop , An exception occured while waiting for the element to become visible " + ex.ToString());
                throw;
            }
        }


        /// <summary>
        /// This is for waitforelementclickable
        /// </summary>
        /// <param name="webDriver">this is webdiver</param>
        /// <param name="element">Iweb element</param>
        /// <param name="name">name of element</param>
        public static void WaitForClickable(this IWebDriver webDriver, IWebElement element, string name)
        {
            try
            {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.ElementToBeClickable(element));
#pragma warning restore CS0618 // Type or member is obsolete
                /*
                 * ExtentTestManager.getTest().log(LogStatus.PASS, "To verify " + webElementName
                 * + " is clickable", webElementName + " to become clickable.");
                 */
            }
            catch (TimeoutException)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clickable", "Timed out waiting for " + name + " to become clickable.");
                throw;
            }
            catch (NoSuchElementException)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clickable", "Unable to locate element " + name + " to become clickable.");
                throw;
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clickable", "An exception occurred waiting for " + name + " to become clickable.");
                throw;
            }
        }

        /// <summary>
        /// This Method is used to get the actual text and match with expected text
        /// </summary>
        /// <param name="webDriver">this is webdiver</param>
        /// <param name="locator">Locator of Webelement</param>
        /// <param name="expectedText">Expected from Text from source File</param>
        /// <param name="actualText">Actual text </param>
        public static string IsExpectedTextMatchWithActualTextFromListOFValues(this IWebDriver webDriver, ElementLocator locator, string expectedText, string actualText = "")
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                foreach (var item in webElementLocator)
                {
                    actualText = item.Text.Trim();
                    if (item.Text.Trim().Equals(expectedText))
                    {
                        return item.Text;
                        //ScrollToWebElement(webDriver, locator);
                        //HighlightingWebElement(webDriver, item);
                        //DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Expected Value " + expectedText + " is matching with Actual text ", "The expected Value is " + expectedText + " and  actual value is " + actualText + " matching successfully");
                        //Logger.Info("Expected text " + expectedText + " and Actual text is " + actualText);
                    }
                }
             
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to verify text on " + expectedText + "with Actual Text " + actualText + " Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify text on " + expectedText + "with Actual Text " + actualText, "An exception occurred while finding text on " + expectedText);
                throw;
            }
            return actualText;
        }

        /// <summary>
        /// This Method is used to get the actual text and match with expected text
        /// </summary>
        /// <param name="webDriver">this is webdiver</param>
        /// <param name="locator">Locator of Webelement</param>
        /// <param name="expectedText">Expected from Text from source File</param>
        /// <param name="actualText">Actual text </param>
        public static void IsExpectedTextMatchWithActualText(this IWebDriver webDriver, ElementLocator locator, string expectedText, string actualText = "")
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElement(locator);
                actualText = webElementLocator.Text.Trim();
                Assert.AreEqual(actualText, expectedText);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Expected Value (" + expectedText + ") is matching with Actual text ", "The expected Value is (" + expectedText + ") and  actual value is (" + actualText + ") matching successfully");
                Logger.Info("Expected text " + expectedText + " and Actual text is " + actualText);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to verify text on (" + expectedText + ") with Actual Text (" + actualText + ") Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify text on (" + expectedText + ") with Actual Text (" + actualText + ")", "An exception occurred while finding text on (" + expectedText + ")");
                throw;
            }
        }

        /// <summary>
        /// This Method is used to get the actual text and match with expected text
        /// </summary>
        /// <param name="webDriver">this is webdiver</param>
        /// <param name="locator">Locator of Webelement</param>
        /// <param name="expectedText">Expected from Text from source File To Lower</param>
        /// <param name="actualText">Actual text </param>
        public static void IsExpectedTextMatchWithActualTextToLower(this IWebDriver webDriver, ElementLocator locator, string expectedText, string actualText)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElement(locator);

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
                throw;
            }
        }

        public static void IsElementVisibleHiddenMode(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var by = locator.ToBy();
                var webElement = webDriver.FindElement(by);
                HighlightingWebElement(webDriver, webElement);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is visible ", name + " is visible successfully");
                Logger.Info(name + " is visible successfully");

            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element is visible" + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is not visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become present in hidden mode");
                throw;
            }
        }

        public static void IsElementClickableinHiddenMode(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var by = locator.ToBy();
                var webElement = webDriver.FindElement(by);
                webElement.Click();
                Thread.Sleep(1000);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is clicked ", name + " is clicked successfully");
                Logger.Info(name + " is clicked successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                throw;
            }
        }

        public static void IsElementClickableWithoutLog(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElement(locator);
                WaitForClickable(webDriver, webElementLocator, name);
                if (webElementLocator.Displayed && webElementLocator.Enabled)
                {
                    webElementLocator.Click();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                throw;
            }
        }

        /// <summary>
        /// Enable synchronization with angular.
        /// </summary>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="locator">Enable or disable synchronization.</param>
        /// <param name="name">Name of the Element</param>
        public static void IsElementClickable(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElement(locator);
                WaitForClickable(webDriver, webElementLocator, name);
                if (webElementLocator.Displayed && webElementLocator.Enabled)
                {
                    webElementLocator.Click();
                    Thread.Sleep(1000);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is clicked ", name + " is clicked successfully");
                    Logger.Info(name + " is clicked successfully");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                throw;
            }
        }

        /// <summary>
        /// Enable synchronization with angular.
        /// </summary>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="locator">Enable or disable synchronization.</param>
        /// <param name="name">Name of the Element</param>
        public static void IsElementClickableWithSoftAssertion(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElement(locator);
                WaitForClickable(webDriver, webElementLocator, name);
                if (webElementLocator.Displayed && webElementLocator.Enabled)
                {
                    webElementLocator.Click();
                    Thread.Sleep(1000);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is clicked ", name + " is clicked successfully");
                    Logger.Info(name + " is clicked successfully");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);

            }
        }

        ///// <summary>
        ///// Enable synchronization with angular.
        ///// </summary>
        ///// <param name="webDriver">The WebDriver.</param>
        ///// <param name="webElementLocator">Enable or disable synchronization4.</param>
        // public static void HighlightingWebElement(this IWebDriver webDriver, IWebElement webElementLocator)
        // {
        //    try
        //    {
        //        var javaScriptExecutor = webDriver as IJavaScriptExecutor;
        //        javaScriptExecutor.ExecuteScript("arguments[0].style.border='3px solid red'", webElementLocator);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        // }

        ///// <summary>
        ///// Enable synchronization with angular.
        ///// </summary>
        ///// <param name="webDriver">The WebDriver.</param>
        ///// <param name="locator">Enable or disable synchronization.</param>
        ///// <returns>bool.</returns>
        // public static bool IsElementVisible(this IWebDriver webDriver, ElementLocator locator)
        // {
        //    try
        //    {
        //        var webElementLocator = webDriver.GetElement(locator);
        //        if (webElementLocator.Displayed)
        //        {
        //            HighlightingWebElement(webDriver, webElementLocator);
        //        }

        // Logger.Info(locator + " is displayed/Enabled successfully");
        //        return webElementLocator.Enabled;
        //    }
        //    catch (TimeoutException)
        //    {
        //        throw;
        //    }
        //    catch (NoSuchElementException)
        //    {
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        // }

        /// <summary>
        /// Enable synchronization with angular.
        /// </summary>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="enable">Enable or disable synchronization.</param>
        public static void SynchronizeWithAngular(this IWebDriver webDriver, bool enable)
        {
            DriversCustomSettings.SetAngularSynchronizationForDriver(webDriver, enable);
        }

        /// <summary>
        /// Javascript drag and drop function.
        /// </summary>
        /// <param name="webDriver">The WebDriver</param>
        /// <param name="source">Source element</param>
        /// <param name="destination">Destination element</param>
        public static void DragAndDropJs(this IWebDriver webDriver, IWebElement source, IWebElement destination)
        {
            var script =
                "function createEvent(typeOfEvent) { " +
                   "var event = document.createEvent(\"CustomEvent\"); " +
                   "event.initCustomEvent(typeOfEvent, true, true, null); " +
                   "event.dataTransfer = { " +
                            "data: { }, " +
                        "setData: function(key, value) { " +
                                "this.data[key] = value; " +
                            "}, " +
                        "getData: function(key) { " +
                               "return this.data[key]; " +
                            "} " +
                        "}; " +
                    "return event; " +
                        "} " +
                        "function dispatchEvent(element, event, transferData) { " +
                            "if (transferData !== undefined)" +
                            "{" +
                        "event.dataTransfer = transferData;" +
                        "}" +
                    "if (element.dispatchEvent) {" +
                        "element.dispatchEvent(event);" +
                        "} else if (element.fireEvent) {" +
                        "element.fireEvent(\"on\" + event.type, event);" +
                        "}" +
                    "}" +
                    "function simulateHTML5DragAndDrop(element, target)" +
                    "{" +
                        "var dragStartEvent = createEvent('dragstart');" +
                        "dispatchEvent(element, dragStartEvent);" +
                        "var dropEvent = createEvent('drop');" +
                        "dispatchEvent(target, dropEvent, dragStartEvent.dataTransfer);" +
                        "var dragEndEvent = createEvent('dragend');" +
                        "dispatchEvent(element, dragEndEvent, dropEvent.dataTransfer);" +
                    "} simulateHTML5DragAndDrop(arguments[0], arguments[1])";

            ((IJavaScriptExecutor)webDriver).ExecuteScript(script, source, destination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webDriver">This is Web Driver</param>
        /// <param name="locator">This is Locator</param>
        /// <param name="num">number </param>
        /// <param name="name">Name of Element</param>  
        /// <returns></returns>
        public static int IsElementTextLengthMatchWithActualLength(this IWebDriver webDriver, ElementLocator locator, string name, int num)
        {
            int finalvalue=0;
            try
            {
                var webElement = webDriver.GetElement(locator);
                string count = webElement.GetAttribute("maxlength").ToString();
                finalvalue = int.Parse(count);

                return finalvalue;
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to find the length of " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is matching with Actual ", "An exception occurred while verify the " + name +" And actual Length is "+ finalvalue +" instead of "+ num);
                throw;
            }
        }

        /// <summary>
        /// To get the Selected Text from the DropDown
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <returns>Text in the DropDown</returns>
        public static string SelectedText(this IWebDriver webDriver, ElementLocator locator)
        {
            var element = webDriver.GetElement(locator);
            var select = new SelectElement(element);
            return select.SelectedOption.Text;
        }

#pragma warning disable SA1614 // Element parameter documentation must have text
        /// <summary>
        /// To check if option with the text is present
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="optionText"></param>
        /// <returns>True or false about the text presence</returns>
        public static bool IsOptionWithTextPresent(this IWebDriver webDriver, ElementLocator locator, string optionText)
#pragma warning restore SA1614 // Element parameter documentation must have text
        {
            var isPresent = false;
            var element = webDriver.GetElement(locator);
            var select = new SelectElement(element);
            foreach (var option in select.Options)
            {
                if (optionText.Equals(option.Text))
                {
                    isPresent = true;
                }
            }

            return isPresent;
        }

        /// <summary>
        /// Select an element by index with Timeout
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="index">Index</param>
        /// <param name="timeout">Time Out</param>
        public static void SelectByIndexWithCustomTimeout(this IWebDriver webDriver, ElementLocator locator, int index, int timeout)
        {
            Select select = webDriver.GetElement<Select>(locator, timeout);
            select.SelectByIndex(index, timeout);
        }

        /// <summary>
        /// Select an element by index
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="index">Index</param>
        public static void SelectByIndex(this IWebDriver webDriver, ElementLocator locator, int index)
        {
            Select select = webDriver.GetElement<Select>(locator, 300);
            select.SelectByIndex(index);
        }

        /// <summary>
        /// Select an element by value
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="value">Value</param>
        public static void SelectByValue(this IWebDriver webDriver, ElementLocator locator, string value)
        {
            Select select = webDriver.GetElement<Select>(locator, 300);
            select.SelectByValue(value);
        }

        /// <summary>
        /// Select an element by value with timeout
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="value">Value</param>
        /// <param name="timeout">Timeout</param>
        public static void SelectByValueWithCustomTimeout(this IWebDriver webDriver, ElementLocator locator, string value, int timeout)
        {
            Select select = webDriver.GetElement<Select>(locator, 300);
            select.SelectByValue(value, timeout);
        }

        /// <summary>
        /// Select an element by text
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="textToBeSelected">Text</param>
        /// <param name="locatorName">Locator Name</param>
        public static void SelectByText(this IWebDriver webDriver, ElementLocator locator, string textToBeSelected, string locatorName)
        {
            Select select = webDriver.GetElement<Select>(locator);

            if (select.IsSelectOptionAvailable(textToBeSelected) == false)
            {
                throw new NoSuchElementException("Option with text " + textToBeSelected + " is not present");
            }

            select.SelectByText(textToBeSelected);

            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify user able to select " + textToBeSelected + " option in " + locatorName + "DropDown", textToBeSelected + " option is selected successfully");
            Logger.Info("Option with text " + textToBeSelected + " is selected in " + locatorName + "DropDown");
        }

        /// <summary>
        /// Select an element by text with timeout
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="text">Text</param>
        /// <param name="timeout">Timeout</param>
        public static void SelectByText(this IWebDriver webDriver, ElementLocator locator, string text, int timeout)
        {
            Select select = webDriver.GetElement<Select>(locator);

            select.SelectByText(text, timeout);
        }

        /// <summary>
        /// Returns the Selected Option Text
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <returns>Selected Option Text</returns>
        public static string SelectedOption(this IWebDriver webDriver, ElementLocator locator)
        {
            Select select = webDriver.GetElement<Select>(locator);

            return select.SelectElement().SelectedOption.Text;
        }

        /// <summary>
        /// This Method is used to hit the Item from Populated items from Search box
        /// </summary>
        /// <param name="webDriver">This is Web Driver</param>
        /// <param name="locator">This is Locator</param>
        public static void EnterArrowDown(this IWebDriver webDriver, ElementLocator locator)
        {
            var txtBox = webDriver.GetElement(locator);
            txtBox.SendKeys(Keys.ArrowDown);
            txtBox.SendKeys(Keys.Enter);
        }

        /// <summary>
        /// Enter Text into a Text Box
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="text">Text to be Entered into the text Box</param>
        /// <param name="name">Element name.</param>
        public static void EnterText(this IWebDriver webDriver, ElementLocator locator, string text, string name)
        {
            try
            {
                var txtBox = webDriver.GetElement(locator);
                if (txtBox.Enabled)
                {
                    Logger.Info(name + " is viewed successfully");
                    HighlightingWebElement(webDriver, txtBox);
                    txtBox.Click();
                    txtBox.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                    txtBox.Clear();
                    txtBox.SendKeys(text);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify the User is able to Enter  text '" + text + "'  in the " + name, "'" + text + "'  is Entered successfully in " + name);
                    Logger.Info(text + " is Entered successfully in " + name);
                }
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify the User is able to Enter  text  in the " + name, "Exception occured while entering text in " + name);
                throw;
            }
        }

        /// <summary>
        /// To Validate the Message Content in the Control
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="text">Text to be Entered into the text Box</param>
        /// <param name="name">Element name.</param>
        public static void ValidateMessage(this IWebDriver webDriver, ElementLocator locator, string text, string name)
        {
            try
            {
                var msgContainer = webDriver.GetElement(locator);
                if (msgContainer.Displayed)
                {
                    Logger.Info(name + " is viewed successfully");
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Validate the Message : " + text + " is displayed in the " + name, text + " is successfully displayed in " + name);
                    string actualMessage = msgContainer.GetTextContent();
                    Console.WriteLine(actualMessage);
                    Assert.AreEqual(text, actualMessage);
                    Logger.Info(text + " is successfully displayed in the " + name);
                }
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Validate the Message :  " + text + " is displayed in the " + name, "Exception occured while Validating text in " + name);
                Logger.Error("Failed to Validate the Message : " + text + " in the " + name);
                throw;
            }
        }

        /// <summary>
        /// To Get the Message Content in the Control
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <returns>Returns the Content in the control</returns>
        public static string GetMessage(this IWebDriver webDriver, ElementLocator locator)
        {
            var msgContainer = webDriver.GetElement(locator);
            string actualMessage = string.Empty;
            if (msgContainer.Displayed)
            {
                actualMessage = msgContainer.GetTextContent();
            }

            return actualMessage;
        }

        /// <summary>
        /// To Get the Message Content in the Control
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <returns>Returns the Content in the control</returns>
        public static string GetValueFromDisabledElement(this IWebDriver webDriver, ElementLocator locator)
        {
            var msgContainer = webDriver.GetElementForDisabled(locator);
            string actualMessage = string.Empty;
            if (!msgContainer.Enabled)
            {
                actualMessage = msgContainer.GetAttribute("value");
            }

            return actualMessage;
        }


        /// <summary>
        /// To Get the Message Content in the Control
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <returns>Returns the Content in the control</returns>
        public static string GetValue(this IWebDriver webDriver, ElementLocator locator)
        {
            var msgContainer = webDriver.GetElement(locator);
            string actualMessage = string.Empty;
            if (msgContainer.Displayed)
            {
                actualMessage = msgContainer.GetAttribute("value");
            }

            return actualMessage;
        }

        /// <summary>
        /// To Get the Message Content in the Table
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <returns>Returns the Content in the control</returns>
        public static string GetText(this IWebDriver webDriver, ElementLocator locator)
        {
            var msgContainer = webDriver.GetElement(locator);
            string actualMessage = string.Empty;
            if (msgContainer.Displayed)
            {
                actualMessage = msgContainer.Text;
            }

            return actualMessage;
        }

        /// <summary>
        /// To Get the checked status of the Check Box
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <returns>Returns the Content in the control</returns>
        public static bool IsCheckBoxChecked(this IWebDriver webDriver, ElementLocator locator)
        {
            var msgContainer = webDriver.GetElement(locator);
            if (msgContainer.Selected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// To Select the checked status of the Check Box
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        public static void SelectCheckBoxifUnselected(this IWebDriver webDriver, ElementLocator locator)
        {
            var chkBoxWebElement = webDriver.GetElement(locator);
            if (chkBoxWebElement.Selected)
            {
            }
            else
            {
                chkBoxWebElement.Click();
            }
        }

        /// <summary>
        /// To Get the Message Content in the Table
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <returns>Returns the Content in the table as a List</returns>
        public static Collection<string> GetTableElementsList(this IWebDriver webDriver, ElementLocator locator)
        {
            IList<IWebElement> lstTableElements = webDriver.GetElements(locator);
            Collection<string> lstTableValues = new Collection<string>();
            foreach (IWebElement elmnt in lstTableElements)
            {
                lstTableValues.Add(elmnt.Text);
            }

            return lstTableValues;
        }

        /// <summary>
        /// GetElementText BAsed on Index
        /// </summary>
        /// <param name="webDriver">This is WebDriver</param>
        /// <param name="locator">Locator</param>
        /// <param name="name">name</param>
        /// <param name="index">index</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public static string GetElementTextBasedOnIndex(this IWebDriver webDriver, ElementLocator locator, string name, int index, string value = "")
        {

            try
            {
                IList<IWebElement> lstTableElements = webDriver.GetElements(locator);
                for (int i = 0; i <= lstTableElements.Count; i++)
                {
                    WaitForPageLoad(webDriver);

                    IWebElement e = lstTableElements[index];
                    value = e.Text;

                }
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " : (" + value + ") is Visible ", value + " is visible successfully");
                Logger.Info(name + " is clicked successfully");
                return value;
            }
            catch (TimeoutException ex)
            {
                Logger.Error("Failed to click on " + value + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " : (" + value + ") is clicked ", "An exception occurred while clicking on " + value);
                throw;
            }
        }

        /// <summary>
        /// Click an Element Based on Index
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="locator"></param>
        /// <param name="name"></param>
        /// <param name="index"></param>
        public static void IsElementClickableBasedonIndex(this IWebDriver webDriver, ElementLocator locator, string name, int index)
        {
            try
            {
                WaitForPageLoad(webDriver);
                IList<IWebElement> lstTableElements = webDriver.GetElements(locator);
#pragma warning disable CS0162 // Unreachable code detected
                for (int i = 0; i <= lstTableElements.Count; i++)
#pragma warning restore CS0162 // Unreachable code detected
                {
                    WaitForPageLoad(webDriver);
                    
                    IWebElement e = lstTableElements[index];
                    HighlightingWebElement(webDriver, e);
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
                throw;
            }
        }

        /// <summary>
        /// Click an Element Based on Index
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="locator"></param>
        /// <param name="name"></param>
        /// <param name="index"></param>
        public static void IsElementDragSliderToSetHieght(this IWebDriver webDriver, ElementLocator locator, string name, int index)
        {
            try
            {
                WaitForPageLoad(webDriver);
                IList<IWebElement> lstTableElements = webDriver.GetElements(locator);
                Actions act = new Actions(webDriver);
#pragma warning disable CS0162 // Unreachable code detected
                for (int i = 0; i <= lstTableElements.Count; i++)
#pragma warning restore CS0162 // Unreachable code detected
                {
                    WaitForPageLoad(webDriver);
                    Random rnd = new Random();
                    int randomNo = rnd.Next(2, 7);
                    IWebElement e = lstTableElements[index];
                    HighlightingWebElement(webDriver, e);
                    IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
                    executor.ExecuteScript("window.scrollTo(0, " + e.Location.X + ")");
                    e.Click();
                    //int x = e.Location.X;
                    //int y = e.Location.Y;
                    //Console.WriteLine(e);
                    //act.MoveToElement(e, x, y).Click().Build().Perform();
                    ////act.MoveByOffset(x, y).Click().Build().Perform();
                    break;
                }

            }
            catch (TimeoutException ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                throw;
            }
        }


        /// <summary>
        /// To Get the Message Content in the Table
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="name">Element Name.</param>
        /// <param name="index">Element Index to be clicked.</param>
        /// <param name="text">Element text</param>
        public static void EnterTextInSelectedElementfromList(this IWebDriver webDriver, ElementLocator locator, string name, int index, string text)
        {
            try
            {
                for (int i = 0; i < index; i++)
                {
                    WaitForPageLoad(webDriver);
                    IList<IWebElement> lstTableElements = webDriver.GetElements(locator);
                    IWebElement e = lstTableElements[index];
                    e.Click();
                    e.SendKeys(text);
                }
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is clicked ", name + " is clicked successfully");
                Logger.Info(name + " is clicked successfully");
            }
            catch (TimeoutException ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                throw;
            }
            catch (NoSuchElementException ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                throw;
            }
        }

        /// <summary>
        /// To Get the Message Content in the Table
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="name">Element Name.</param>
        /// <param name="index">Element Index to be clicked.</param>
        public static void IsElementClickableFromListOfElemetsBasedOnIndex(this IWebDriver webDriver, ElementLocator locator, string name, int index)
        {
            try
            {
                WaitForPageLoad(webDriver);
                IList<IWebElement> lstTableElements = webDriver.GetElements(locator);
                IWebElement e = lstTableElements[index];
                if (!e.Selected)
                {
                    e.Click();
                }

                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is clicked ", name + " is clicked successfully");
                Logger.Info(name + " is clicked successfully");
            }
            catch (TimeoutException ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                throw;
            }
            catch (NoSuchElementException ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                throw;
            }
        }

        public static string GetTextForSelectedElementfromList(this IWebDriver webDriver, ElementLocator locator, string name, int index)
        {
            try
            {
                WaitForPageLoad(webDriver);
                IList<IWebElement> lstTableElements = webDriver.GetElements(locator);
                IWebElement e = lstTableElements[index];
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " text ", name + " text is found successfully");
                Logger.Info(name + " text is found successfully");
                return e.Text;
            }
            catch (TimeoutException ex)
            {
                Logger.Error("Failed to get  on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " text ", "An exception occurred while clicking on " + name);
                throw;
            }
            catch (NoSuchElementException ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " text ", "An exception occurred while clicking on " + name);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " text ", "An exception occurred while clicking on " + name);
                throw;
            }
        }

        /// <summary>
        /// To Get the Message Content in the Table
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="name">Element Name.</param>
        /// <param name="index">Element Index text to be returned.</param>
        public static string GetTextFromListOfElementsBasedOnIndex(this IWebDriver webDriver, ElementLocator locator, string name, int index)
        {
            try
            {
                WaitForPageLoad(webDriver);
                IList<IWebElement> lstTableElements = webDriver.GetElements(locator);
                IWebElement e = lstTableElements[index];
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is visible ", name + " is " + e.Text + " visible successfully");
                Logger.Info(name + " is visible successfully");
                return e.Text;
            }
            catch (TimeoutException ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                throw;
            }
            catch (NoSuchElementException ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                throw;
            }
        }


        /// <summary>
        /// To Get the Message Content in the Table
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <param name="name">Element Name.</param>
        public static bool GetTextFromListOfElementsBasedOnName(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                IList<IWebElement> lstTableElements = webDriver.GetElements(locator);
                bool exist = lstTableElements.Any(x => x.Text == name);
                if (!exist)
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is not visible ", name + " is not visible successfully");
                    Logger.Info(name + " is not visible successfully");
                }
                else
                {
                    Assert.IsFalse(exist);
                }

                return exist;
            }
            catch (TimeoutException ex)
            {
                Logger.Error("Failed to visible on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible ", "An exception occurred while clicking on " + name);
                throw;
            }
            catch (NoSuchElementException ex)
            {
                Logger.Error("Failed to visible on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible ", "An exception occurred while clicking on " + name);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to visible on " + name + "Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible ", "An exception occurred while clicking on " + name);
                throw;
            }
        }


        /// <summary>
        /// To Get the Message Content in the Table
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">Element Locator.</param>
        /// <returns>Returns the Content in the table as a List</returns>
        public static HashSet<string> GetTableElementsListHash(this IWebDriver webDriver, ElementLocator locator)
        {
            IList<IWebElement> lstTableElements = webDriver.GetElements(locator);
            HashSet<string> lstTableValues = new HashSet<string>();
            foreach (IWebElement elmnt in lstTableElements)
            {
                lstTableValues.Add(elmnt.Text);
            }

            return lstTableValues;
        }

        /// <summary>
        /// GetScrollPosition.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <returns>Returns the y offset</returns>
        public static long GetScrollPosition(this IWebDriver webDriver)
        {
            var javaScriptExecutor = webDriver as IJavaScriptExecutor;
            return (long)javaScriptExecutor.ExecuteScript("return window.pageYOffset;");
        }

        /// <summary>
        /// Perform a enter text through JavaScript
        /// </summary>
        /// <param name="webDriver">The web driver</param>
        /// <param name="locator">ElementLocator.</param>
        /// <param name="name">name of the element.</param>
        /// <param name="testdata">Text to be written.</param>
        public static void ClearAndTypeInEditBoxUsingJS(this IWebDriver webDriver, ElementLocator locator, string name, string testdata)
        {
            try
            {
                var element = webDriver.GetElement(locator);
                element.Clear();
                element.Click();
                var javaScriptExecutor = webDriver as IJavaScriptExecutor;
                javaScriptExecutor.ExecuteScript("arguments[0].value='" + testdata, element);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify user able clear and type test data  as " + testdata + " in " + name, "User able to type/Enter test data as " + testdata + " in " + name + " successfully");
                Logger.Info(testdata + " is entered in " + name);
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify user able to enter/type required data " + testdata + " in " + name, "An exception occurred waiting for " + name + " to enter any value");
                Logger.Error("Failed tp enter " + testdata + " in " + name);
                throw;
            }
        }

        /// <summary>
        /// Perform a Click through JavaScript
        /// </summary>
        /// <param name="name">name of the element.</param>
        /// <param name="webDriver">The web driver</param>
        /// <param name="locator">Locator</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = " Need to handle exception ")]
        public static void JavaScriptClickWithoutExtent(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                var element = webDriver.GetElement(locator);
                var javaScriptExecutor = webDriver as IJavaScriptExecutor;
                javaScriptExecutor.ExecuteScript("arguments[0].click();", element);
               
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is clicked ", "An exception occurred while clicking on " + name);
                Logger.Error("Failed to click on " + name + "Due to exception: " + ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Perform a Click through JavaScript
        /// </summary>
        /// <param name="name">name of the element.</param>
        /// <param name="webDriver">The web driver</param>
        /// <param name="element">The web element.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = " Need to handle exception ")]
        public static void JavaScriptClick(this IWebDriver webDriver, IWebElement element, string name)
        {
            try
            {
                var javaScriptExecutor = webDriver as IJavaScriptExecutor;
                javaScriptExecutor.ExecuteScript("arguments[0].click();", element);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is clicked ", name + " is clicked successfully");
                Logger.Info(name + " is clicked successfully");
            }
            catch (UnhandledAlertException)
            {
                if (IsAlertPresent(webDriver))
                {
                    webDriver.SwitchTo().Alert().Dismiss();
                    var javaScriptExecutor = webDriver as IJavaScriptExecutor;
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
                throw;
            }
        }

        /// <summary>
        /// Wait Till File Exists
        /// </summary>
        /// <param name="webDriver">The web driver</param>
        /// <param name="filepath">File Path</param>
        /// <returns>The file status</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "wait", Justification = "Need to wait till file exists")]
        public static bool WaitTillFileExists(this IWebDriver webDriver, string filepath)
        {
            var filePresent = File.Exists(filepath);
            while (!File.Exists(filepath))
            {
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.LongTimeout));
            }

            return filePresent;
        }

        /// <summary>
        /// Wait for dom Ready
        /// </summary>
        /// <param name="webDriver">The web driver</param>
        public static void WaitForDocumentReady(this IWebDriver webDriver)
        {
            var timeout = new TimeSpan(0, 0, Convert.ToInt32(BaseConfiguration.ShortTimeout));
            var wait = new WebDriverWait(webDriver, timeout);

            var javascript = webDriver as IJavaScriptExecutor;
            if (javascript == null)
            {
                throw new ArgumentException("driver", "Driver must support javascript execution");
            }

            wait.Until((d) =>
            {
                try
                {
                    string readyState = javascript.ExecuteScript(
                        "if (document.readyState) return document.readyState;").ToString();
                    return readyState.ToLower(CultureInfo.CurrentCulture) == "complete";
                }
                catch (InvalidOperationException e)
                {
                    return e.Message.ToLower(CultureInfo.CurrentCulture).Contains("unable to get browser");
                }
                catch (WebDriverException e)
                {
                    return e.Message.ToLower(CultureInfo.CurrentCulture).Contains("unable to connect");
                }
            });
        }

        /// <summary>
        /// Wait for dom Ready
        /// </summary>
        /// <param name="webDriver">The web driver</param>
        /// <param name="frameId">the frame id</param>
        public static void SwitchToIFrame(this IWebDriver webDriver, string frameId)
        {
            webDriver.SwitchTo().Frame(frameId);
        }

        /// <summary>
        /// Wait for element to be displayed for specified time
        /// </summary>
        /// <example>Example code to wait for login Button: <code>
        /// this.Driver.IsElementPresent(this.loginButton, BaseConfiguration.ShortTimeout);
        /// </code></example>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="elementName">The element Name </param>
        /// <param name="customTimeout">The timeout.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public static bool IsElementPresent(this IWebDriver webDriver, ElementLocator locator, string elementName, double customTimeout)
        {
            try
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + elementName + " is displayed", elementName + " is displayed successfully");
                webDriver.GetElement(locator, customTimeout, e => e.Displayed);

                // DriverContextTH.ExtentStepTest.Log(Status.Pass, "pass in openlandingpage of in common");
                return true;
            }
            catch (NoSuchElementException)
            {
                // DriverContextTH.ExtentStepTest.Log(Status.Fail, "failed in openlandingpage of in common");
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Splits a string with the given separator and returns the collection
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="myString">the String to be split.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>
        /// The Collection
        /// </returns>
        public static Collection<string> GetCollectionofMyTools(this IWebDriver webDriver, string myString, char separator)
        {
            Console.WriteLine(webDriver);
            Collection<string> clcmyToolsToCheck = new Collection<string>();
            if (!string.IsNullOrEmpty(myString))
            {
                string[] lstMyTools = myString.Split(separator);
                foreach (string s in lstMyTools)
                {
                    clcmyToolsToCheck.Add(s);
                }
            }

            return clcmyToolsToCheck;
        }

        /// <summary>
        /// To mouse hover on given webelement
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">ElementLocator.</param>
        /// <param name="webelementname">webelementname.</param>
        public static void MouseOverOnWebElementAndClick(this IWebDriver webDriver, ElementLocator locator, string webelementname)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var web_Element_To_Be_Hovered = webDriver.GetElement(locator);
                HighlightingWebElement(webDriver, web_Element_To_Be_Hovered);
                Actions builder = new Actions(webDriver);
                builder.MoveToElement(web_Element_To_Be_Hovered).Build().Perform();
                builder.Click().Perform();
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify mouse hover and click on " + webelementname, "Mouse hovered and clicked on " + webelementname + " successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while mouse hovering on  " + webelementname + " " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify mouse hover on " + webelementname, "An exception occured while mouse hovering on  " + webelementname);
                throw;
            }
        }

        /// <summary>
        /// To mouse hover on given webelement
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">ElementLocator.</param>
        public static void MouseOverOnWebElement(this IWebDriver webDriver, ElementLocator locator)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var web_Element_To_Be_Hovered = webDriver.GetElement(locator);
                HighlightingWebElement(webDriver, web_Element_To_Be_Hovered);
                Actions builder = new Actions(webDriver);
                builder.MoveToElement(web_Element_To_Be_Hovered).Build().Perform();
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while mouse hovering on  " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify mouse hover on ", "An exception occured while mouse hovering on  ");
                throw;
            }
        }


        public static bool IsExpectedTextMatchWithActualTextFromListOfElements(this IWebDriver webDriver, ElementLocator locator, string value, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(collection));
                foreach (var item in collection)
                {
                    if (item.Text.Equals(value))
                    {
                        return true;
                    }
                }
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is visible in list of elements", name + " is :"+value+ ": visible successfully");
                Logger.Info(name + " are displayed/Enabled successfully");

            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the elements to become visible " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " are visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                throw;
            }
            return true;
        }
        /// <summary>
        /// Enable synchronization with angular.
        /// </summary>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="locator">Enable or disable synchronization.</param>
        /// <param name="name">Name of the Element</param>
        /// <returns>bool.</returns>
        public static IList<IWebElement> AreElementsVisible(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElements(locator);
                ReadOnlyCollection<IWebElement> collection = new ReadOnlyCollection<IWebElement>(webElementLocator);
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
                wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(collection));
#pragma warning restore CS0618 // Type or member is obsolete
                for (int i = 0; i < webElementLocator.Count(); i++)
                {
                    IWebElement checkBox = webElementLocator[i];
                    ScrollToWebElement(webDriver, checkBox);
                    HighlightingWebElement(webDriver, checkBox);
                }

                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " are visible", name + " are visible successfully");
                Logger.Info(name + " are displayed/Enabled successfully");
                return webElementLocator;
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the elements to become visible " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " are visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                throw;
            }
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
        ///
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

        /// <summary>
        /// Select option from My Accounr Menu
        /// </summary>
        /// <param name="webDriver">This is webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="name">Name of the element</param>
        public static void IsMyAccountOptionClicked(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            WaitForPageLoad(webDriver);
            var webElementLocator = webDriver.GetElements(locator);
            ReadOnlyCollection<IWebElement> elements = new ReadOnlyCollection<IWebElement>(webElementLocator);
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(BaseConfiguration.MediumTimeout));
#pragma warning disable CS0618 // Type or member is obsolete
            wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(elements));
#pragma warning restore CS0618 // Type or member is obsolete
            foreach (var item in elements)
            {
                Logger.Info("Links are - :" + item.Text);
                if (item.Text.Contains(name))
                {
                    new Actions(webDriver).MoveToElement(item).Build().Perform();
                    item.Click();
                    break;
                }
            }
        }

        /// <summary>
        /// get All element displayed or not
        /// </summary>
        /// <param name="webDriver">This is Webdriver</param>
        /// <param name="locator">Element found or not found</param>
        /// <param name="name">Name of the Element</param>
        public static void AreAllSmallImagesDisplayed(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                string imageUrl = null;
                bool imageDisplayStatus = false;
                ArrayList a1 = new ArrayList();
                var imagesDicObject = new Dictionary<string, bool>();
                IList<IWebElement> webElementLocator = webDriver.GetElements(locator);
                for (int i = 0; i < webElementLocator.Count; i++)
                {
                    HighlightingWebElement(webDriver, webElementLocator[i]);
                    string href = webElementLocator[i].GetAttribute("src");
                    if ((href.Contains(".png") || href.Contains(".jpg")) && webElementLocator[i].Displayed)
                    {
                        imageUrl = "Position of Image is " + i + " with Img Path is " + href;
                        imageDisplayStatus = true;
                    }
                    else
                    {
                        imageUrl = "No Image displayed at Position " + i + " path is " + href;
                        imageDisplayStatus = false;
                    }

                    imagesDicObject.Add(imageUrl.Trim(), imageDisplayStatus);
                }

                for (int count = 0; count < imagesDicObject.Count; count++)
                {
                    KeyValuePair<string, bool> element = imagesDicObject.ElementAt(count);
                    string keyOfImages = element.Key;
                    bool valueofImagesDispyed = element.Value;

                    if (valueofImagesDispyed)
                    {
                        string value = $"<html><body><p><br>{keyOfImages}</p></body></html>";
                        a1.Add(value);
                        Console.Write(a1);
                        Logger.Info(message: "value of image with url" + a1.ToString());
                        if (count == imagesDicObject.Count - 1)
                        {
                            string temp = string.Empty;
                            foreach (var item in a1)
                            {
                                temp = temp + item + ",";

                                Logger.Info(temp);
                            }

                            Logger.Info("value of image with url after 10 " + a1.ToString());
                            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify all small image are visible with in provided time", "List Small images are displayed for " + "<br>" + "[" + temp + "] ");
                        }
                    }
                    else
                    {
                        DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify small image  is visible with in provided time " + BaseConfiguration.MediumTimeout, "Small image is not displayed for " + keyOfImages);
                        if (count == imagesDicObject.Count - 1)
                        {
                            Assert.That(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                throw;
            }
        }

        /// <summary>
        /// IsSmall Images Displayed
        /// </summary>
        /// <param name="webDriver">This is Webdriver</param>
        /// <param name="locator">Element found or not found</param>
        /// <param name="name">Name of the Element</param>
        public static void AreSmallImagesDisplayed(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                ArrayList items = new ArrayList();
                IList<IWebElement> webElementLocator = webDriver.GetElements(locator);
                for (int i = 0; i < webElementLocator.Count;)
                {
                    items.Add(webElementLocator[i]);

                    if (webElementLocator[i].Displayed)
                    {
                        Assert.IsTrue(webElementLocator[i].GetAttribute("src").Contains(".png"));
                        DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + webElementLocator[i].GetAttribute("src") + " is visible", name + " is visible successfully");
                        Logger.Info(name + " is displayed/Enabled successfully" + webElementLocator[i].GetAttribute("src"));
                    }

                    i++;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                throw;
            }
        }

        /// <summary>
        /// HttpResponseCode for all links
        /// </summary>
        /// <param name="webDriver">This is a webdriver</param>
        /// <param name="locator">Element locator</param>
        /// <param name="name">Name of Element</param>
        public static void AreAllLinksStatusCode200Displayed(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                string links = null;
                bool linksDisplayStatus = true;

                ArrayList a1 = new ArrayList();
                string statusCodevalue = null;
                var linksDicObject = new Dictionary<string, bool>();
                IList<IWebElement> webElementLocator = webDriver.GetElements(locator);
                for (int j = 0; j < webElementLocator.Count; j++)
                {
                    ScrollToWebElement(webDriver, webElementLocator[j]);
                    HighlightingWebElement(webDriver, webElementLocator[j]);
                    string hrefs = webElementLocator[j].GetAttribute("href");
                    statusCodevalue = GetResponseCode(hrefs);
                    Logger.Info(statusCodevalue);
                    if (statusCodevalue.Contains(value: "OK"))
                    {
                        links = "Position of links is " + j + " with Img Path is " + hrefs;
                        linksDisplayStatus = true;
                    }
                    else
                    {
                        links = "No links displayed at Position " + j + " path is " + hrefs;
                        linksDisplayStatus = false;
                    }

                    linksDicObject.Add(links.Trim(), linksDisplayStatus);
                }

                for (int count = 0; count < linksDicObject.Count; count++)
                {
                    KeyValuePair<string, bool> element = linksDicObject.ElementAt(count);
                    string keyOfImages = element.Key;
                    bool valueofImagesDispyed = element.Value;

                    if (valueofImagesDispyed)
                    {
                        string value = $"<html><body><p><br>{keyOfImages}</br> with status Code {statusCodevalue}</p></body></html>";
                        a1.Add(value);
                        Console.Write(a1);
                        Logger.Info(message: "value of image with url" + a1.ToString());
                        if (count == linksDicObject.Count - 1)
                        {
                            string temp = string.Empty;
                            foreach (var item in a1)
                            {
                                temp = temp + item + ",";

                                Logger.Info(temp);
                            }

                            Logger.Info("value of image with url after 10 " + a1.ToString());
                            string details = "List all links are displayed for " + "<br>" + "[" + temp + "] ";
                            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify all links are visible with in provided time", details);
                        }
                    }
                    else
                    {
                        DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify all links  is visible with in provided time " + BaseConfiguration.MediumTimeout, "Title of the links are not displayed for " + keyOfImages);
                        if (count == linksDicObject.Count - 1)
                        {
                            Assert.That(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                throw;
            }
        }

        /// <summary>
        /// get response code
        /// </summary>
        /// <param name="hrefs">statuc code</param>
        /// <returns>string..</returns>
        public static string GetResponseCode(string hrefs)
        {
            Uri ourUri;
            HttpWebRequest request;
            HttpWebResponse response = null;
            ourUri = new Uri(hrefs);
            request = (HttpWebRequest)WebRequest.Create(ourUri);
            response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode.ToString();
        }

        /// <summary>
        /// Are all links displayed in Customer Results Page
        /// </summary>
        /// <param name="webDriver">This is Webdriver</param>
        /// <param name="locator">Location of element</param>
        /// <param name="name">Name of the Element</param>
        public static void AreAllLinksDisplayed(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                IList<IWebElement> webElementLocator = webDriver.GetElements(locator);
                for (int i = 0; i < webElementLocator.Count;)
                {
                    Logger.Info("Current Href - " + webElementLocator[i].GetAttribute("href"));

                    var hrefs = webElementLocator[i].GetAttribute("href");
                    Uri ourUri = new Uri(hrefs);
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(ourUri);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if ((response.StatusCode == HttpStatusCode.Found) ||
                           (response.StatusCode == HttpStatusCode.OK))
                    {
                        Logger.Info("Status code is " + response.StatusCode);
                        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                    }

                    i++;
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + ourUri + " all links ", name + " are Visable successfully");

                    Logger.Info(name + " Are Visable successfully");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                throw;
            }
        }


        public static string GetBrowerName()
        {
            var browserType = TestContext.Parameters.Get("Browser", BaseConfiguration.TestBrowser.ToString());
            var browserType2 = (BrowserType)Enum.Parse(typeof(BrowserType), browserType);
            return browserType2.ToString().ToLower();
        }
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="locator">Enable or Disable</param>
        /// <param name="name">Name of the Element</param>
        public static void IsElementcontainsGivenattribute(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                webDriver.WaitForPageLoad();
                var element = webDriver.GetElement(locator);
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
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify type attribute for " + name + " is not available", "Type attribute for" + name + " is  available");
                throw;
            }
        }

        public static void IsElementGivenAttributePresent(this IWebDriver webDriver, ElementLocator locator, string name, string attributeName, string attributeText, string isReadOnly = "")
        {
            try
            {
                webDriver.WaitForPageLoad();
                bool expectedstatus = false;
                bool status = true;
                if (isReadOnly != string.Empty)
                {
                    var by = locator.ToBy();
                    var readOnlyElement = webDriver.FindElement(by);
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
                    var element = webDriver.GetElement(locator);
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
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify type attribute for " + name + " is not available", "Type attribute for" + name + " is  available");
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="locator"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public static void IsElementContainGivenAttribute(this IWebDriver webDriver, ElementLocator locator, string text, string value)
        {
            try
            {
                webDriver.WaitForPageLoad();
                var element = webDriver.GetElement(locator);
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
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify type attribute for " + text + " is not available", "Type attribute for" + text + " is  available");
                throw;
            }
        }

        /// <summary>
        /// Full View button Enable to Disable
        /// </summary>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="locator">Enable or Disable</param>
        /// <param name="name">Name of the Element</param>
        public static void IsListViewModeSelected(this IWebDriver webDriver, ElementLocator locator, string name)
        {
            try
            {
                WaitForPageLoad(webDriver);
                var webElementLocator = webDriver.GetElement(locator);

                // bool webElementLocator = webDriver.GetElement(locator).GetAttribute("class").Contains("active");
                if (webElementLocator.Displayed)
                {
                    HighlightingWebElement(webDriver, webElementLocator);
                    Assert.IsTrue(webElementLocator.GetAttribute("class").Contains("active"));
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is Selected", name + " is Selected Successfully");

                    // DriverContext.ExtentStepTest.Log(Status.Pass, name + " is displayed successfully");
                    Logger.Info(name + " is Selected successfully");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is FullView button with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for " + name + " to become visible");
                throw;
            }
        }

        /// <summary>
        /// Approves the trust certificate for internet explorer.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        private static void ApproveCertificateForInternetExplorer(this IWebDriver webDriver)
        {
            if ((BaseConfiguration.TestBrowser.Equals(BrowserType.InternetExplorer) || BaseConfiguration.TestBrowser.Equals(BrowserType.IE)) && webDriver.Title.Contains("Certificate"))
            {
                webDriver.FindElement(By.Id("overridelink")).JavaScriptClick();
            }
        }

        /// <summary>
        /// Page Scroll
        /// </summary>
        /// <param name="webDriver">This is webdriver</param>
        public static void PageScrollUpToTop(this IWebDriver webDriver)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
                js.ExecuteScript("window.scrollTo(0, 0)");
                System.Threading.Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Page Scroll
        /// </summary>
        /// <param name="webDriver">This is webdriver</param>
        public static void PageScrollDown(this IWebDriver webDriver)
        {
            try
            {
                long scrollHeight = 0;
                do
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
                    var newScrollHeight = (long)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight); return document.body.scrollHeight;");

                    if (newScrollHeight == scrollHeight)
                    {
                        break;
                    }
                    else
                    {
                        scrollHeight = newScrollHeight;
                        Thread.Sleep(400);
                    }
                } while (true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Page Scroll down to bottom
        /// </summary>
        /// <param name="webDriver">This is Webdriver</param>
        public static void PageScrollDownToBottom(this IWebDriver webDriver)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
                js.ExecuteScript("window.scrollTo(300, 600)");
                System.Threading.Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get Count of Elements
        /// </summary>
        /// <param name="webDriver">This is WebDriver</param>
        /// <param name="locator">This is Locator</param>
        /// <param name="maxWaitTime">Max Wait time</param>
        /// <returns></returns>
        public static int CountForElements(this IWebDriver webDriver, ElementLocator locator, int maxWaitTime = 20)
        {
            int element = 0;
            try
            {
                IList<IWebElement> webElementLocator = webDriver.GetElements(locator);

                element = webElementLocator.Count;
            }
            catch
            {
                element = 0;
            }
            return element;
        }


        /// <summary>
        /// This method is used to upload a file into application
        /// </summary>
        /// <param name="webDriver">this is webdriver</param>
        /// <param name="locator">Element Locator</param>
        /// <param name="path">File Path</param>
        public static void UploadFileUsingInputTag(this IWebDriver webDriver, ElementLocator locator, string path)
        {
            try
            {
                var webElementLocator = webDriver.GetElement(locator);
                HighlightingWebElement(webDriver, webElementLocator);
                ////webElementLocator.JavaScriptClick();
                Thread.Sleep(2000);
                webElementLocator.SendKeys(path);
                ////SendKeys.SendWait(path);
                ////SendKeys.SendWait(@"{Enter}");

                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify file uploaded", " file uploaded  successfully");
                Logger.Info(path + "  file uploaded  successfully");
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify file uploaded", " Failed to uploaded file");

                throw;
            }
        }

    }
}
