// <copyright file="InternalStaffCommitteeManagementPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Helpers;
    using global::MCS.Test.Automation.Common.Types;
    using global::MCS.Test.Automation.Tests.PageObjects;
    using NLog;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using RelevantCodes.ExtentReports;

    public class InternalStaffCommitteeManagementPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        //// Locators
        private readonly ElementLocator
       dimmerVisible = new ElementLocator(Locator.XPath, "//div[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
         committeeManagementMenu = new ElementLocator(Locator.XPath, "//a[contains(text(),'Committee Management')]");

        private readonly ElementLocator
         addCommitteeBtn = new ElementLocator(Locator.XPath, "//button[text()='Add Committee']");

        private readonly ElementLocator
         committeesHeader = new ElementLocator(Locator.CssSelector, "div.headingTitle.clearfix>h2");

        private readonly ElementLocator
         addCommitteePopupHeader = new ElementLocator(Locator.XPath, "//div[@class='header'][text()='ADD COMMITTEE ']");

        private readonly ElementLocator
           committeeTypeDropdownListIcon = new ElementLocator(Locator.XPath, "//label[text()='Select Committee Type']//..//div[@class='ui selection dropdown']//i");

        private readonly ElementLocator
            committeeTypeDrpDwnListOptions = new ElementLocator(Locator.XPath, "//label[text()='Select Committee Type']//..//span[@class='text']");

        private readonly ElementLocator
            selectCommitteTypeInCommitteeTypeDrpDwn = new ElementLocator(Locator.XPath, "//label[text()='Select Committee Type']//..//span[text()='{0}']");

        ////Static Variables
        private string nmCommitteeManagementMenu = "Committee Management Menu";
        private string nmAddCommitteeBtn = "Add Committee button";
        private string nmCommitteesHeader = "Committees header";
        private string nmaddCommitteePopupHeader = "Add Committee Popup Header";
        private string nmcommitteeTypeDropdownListIcon = "Committee Type Dropdown List Icon";
        private string nmnewlyAddedCommitteeType = "Newly Added Committee Type";
        private string nmDimmer = "Dimmer";

        public InternalStaffCommitteeManagementPage(DriverContext driverContext)
           : base(driverContext)
        {
        }

        public void IsCommitteeManagementMenuItemClickable()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmDimmer);
            var webElement = this.Driver.GetElement(this.committeeManagementMenu);
            this.Driver.JavaScriptClick(webElement, this.nmCommitteeManagementMenu);
        }

        public void IsCommitteesHeaderVisible()
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.committeesHeader, this.nmCommitteesHeader, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.committeesHeader, this.nmCommitteesHeader);
        }

        public void IsUserIsAbleToClickOnAddCommitteeButton()
        {
            this.Driver.WaitUntilElementIsFound(this.addCommitteeBtn, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.addCommitteeBtn);
            this.Driver.JavaScriptClick(webElement, this.nmAddCommitteeBtn);
        }

        public void IsAddCommitteePopupHeaderVisible()
        {
            this.Driver.WaitUntilElementIsFound(this.addCommitteePopupHeader, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.addCommitteePopupHeader, this.nmaddCommitteePopupHeader);
        }

        public void IsUserIsAbleToClickOnCommitteeTypeDropDownList()
        {
            this.Driver.WaitUntilElementIsFound(this.committeeTypeDropdownListIcon, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.committeeTypeDropdownListIcon);
            this.Driver.JavaScriptClick(webElement, this.nmcommitteeTypeDropdownListIcon);
        }

        public void ValidateCommitteeTypeDrpDwnOptionsHoldsNewlyAddedCommitteeTypeFromRnE(string newlyAddedCommitteeType)
        {
            try
            {
                this.Driver.WaitUntilElementIsFound(this.committeeTypeDrpDwnListOptions, BaseConfiguration.LongTimeout);

                IList<IWebElement> itemsCommitteeTypes = this.Driver.GetElements(this.committeeTypeDrpDwnListOptions);
                IList<string> committeeTypesText = new List<string>();
                foreach (IWebElement i in itemsCommitteeTypes)
                {
                    committeeTypesText.Add(i.Text);
                }

                if (committeeTypesText.Contains(newlyAddedCommitteeType))
                {
                    Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.selectCommitteTypeInCommitteeTypeDrpDwn.Format(newlyAddedCommitteeType), this.nmnewlyAddedCommitteeType);

                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify Committee Type added in Rules and Exception reflected on Add Committee Popup while adding new Committee Type", "Committee Type added in Rules and Exception reflected on Add Committee Popup while adding new Committee Type");
                    Logger.Info("Committee Type added in Rules and Exception reflected on Add Committee Popup while adding new Committee Type");
                }
                else
                {
                    Assert.IsFalse(true);
                    DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify Committee Type added in Rules and Exception reflected on Add Committee Popup while adding new Committee Type ", "Committee Type added in Rules and Exception is not reflected on Add Committee Popup while adding new Committee Type");
                    Logger.Info("Committee Type added in Rules and Exception is not reflected on Add Committee Popup while adding new Committee Type");
                }
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify Committee Type added in Rules and Exception reflected on Add Committee Popup while adding new Committee Type ", "Committee Type added in Rules and Exception is not reflected on Add Committee Popup while adding new Committee Type");
                Logger.Error("Committee Type added in Rules and Exception not reflected on Add Committee Popup while adding new Committee Type");
                throw new Exception("Committee Type added in Rules and Exception not reflected on Add Committee Popup while adding new Committee Type");
            }
        }
    }
}
