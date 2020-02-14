// <copyright file="ManageCommitteeTypes.cs" company="PlaceholderCompany">
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
    using global::NUnit.Framework;
    using NLog;
    using OpenQA.Selenium;
    using RelevantCodes.ExtentReports;

#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1649 // File name should match first type name
    public class ManageCommitteeTypesPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator
            committeTypeheader = new ElementLocator(Locator.CssSelector, "div.headingTitle.clearfix h2");

        private readonly ElementLocator
            addCommitteeTypeButton = new ElementLocator(Locator.CssSelector, "button.ui.secondary.button");

        private readonly ElementLocator
            committeeTypetext = new ElementLocator(Locator.Name, "CommitteeTypeName");

        private readonly ElementLocator
           manageCommitteeHierarchytext = new ElementLocator(Locator.Name, "level");

        private readonly ElementLocator
            addCommitteeTypesavebutton = new ElementLocator(Locator.CssSelector, "button.ui.primary.button");

        private readonly ElementLocator
            addLevel = new ElementLocator(Locator.XPath, "//a[@class='addBtn']");

        private readonly ElementLocator
            successfullMsg = new ElementLocator(Locator.CssSelector, "div.ui.compact.message.success div.content p");

        private readonly ElementLocator
           manageCommitteeHierarchyHeader = new ElementLocator(Locator.XPath, "//h5[contains(text(),'{0}')]");

        private readonly ElementLocator
           membershipTypeEligibleTojoinHeader = new ElementLocator(Locator.XPath, "//h5[contains(text(),'{0}')]");

        private readonly ElementLocator
           balanceRequiredHeader = new ElementLocator(Locator.XPath, "//label[contains(text(),'{0}')]");

        private readonly ElementLocator
           enableCommitteeTypeonwebHeader = new ElementLocator(Locator.XPath, "//label[contains(text(),'{0}')]");

        private readonly ElementLocator
            errorMsgUnique = new ElementLocator(Locator.CssSelector, "div.ui.compact.message.error div.content p");

        private readonly ElementLocator
<<<<<<< HEAD
<<<<<<< HEAD
           getLatestRecord = new ElementLocator(Locator.XPath, "//table[1]//tr[1]//td[1]//a[contains(.,'{0}')]");
=======
         errorHierarchyUniqueLevel = new ElementLocator(Locator.XPath, "//span[contains(.,'{0}')]");
>>>>>>> feature/Sprint16-QTA-895-MCS2-2035

=======
            radioButtonForLimitedMembers = new ElementLocator(Locator.XPath, "//label[text()='Limited']");

        private readonly ElementLocator
            textBoxforNumbers = new ElementLocator(Locator.XPath, "//input[@name='NoOfMembersPermitted']");

        private readonly ElementLocator
            incorrectInput = new ElementLocator(Locator.CssSelector, "span.errorMessage");

        private string nmIncorrectInput = "Error Message for Incorrect Input";
        private string nmtextBoxforNumbers = "TextBox to Enter Members Permitted";
        private string nmradioButtonForLimitedMembers = "Max No Of Members Permitted Radio Button";
>>>>>>> feature/Sprint16_QTA-962-MCS2-1979
        private string nmaddcommiteeTypesuccessfullmessage = "Committee Type added successfully.";
        private string nmaddCommitteeTypeTitleButton = "Add Committee Type button";
        private string nmcommitteeTypetext = "Committee Type Name";
        private string nmcmanageCommitteeHierarchytext = "Committee Hierarchy Add level";
        private string nmaddCommitteeTypeSaveButton = "Add New Committee Type Save button";
        private string nmaddlevelbutton = "Add Committee Hierarchy level button";
        private string nmcommitteTypeheader = "Committee Type header";
        private string nmmanageCommitteeHierarchyheader = "Committee Hierarchy header";
        private string nmmembershipTypeEligibleTojoinHeader = "Membership Type Eligible To Join header";
        private string nmbalanceRequiredHeader = "Balance Required header";
        private string nmenableCommitteeTypeonwebHeader = "Enable Committee Type On Web header";
        private string nmaddcommiteeTypeUniqueerrormessage = "Unique Committee Type error";
<<<<<<< HEAD
        private string nmgetlatestRecord = "Latest Record:  {0} ,";
=======
        private string nmHierarchyUniqueLevelerrormessage = "Hierarchy Unique Level";
>>>>>>> feature/Sprint16-QTA-895-MCS2-2035

        public ManageCommitteeTypesPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        
        public void IsManageCommitteeHierarchyHeaderDisplayed(string header)
        {
            this.Driver.WaitUntilElementIsFound(this.manageCommitteeHierarchyHeader.Format(header), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.manageCommitteeHierarchyHeader.Format(header), this.nmmanageCommitteeHierarchyheader);
        }

        public void IsMembershipTypeEligibleTojoinHeaderDisplayed(string header)
        {
            this.Driver.WaitUntilElementIsFound(this.membershipTypeEligibleTojoinHeader.Format(header), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.membershipTypeEligibleTojoinHeader.Format(header), this.nmmembershipTypeEligibleTojoinHeader);
        }

        public void IsBalanceRequiredHeaderDisplayed(string header)
        {
            this.Driver.WaitUntilElementIsFound(this.balanceRequiredHeader.Format(header), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.balanceRequiredHeader.Format(header), this.nmbalanceRequiredHeader);
        }

        public void IsEnableCommitteeTypeonwebHeaderDisplayed(string header)
        {
            this.Driver.WaitUntilElementIsFound(this.enableCommitteeTypeonwebHeader.Format(header), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.enableCommitteeTypeonwebHeader.Format(header), this.nmenableCommitteeTypeonwebHeader);
        }

        public void IsManageCommitteTypeHeaderDisplayed(string header)
        {
            var text = this.Driver.GetText(this.committeTypeheader);
            this.Driver.IsElementVisible(this.committeTypeheader.Format(header), this.nmcommitteTypeheader);
            Assert.AreEqual(text, header, "Member Committee Type Title header is missing");
        }


        public void IsGetLatestRecordDisplayed(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.getLatestRecord.Format(expected), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.getLatestRecord.Format(expected), string.Format(this.nmgetlatestRecord, expected));
        }

        public void IsAddCommitteeTypeButtonClickable()
        {
            this.Driver.WaitUntilElementIsFound(this.addCommitteeTypeButton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.addCommitteeTypeButton);
            this.Driver.IsElementVisible(this.addCommitteeTypeButton, this.nmaddCommitteeTypeTitleButton);
            this.Driver.JavaScriptClick(webElement);
        }

        public void IsNewCommitteeTypeaddedsuccessfullyDisplayed(string message)
        {
            this.Driver.IsElementVisible(this.successfullMsg, this.nmaddcommiteeTypesuccessfullmessage);
            this.Driver.IsExpectedTextMatchWithActualText(this.successfullMsg, message, this.nmaddcommiteeTypesuccessfullmessage);
        }

        public void IsNewCommitteeTypeUniqueErrorMessageDisplayed(string expected)
        {
            string actual = this.Driver.GetText(this.errorMsgUnique);
            this.Driver.IsExpectedTextMatchWithActualText(this.errorMsgUnique, expected, actual);
        }

        public void IsUserAbletoEnterCommitteeTypeInTextBox(string committeeTypeNameText)
        {
            this.Driver.IsElementVisible(this.committeeTypetext, this.nmcommitteeTypetext);
            this.Driver.EnterText(this.committeeTypetext, committeeTypeNameText, this.nmcommitteeTypetext);
        }

        public void IsUserAbletoEnterCommitteeHierarcyInTextBox(string committeeHierarchyNameText)
        {
            this.Driver.IsElementVisible(this.manageCommitteeHierarchytext, this.nmcmanageCommitteeHierarchytext);
            this.Driver.EnterText(this.manageCommitteeHierarchytext, committeeHierarchyNameText, this.nmcmanageCommitteeHierarchytext);
        }

        public void IsAddNewCommitteeTypeSaveButtonClickable()
        {
            this.Driver.IsElementVisible(this.addCommitteeTypesavebutton, this.nmaddCommitteeTypeSaveButton);
            this.Driver.IsElementClickable(this.addCommitteeTypesavebutton, this.nmaddCommitteeTypeSaveButton);
        }

        public void IsAddLevelButtonClickable()
        {
            this.Driver.IsElementVisible(this.addLevel, this.nmaddlevelbutton);
            this.Driver.IsElementClickable(this.addLevel, this.nmaddlevelbutton);
        }
<<<<<<< HEAD
        public void IsHierarchyUniqueLevelErrorDisplayed(string header)
        {
            this.Driver.WaitUntilElementIsFound(this.errorHierarchyUniqueLevel.Format(header), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.errorHierarchyUniqueLevel.Format(header), this.nmHierarchyUniqueLevelerrormessage);
=======

        public void IsLimitedPersonsSelectedAndEnterNumber(string num)
        {
            this.Driver.IsElementClickable(this.radioButtonForLimitedMembers, this.nmradioButtonForLimitedMembers);
            this.Driver.EnterText(this.textBoxforNumbers, num, this.nmtextBoxforNumbers);
        }

        public void IserrorMsgDisplayedForIncorrectInput(string errorMessage)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.incorrectInput, errorMessage);
>>>>>>> feature/Sprint16_QTA-962-MCS2-1979
        }
    }
}