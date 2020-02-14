// <copyright file="ManageMeetingTypesPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
    using System.Collections.Generic;
    using System.Data;
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Helpers;
    using global::MCS.Test.Automation.Common.Types;
    using global::MCS.Test.Automation.Tests.PageObjects;
    using global::NUnit.Framework;
    using NLog;
    using OpenQA.Selenium;
    using RelevantCodes.ExtentReports;

#pragma warning disable SA1600 // Elements should be documented
    public class ManageMeetingTypesPage : ProjectPageBase
#pragma warning restore SA1600 // Elements should be documented
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator
meetingType = new ElementLocator(Locator.XPath, "//*[@name='TitleName']");

        private readonly ElementLocator
meetingDescription = new ElementLocator(Locator.XPath, "//*[@name='Description']");

        private readonly ElementLocator
savebutton = new ElementLocator(Locator.CssSelector, "button.ui.primary.button");

        private readonly ElementLocator
addmeetingTypeButton = new ElementLocator(Locator.CssSelector, "button.ui.secondary.button");

        private readonly ElementLocator
            passwordErrormsg = new ElementLocator(Locator.XPath, "//*[@class='errorMessage' and contains(text(), 'Password is required.')]");

        private readonly ElementLocator
           successfullMsg = new ElementLocator(Locator.CssSelector, "div.ui.compact.message.success div.content p");

        private readonly ElementLocator
          loadbutton = new ElementLocator(Locator.XPath, "//*[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
            meetingTypesHeader = new ElementLocator(Locator.XPath, "//h2[text()='Meeting Types']");

        private readonly ElementLocator
        addMeetingTypePopup = new ElementLocator(Locator.XPath, "//div[@class='header'][text()='Add Meeting Type']//..//button[@type='submit']");

        private readonly ElementLocator
            editmeetingTypePopup = new ElementLocator(Locator.XPath, "//div[@class='ui modal transition visible active tiny infoBox']");

        private readonly ElementLocator
           newlyAddedTypeValidate = new ElementLocator(Locator.XPath, "(//td[@class='appName']/a[text()='{0}'])[1]");

        private readonly ElementLocator
            getMeetingTypeDesc = new ElementLocator(Locator.XPath, "(//tr//td[@class='appName']//..//following-sibling::td)[1]");

        private readonly ElementLocator
            getStatus = new ElementLocator(Locator.XPath, "(//tr//td[@class='appName']//..//following-sibling::td)[2]");

        private readonly ElementLocator
           meetingTypeNameRequiredErrorMsg = new ElementLocator(Locator.XPath, "//span[text()='Meeting Type Name is required.']");

        private readonly ElementLocator
            meetingTypeNameTextBoxHighlighted = new ElementLocator(Locator.XPath, "//label[text()='Meeting Type Name ']//..//following-sibling::div[@class='field error']");

        private readonly ElementLocator
          meetingTypeDescRequiredErrorMsg = new ElementLocator(Locator.XPath, "//span[text()='Description is required.']");

        private readonly ElementLocator
            meetingTypeDescTextAreaHighlighted = new ElementLocator(Locator.XPath, "//label[text()='Description']//..//following-sibling::div[@class='field error']");

        private readonly ElementLocator
            meetingTypesExistingRecord = new ElementLocator(Locator.XPath, "//tbody//tr[1]//td");

        private readonly ElementLocator
            meetingTypeAlreadyExistsErrorMsg = new ElementLocator(Locator.XPath, "//span[text()='Meeting Type Name already exists.']");

        private readonly ElementLocator
       selectedMeetingType = new ElementLocator(Locator.XPath, "//table[@class='customTable meetingTypeTable']//..//tr[{0}]//a");

        private readonly ElementLocator
       editButton = new ElementLocator(Locator.XPath, "//i[@class='pencil icon']");

        private readonly ElementLocator
            meetingTypeStatusInactive = new ElementLocator(Locator.XPath, "//div[@class='ui checked radio checkbox']/label[text()='Inactive']");

        private readonly ElementLocator
            meetingTypeStatusInActive = new ElementLocator(Locator.XPath, "//label[text()='Inactive']");

        private string nmaddMeetingType = "Add Meeting Type button";
        private string nmmeetingTypetxt = "Meeting Type text box";
        private string nmmeetingDescription = "Meeting Description";
        private string nmsavebutton = "Save button";
        private string nmmeetypesuccessmsg = "Meeting Type added successfully.";
        private string nmmeetingTypesHeader = "Meeting Types header";
        private string nmaddMeetingTypePopup = "Add Meeting Type Popup";
        private string nmmeetingTypeNameRequiredErrorMsg = "Meeting Type Name required Error Message";
        private string nmmeetingTypeNameTextBoxHighlighted = "Meeting Type Name Text box Highlighted";
        private string nmmeetingTypeDescRequiredErrorMsg = "Description required Error Message";
        private string nmmeetingTypeDescTextAreaHighlighted = "Description Text box Highlighted";
        private string nmmeetingTypesExistingRecordItems = "Meeting Types Existing Record Items";
        private string nmmeetingTypeAlreadyExistsErrorMsg = "Meeting Type Already Exists Error Message";
        private string nmmeetingTypeAdded = "Meeting Type";
        private string nmmeetingTypelink = "Meeting Type link";
        private string nmeditMeetingTypePopup = "Edit Meeting Type Popup";
        private string nmdimmerloading = "dimmer loading";
        private string nmEditButton = "Edit Button";
        private string nmStatusInactive = "Meeting Type Status Inactive";
        private string nmStatusInActive = "Meeting Type Status InActive radio button";
        private string nmmeetingTypeUpdateSuccessMsg = "Meeting Type updated successfully.";

#pragma warning disable SA1600 // Elements should be documented
        public ManageMeetingTypesPage(DriverContext driverContext)
#pragma warning restore SA1600 // Elements should be documented
            : base(driverContext)
        {
        }

#pragma warning disable SA1600 // Elements should be documented
        public void EnterMeetingType(string meetingType)
#pragma warning restore SA1600 // Elements should be documented
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.meetingType, this.nmmeetingTypetxt);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.meetingType, meetingType, this.nmmeetingTypetxt);
        }

#pragma warning disable SA1600 // Elements should be documented
        public void EnterMeetingDescription(string description)
#pragma warning restore SA1600 // Elements should be documented
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.meetingDescription, this.nmmeetingDescription);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.meetingDescription, description, this.nmmeetingDescription);
        }

#pragma warning disable SA1600 // Elements should be documented
        public void IsMeetingTypeSaveButtonClickable()
#pragma warning restore SA1600 // Elements should be documented
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, 60, this.nmsavebutton);
            this.Driver.IsElementVisible(this.savebutton, this.nmsavebutton);
            this.Driver.IsElementClickable(this.savebutton, this.nmsavebutton);
        }

#pragma warning disable SA1600 // Elements should be documented
        public void IsUpdateSuccessfullMessageForAddMeetingTypeDisplayed(string updateSuccessMsg)
#pragma warning restore SA1600 // Elements should be documented
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.loadbutton, this.nmmeetingTypeUpdateSuccessMsg, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.successfullMsg, this.nmmeetingTypeUpdateSuccessMsg);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.successfullMsg, updateSuccessMsg, this.nmmeetingTypeUpdateSuccessMsg);
        }

#pragma warning disable SA1600 // Elements should be documented
        public void IsSuccessfullMessageForAddMeetingTypeDisplayed(string successmessage)
#pragma warning restore SA1600 // Elements should be documented
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmmeetypesuccessmsg);
            this.Driver.IsElementVisible(this.successfullMsg, this.nmmeetypesuccessmsg);
            this.Driver.IsExpectedTextMatchWithActualText(this.successfullMsg, successmessage, this.nmmeetypesuccessmsg);
        }

#pragma warning disable SA1600 // Elements should be documented
        public void IsAddMeetingTypeButtonClickable()
#pragma warning restore SA1600 // Elements should be documented
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, 60, this.nmaddMeetingType);
            this.Driver.IsElementVisible(this.addmeetingTypeButton, this.nmaddMeetingType);
            this.Driver.IsElementClickable(this.addmeetingTypeButton, this.nmaddMeetingType);
        }

#pragma warning disable SA1600 // Elements should be documented
        public void IsMeetingTypesHeaderDisplayed(string header)
#pragma warning restore SA1600 // Elements should be documented
        {
            var text = this.Driver.GetText(this.meetingTypesHeader);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.meetingTypesHeader.Format(header), this.nmmeetingTypesHeader);
        }

#pragma warning disable SA1600 // Elements should be documented
        public void IsAddMeetingTypePopupVisible()
#pragma warning restore SA1600 // Elements should be documented
        {
            this.Driver.WaitUntilElementIsFound(this.addMeetingTypePopup, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.addMeetingTypePopup, this.nmaddMeetingTypePopup);
        }

#pragma warning disable SA1600 // Elements should be documented
        public void IsUserAbleToViewNewlyCreatedMeetingType(string meetingType, string meetingTypeDesc, string status)
#pragma warning restore SA1600 // Elements should be documented
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.newlyAddedTypeValidate.Format(meetingType), this.nmmeetingTypeAdded, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.newlyAddedTypeValidate.Format(meetingType), this.nmmeetingTypeAdded);

            string actualMeetingTypeDescText = this.Driver.GetText(this.getMeetingTypeDesc);
            Verify.That(this.DriverContext, () => Assert.AreEqual(meetingTypeDesc, actualMeetingTypeDescText, "verifying that Meeting Type " + meetingType + " is displayed successfuly at top along with Meeting Type Description field data" + meetingTypeDesc), "To Verify whether Meeting Type (" + meetingType + ") is displayed successfuly at top along with Meeting Type Description field data (" + meetingTypeDesc + " )", "Meeting Type (" + meetingType + ") is displayed successfuly at top along with Meeting Type Description field data (" + meetingTypeDesc + ")", "Expected Meeting Type Description (" + meetingTypeDesc + ") is not macthing with actual value : (" + actualMeetingTypeDescText + ")");

            string actualMeetingTypeStatus = this.Driver.GetText(this.getStatus);
            Verify.That(this.DriverContext, () => Assert.AreEqual(status, actualMeetingTypeStatus, "verifying that Meeting Type added " + meetingType + " is displayed successfuly at top along with Status" + status), "To Verify whether Meeting Type added (" + meetingType + ") is displayed successfuly at top along with Status (" + status + ")", "Meeting Type added (" + meetingType + ") is displayed successfuly at top along with Status (" + status + ")", "Expected Meeting Type Status (" + status + ") is not macthing with actual value : (" + actualMeetingTypeStatus + ")");

            Verify.That(this.DriverContext, () => Assert.AreEqual(status, actualMeetingTypeStatus, "verifying that Meeting Type " + meetingType + " is displayed successfuly at top along with Status" + status), "To Verify whether Meeting Type (" + meetingType + ") is displayed successfuly at top along with Status (" + status + ")", "Meeting Type (" + meetingType + ") is displayed successfuly at top along with Status (" + status + ")", "Expected Meeting Type Status (" + status + ") is not macthing with actual value : (" + actualMeetingTypeStatus + ")");
        }

#pragma warning disable SA1600 // Elements should be documented
        public string GetListofValuesMeetingType(string meetingType, string meetingTypeDesc, string isActiveStatus)
#pragma warning restore SA1600 // Elements should be documented
        {
            return meetingType + "," + meetingTypeDesc + "," + isActiveStatus;
        }

#pragma warning disable SA1600 // Elements should be documented
        public void GetMeetingTypeExistsInDB(string meetingType, string compareText, string message, List<string> columnnames, List<string> values)
#pragma warning restore SA1600 // Elements should be documented
        {
            this.Driver.GetSingleValueFromDBCompareWithExpectedValue(string.Format(SqlQuery.FunctionalAddMeetingType, meetingType), compareText, message);
            DataSet resultTable = SqlHelper.ExecuteSqlCommandQuery(string.Format(SqlQuery.FunctionalMeetingTypeDbValidation, meetingType));
            Dictionary<string, string> actualDict = SqlHelper.GetTableColumnValueInDictionary(resultTable.Tables[0], columnnames);
            Dictionary<string, string> expectedDict = SqlHelper.GenerateDictionaryfromLists(columnnames, values);
            Verify.CompareTwoDictionaryFromPageAndDBWithSoftAssertion(this.DriverContext, actualDict, expectedDict);
        }

#pragma warning disable SA1600 // Elements should be documented
        public void IsMeetingTypeNameRequiredErrorMsgSuccessfullyDisplayed(string errorMessage)
#pragma warning restore SA1600 // Elements should be documented
        {
            bool isAvailable = this.Driver.IsElementPresent(this.meetingTypeNameRequiredErrorMsg, BaseConfiguration.LongTimeout);

            if (isAvailable)
            {
                Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.meetingTypeNameRequiredErrorMsg, errorMessage, this.nmmeetingTypeNameRequiredErrorMsg);
                bool isHighlighted = Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.meetingTypeNameTextBoxHighlighted, this.nmmeetingTypeNameTextBoxHighlighted, BaseConfiguration.LongTimeout);

                if (isHighlighted)
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Meeting Type Name field is Highlighted", "Meeting Type Name field is Highlighted successfully");
                }
            }
        }

#pragma warning disable SA1600 // Elements should be documented
        public void IsMeetingTypeDescriptionRequiredErrorMsgSuccessfullyDisplayed(string errorMessage)
#pragma warning restore SA1600 // Elements should be documented
        {
            bool isAvailable = this.Driver.IsElementPresent(this.meetingTypeDescRequiredErrorMsg, BaseConfiguration.LongTimeout);

            if (isAvailable)
            {
                Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.meetingTypeDescRequiredErrorMsg, errorMessage, this.nmmeetingTypeDescRequiredErrorMsg);
                bool isHighlighted = Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.meetingTypeDescTextAreaHighlighted, this.nmmeetingTypeDescTextAreaHighlighted, BaseConfiguration.LongTimeout);

                if (isHighlighted)
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Description field is Highlighted", "Description field is Highlighted successfully");
                }
            }
        }

#pragma warning disable SA1600 // Elements should be documented
        public IList<IWebElement> GetAvailableMeetingTypeRecord()
#pragma warning restore SA1600 // Elements should be documented
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.meetingTypesExistingRecord, this.nmmeetingTypesExistingRecordItems, BaseConfiguration.LongTimeout);
            IList<IWebElement> lstElements = this.Driver.GetElements(this.meetingTypesExistingRecord);
            return lstElements;
        }

#pragma warning disable SA1600 // Elements should be documented
        public void EnterExistingMeetingType(IList<IWebElement> existingMeetingDetails)
#pragma warning restore SA1600 // Elements should be documented
        {
            string meetingType = existingMeetingDetails[0].Text;
            string description = existingMeetingDetails[1].Text;
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.meetingType, meetingType, this.nmmeetingTypetxt);
        }

#pragma warning disable SA1600 // Elements should be documented
        public void IsMeetingTypeNameAlreadyExistsErrorMsgSuccessfullyDisplayed(string errorMessage)
#pragma warning restore SA1600 // Elements should be documented
        {
            bool isAvailable = this.Driver.IsElementPresent(this.meetingTypeAlreadyExistsErrorMsg, BaseConfiguration.LongTimeout);

            if (isAvailable)
            {
                Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.meetingTypeAlreadyExistsErrorMsg, errorMessage, this.nmmeetingTypeAlreadyExistsErrorMsg);
                bool isHighlighted = Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.meetingTypeNameTextBoxHighlighted, this.nmmeetingTypeNameTextBoxHighlighted, BaseConfiguration.LongTimeout);

                if (isHighlighted)
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Meeting Type Name field is Highlighted", "Meeting Type Name field is Highlighted successfully");
                }
            }
        }

#pragma warning disable SA1600 // Elements should be documented

        public void IsUserIsAbleToClickOnMeetingType(string index)
#pragma warning restore SA1600 // Elements should be documented
        {
            this.Driver.WaitUntilElementIsFound(this.selectedMeetingType.Format(index), BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.selectedMeetingType.Format(index));
            this.Driver.JavaScriptClick(webElement, this.nmmeetingTypelink);
            this.Driver.WaitForPageLoad();
        }

#pragma warning disable SA1600 // Elements should be documented
        public void IsEditMeetingTypePopupVisible()
#pragma warning restore SA1600 // Elements should be documented
        {
            this.Driver.WaitUntilElementIsFound(this.editmeetingTypePopup, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.editmeetingTypePopup, this.nmeditMeetingTypePopup);
        }

#pragma warning disable SA1600 // Elements should be documented
        public void IsEditButtonMeetingTypeClickable()
#pragma warning restore SA1600 // Elements should be documented
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            this.Driver.WaitUntilElementIsFound(this.editButton, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.editButton, this.nmEditButton);
            this.Driver.IsElementClickable(this.editButton, this.nmEditButton);
        }

#pragma warning disable SA1600 // Elements should be documented
        public void IsMeetingTypeStatusInactiveClickInactiveIfActive()
#pragma warning restore SA1600 // Elements should be documented
        {
            bool inactiveStatusChecked = this.Driver.IsElementPresent(this.meetingTypeStatusInactive, this.nmStatusInactive, BaseConfiguration.MediumTimeout);
            if (inactiveStatusChecked)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify if Status of Meeting Type is already Inactive", "Status of Meeting Type is Inactive");
                Logger.Info("Status of Meeting Type is Inactive");
            }
            else
            {
                Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.meetingTypeStatusInActive, this.nmStatusInActive);
                Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.meetingTypeStatusInActive, this.nmStatusInActive);
            }
        }
    }
}
