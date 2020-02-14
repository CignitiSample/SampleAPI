// <copyright file="InternalStaffMembersPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
    using System;
    using System.Collections.Generic;
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Types;
    using global::MCS.Test.Automation.Tests.PageObjects;
    using NLog;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using RelevantCodes.ExtentReports;

    public class InternalStaffMembersPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ElementLocator
        cityColumnHead = new ElementLocator(Locator.CssSelector, "th.header.header--City.header--expandable");

        private readonly ElementLocator
            membersColumnHead = new ElementLocator(Locator.XPath, "//th[text()=' {0}']");

        private readonly ElementLocator
            ellipsis = new ElementLocator(Locator.CssSelector, "i.icon.ellipsis.vertical");

        private readonly ElementLocator
            checkBoxToSelectColumnPreference = new ElementLocator(Locator.XPath, "//div[@class='react-grid-action-menu-container']//label[text()='{0}']");

        private readonly ElementLocator
         loadbutton = new ElementLocator(Locator.XPath, "//*[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
            emailLink = new ElementLocator(Locator.XPath, "(//a[@class='column--Email'])[1]");

        private readonly ElementLocator
         listOfColumnsToSelected = new ElementLocator(Locator.XPath, "//div[@class='option']//div//label");

        private readonly ElementLocator
         sortIcon = new ElementLocator(Locator.XPath, "//th[text()=' {0}']//i");

        private readonly ElementLocator
         aftersort = new ElementLocator(Locator.XPath, "//p[@class='column--Name']");

        private readonly ElementLocator
        sortAccount = new ElementLocator(Locator.XPath, "//p[@class='column--Account']");

        private string nmsortAccount = "Sort Account";
        private string nmellipsis = "Ellipsis for Column Preferences";
        private string nmcheckBox = "Check Box for Selecting Column Head Preference";
        private string nmsortIcon = "Sort Icon";
        private string nmsorted = "Sorted";
        private string nmcolumnValue = "Name Column";
        private string nmdimmerloading = "Dimmer loading";

        public InternalStaffMembersPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public void GetPageTitle(string expectedTitle)
        {
            try
            {
                string actualTitle = this.Driver.Title;
                Assert.AreEqual(expectedTitle, actualTitle);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Page title Text  is matching with Actual title text ", "The expected Value is " + expectedTitle + " and  actual value is " + actualTitle + " matching successfully");
                Logger.Info("Expected text " + expectedTitle + " and Actual text is " + actualTitle);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void IsColumnHeadVisibleInMembersPage(string name)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            bool value = this.Driver.IsElementVisibleWithSoftAssertion(this.membersColumnHead.Format(name), name);
            if (value == true)
            {
                Verify.That(this.DriverContext, () => Assert.IsTrue(value), "Verifying whether the :" + name + ": column head is visible", name + ": column head  is visible", "column head  is not visible");
            }
            else
            {
                this.Driver.IsElementClickable(this.ellipsis, this.nmellipsis);
                this.Driver.IsElementClickable(this.checkBoxToSelectColumnPreference.Format(name), this.nmcheckBox);
                bool valueAfterPreferenceSet = this.Driver.IsElementVisibleWithSoftAssertion(this.membersColumnHead.Format(name), name);
                Verify.That(this.DriverContext, () => Assert.IsTrue(valueAfterPreferenceSet), "Verifying whether the :" + name + ": column head is visible", name + ": column head  is visible", "column head  is not visible");
            }
        }

        public void IsEmailDisplayedAsHyperLink()
        {
            var value = this.Driver.GetText(this.emailLink);
            var webElement = this.Driver.GetElement(this.emailLink);
            bool linkexist = webElement.GetAttribute("href").Contains(value);
            Verify.That(this.DriverContext, () => Assert.IsTrue(linkexist), "Verifying whether email displayed  is hyperlink", "Email displayed is hyperlink", "Email displayed is not a hyperlink");
        }

        public void IsUserableToGetListColumnsToBeSelected()
        {
            string verifyMessage = "To Verify columns list";
            string failMessage = "An exception occured while verifying Columns list";
            string passMessage = "List Of Columns Contains All expected columns";
            try
            {
                this.Driver.IsElementClickable(this.ellipsis, this.nmellipsis);
                IList<string> lstElements1 = new List<string>() { "Account", "Name", "Company", "Email", "Committee", "Status", "Member Type", "Phone Number", "Country", "State", "City", "Postal Code" };
                IList<string> lstElements2 = new List<string>();
                IList<IWebElement> webelementslist = this.Driver.GetElements(this.listOfColumnsToSelected);
                foreach (IWebElement i in webelementslist)
                {
                    lstElements2.Add(i.Text);
                }

                for (int i = 0; i < lstElements1.Count; i++)
                {
                    if (lstElements1[i] != lstElements2[i])
                    {
                        Verify.That(this.DriverContext, () => Assert.AreSame(lstElements2[i], lstElements1[i], failMessage));
                        break;
                    }
                }

                DriverContext.ExtentStepTest.Log(LogStatus.Pass, verifyMessage, passMessage);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed  Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, verifyMessage, failMessage);
                throw;
            }
        }

        public void IsAbleToSelectColumns()
        {
            this.Driver.IsElementClickable(this.ellipsis, this.nmellipsis);
        }

        public void IsUserAbleToClickOnSortIcon(string name)
        {
            this.Driver.WaitUntilElementIsFound(this.sortIcon.Format(name), 90);
            this.Driver.IsElementVisible(this.sortIcon.Format(name), this.nmsortIcon);
            var webElement = this.Driver.GetElement(this.sortIcon.Format(name));
            this.Driver.JavaScriptClick(webElement, this.nmsortIcon);
        }

        public void IsColumnSortedSuccessfully(string name)
        {
            this.Driver.WaitUntilElementIsFound(this.sortIcon.Format(name), 90);
            this.Driver.IsElementVisible(this.sortIcon.Format(name), this.nmsortIcon);
        }

        public void AreListOfElementsDisplayInAlphabeticalOrderOrNot(string name)
        {
            this.Driver.AreElementsSortedInAlphabeticalOrder(this.aftersort, this.nmsorted);
        }

        public void IsUserAbleToViewExpandingAndContracting(string expected)
        {
            this.Driver.IsElementClickable(this.aftersort, this.nmcolumnValue);
            this.Driver.WaitUntilElementIsFound(this.aftersort, BaseConfiguration.MediumTimeout);
            var webelement = this.Driver.GetElement(this.aftersort);
            string actual = webelement.GetAttribute("style");
            try
            {
                Assert.AreNotEqual(expected, actual);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Expected Value is not matching with Actual text ", "The expected Value is " + expected + " and  actual value is " + actual + " not matching successfully");
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify text on " + expected + "with Actual Text " + actual, "An exception occurred while finding text on " + expected);
                throw;
            }
        }

        public void AreListOfElementsInAccountsDescendingOrder()
        {
            this.Driver.WaitForPageLoad();
            this.Driver.AreElementsSortedInAlphabeticalOrder(this.sortAccount, this.nmsortAccount, "desc");
        }
    }
}
