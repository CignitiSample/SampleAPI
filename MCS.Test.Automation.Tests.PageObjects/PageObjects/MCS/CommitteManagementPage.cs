// <copyright file="CommitteManagementPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
    using System;
    using System.Collections.Generic;
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Helpers;
    using global::MCS.Test.Automation.Common.Types;
    using global::MCS.Test.Automation.Tests.PageObjects;
    using global::NUnit.Framework;
    using NLog;
    using OpenQA.Selenium;
    using RelevantCodes.ExtentReports;

    public class CommitteManagementPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ElementLocator
    customerLogo = new ElementLocator(Locator.CssSelector, "img.ui.image");

        private readonly ElementLocator
    membershipmenu = new ElementLocator(Locator.CssSelector, "i.address.card.icon");

        private readonly ElementLocator
    committeemanagement = new ElementLocator(Locator.CssSelector, "i.users.icon");

        private readonly ElementLocator
            applicationManagement = new ElementLocator(Locator.CssSelector, "i.cog.icon");

        private readonly ElementLocator
            managemembershiptype = new ElementLocator(Locator.CssSelector, "ul > li:nth-child(1)");

        private readonly ElementLocator
            manageMembershipFAQs = new ElementLocator(Locator.CssSelector, "ul > li:nth-child(2)");

        private readonly ElementLocator
            manageMembershipDocuments = new ElementLocator(Locator.CssSelector, "ul > li:nth-child(3)");

        private readonly ElementLocator
            manageMemberClassifications = new ElementLocator(Locator.CssSelector, "ul > li:nth-child(4)");

        private readonly ElementLocator
           committeManagementSectionOptions = new ElementLocator(Locator.CssSelector, "div.menuWrapper ul.subMenu li");

        private readonly ElementLocator
          loadbutton = new ElementLocator(Locator.XPath, "//*[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
          committeeManagementmenu = new ElementLocator(Locator.XPath, "//div[@class='menuWrapper']//a[text()='Committee Management']");

        private readonly ElementLocator
          selectedCommitteeAccount = new ElementLocator(Locator.XPath, "//table[@id='customGrid']//..//tr[{0}]//a[@class='column--CommitteeDesignation']");

        private readonly ElementLocator
         tabCommuncationLog = new ElementLocator(Locator.XPath, "//a[@class='item'][contains(.,'Communication Log')]");

        private readonly ElementLocator
            tabMeetings = new ElementLocator(Locator.XPath, "//a[@class='item'][contains(.,'Meetings')]");

        private readonly ElementLocator
       inputCommuncationLog = new ElementLocator(Locator.XPath, "//div[@contenteditable='true']");

        private readonly ElementLocator
      dimmerVisible = new ElementLocator(Locator.XPath, "//div[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
        tagusersList = new ElementLocator(Locator.XPath, "//div[@class='tribute-container']");

        private readonly ElementLocator
        selectTagUser = new ElementLocator(Locator.XPath, "//div[@class='tribute-container']//li[{0}]");



        private readonly ElementLocator
             getIndexLogText = new ElementLocator(Locator.XPath, "//div[@class='content']//div[@class='text']");

        private readonly ElementLocator
          updateSuccessMessage = new ElementLocator(Locator.CssSelector, "div.content > p");

        private readonly ElementLocator
        submitBtnCommuncationLog = new ElementLocator(Locator.XPath, "//button[@class='ui primary button mr10']");

        private readonly ElementLocator
       editCommuncationLog = new ElementLocator(Locator.XPath, "(//i[contains(@class,'pencil icon')])[1]");

        private readonly ElementLocator
      editinputCommuncationLog = new ElementLocator(Locator.XPath, "(//div[@class='commentposteditor'])[2]");

        private readonly ElementLocator
        editsubmitBtnCommuncationLog = new ElementLocator(Locator.XPath, "//button[@type='submit']");

        private readonly ElementLocator
        editSubmitButton = new ElementLocator(Locator.XPath, "//span[@class='update-cancel']//button[@class='ui primary button mr10']");

        private readonly ElementLocator
       removeCommuncationLog = new ElementLocator(Locator.XPath, "(//i[contains(@class,'delete icon')])[1]");

        private readonly ElementLocator
      btnCommunationLogPopupMessage = new ElementLocator(Locator.XPath, "(//div[@class='content'])[10]");

        private readonly ElementLocator
       communcationlogPopupOkButtron = new ElementLocator(Locator.XPath, "(//button[@class='ui primary button'])[2]");

        private readonly ElementLocator
             getLogFirstIndex = new ElementLocator(Locator.XPath, "//div[@class='content']//div[@class='text'][{0}]");

        private readonly ElementLocator
  communicationLogUser = new ElementLocator(Locator.XPath, "(//a[@class='author'])[{0}]");

        private readonly ElementLocator
                    communicationLogDateTime = new ElementLocator(Locator.XPath, "(//div[@class='metadata']/div)[{0}]");

        private readonly ElementLocator meetingSequenceHeader = new ElementLocator(Locator.XPath, "//h5[text()='Meeting Sequence']");

        private readonly ElementLocator
         addMeetingSequenceBtn = new ElementLocator(Locator.XPath, "//button[text()='Add Meeting Sequence']");

        private readonly ElementLocator
        addMeetingSequencePopupHeader = new ElementLocator(Locator.XPath, "//div[text()='ADD MEETING SEQUENCE ']");

        private readonly ElementLocator
          meetingTypeDropdownListIcon = new ElementLocator(Locator.XPath, "//label[text()='Meeting Type']//..//div[@class='ui selection dropdown']//i");

        private readonly ElementLocator
            meetingTypeDrpDwnListOptions = new ElementLocator(Locator.XPath, "//label[text()='Meeting Type']//..//span[@class='text']");

        private readonly ElementLocator
           selectMeetingTypeInMeetingTypeDrpDwn = new ElementLocator(Locator.XPath, "//label[text()='Meeting Type']//..//span[text()='{0}']");

        private readonly ElementLocator meetingTab = new ElementLocator(Locator.XPath, "//a[contains(text(),'Meetings')]");
        private readonly ElementLocator addMeetingSequence = new ElementLocator(Locator.XPath, "//button[@class='ui secondary button']");
        private readonly ElementLocator monthLabel = new ElementLocator(Locator.XPath, "//label[@class='label' and text()='Month']");
        private readonly ElementLocator meetingTypeLabel = new ElementLocator(Locator.XPath, "//label[@class='label' and text()='Meeting Type']");
        private readonly ElementLocator monthDropDown = new ElementLocator(Locator.XPath, "//label[text()='Month']//..//i[@class='dropdown icon']");
        private readonly ElementLocator monthSelection = new ElementLocator(Locator.XPath, "//div[@class='ui active visible selection dropdown']//span[text()='{0}']");
        private readonly ElementLocator meetingTypeDropDown = new ElementLocator(Locator.XPath, "//label[text()='Meeting Type']//..//i[@class='dropdown icon']");
        private readonly ElementLocator meetingTypeSelection = new ElementLocator(Locator.XPath, "//div[@class='ui active visible selection dropdown']//span[text()='{0}']");
        private readonly ElementLocator saveButton = new ElementLocator(Locator.XPath, "//button[contains(@type,'submit')]");
        private readonly ElementLocator addMeetingSequenceMonthColumn = new ElementLocator(Locator.XPath, "//table[@class='customTable']//th[text()='Month']");
        private readonly ElementLocator addMeetingTypeSequenceColumn = new ElementLocator(Locator.XPath, "//table[@class='customTable']//th[text()='Meeting Type']");
        private readonly ElementLocator deleteIcon = new ElementLocator(Locator.XPath, "(//a[@title='Delete'])[1]");
        private readonly ElementLocator reasonForUpdate = new ElementLocator(Locator.XPath, "//textarea[contains(@placeholder,'Please enter reason for update')]");
        private readonly ElementLocator reasonForUpdateYesButton = new ElementLocator(Locator.XPath, "//button[@type='submit']");
        private readonly ElementLocator newlyAddedMonth = new ElementLocator(Locator.XPath, "(//td[text()='January'])[1]");
        private readonly ElementLocator newlyAddedMeetingType = new ElementLocator(Locator.XPath, "(//td[text()='Committee Week'])[1]");
        private readonly ElementLocator errormessageMonth = new ElementLocator(Locator.XPath, "//span[@class='errorMessage' and text()='Please select Month.']");
        private readonly ElementLocator errormessageMeetingType = new ElementLocator(Locator.XPath, "//span[@class='errorMessage' and text()='Please select Meeting Type.']");
        private readonly ElementLocator addMeetingDate = new ElementLocator(Locator.XPath, "//button[@class='ui secondary button floatRight']");
        private readonly ElementLocator meetingDate = new ElementLocator(Locator.XPath, "//input[@name='MeetingDate']");
        private readonly ElementLocator meetingDateSaveButton = new ElementLocator(Locator.XPath, "//button[@type='submit']");
        private readonly ElementLocator meetingDateColumn = new ElementLocator(Locator.XPath, "//div[@class='meetingDatesWrap']//table[@class='customTable']//th[text()='Meeting Dates']");
        private readonly ElementLocator meetingDateStatusColumn = new ElementLocator(Locator.XPath, "//div[@class='meetingDatesWrap']//table[@class='customTable']//th[text()='Status']");
        private readonly ElementLocator meetingDateStatusActiveField = new ElementLocator(Locator.XPath, "(//div[@class='meetingDatesWrap']//table[@class='customTable']//td[2])[1]");
        private readonly ElementLocator meetinDateEditButton = new ElementLocator(Locator.XPath, "(//section[@class='meetingDates clearfix']//a[@class='editBtn'])[1]");
        private readonly ElementLocator meetingDateInactiveButton = new ElementLocator(Locator.XPath, "//label[contains(.,'Inactive')]");
        private readonly ElementLocator meetinDateReason = new ElementLocator(Locator.XPath, "//textarea[@placeholder='Please enter reason for update']");
        private readonly ElementLocator meetingDateUpdateButton = new ElementLocator(Locator.XPath, "//button[@type='submit']");
        private readonly ElementLocator meetingDateReasonError = new ElementLocator(Locator.XPath, "//span[@class='errorMessage'][contains(.,'Please enter reason for update.')]");
        private readonly ElementLocator meetingDateError = new ElementLocator(Locator.XPath, "//span[@class='errorMessage'][contains(.,'Meeting Date is required.')]");
        private readonly ElementLocator countSelectedMeetingType = new ElementLocator(Locator.XPath, "//div[@class='meetingSequenceWrap']//td[text()='{0}']");
        private readonly ElementLocator countSelectedMeetingDate = new ElementLocator(Locator.XPath, "//div[@class='meetingDatesWrap']//td[text()='{0}']");

        private string nmcountSelectedMeetingType = "Selected Meeting Type";
        private string nmmeetingTab = "Meeting Tab";
        private string nmaddMeetingSequence = "Add Meeting Sequence";
        private string nmmonthLabel = "Month Label";
        private string nmmeetingTypeLabel = "Meeting Type Label";
        private string nmmonthDropDown = "Month Drop Down Icon";
        private string nmmonthSelection = "{0} Month DropDown";
        private string nmmeetingTypeDropDown = "Meeting Type Drop Down Icon";
        private string nmmeetingTypeSelection = "{0} Meeting Type DropDown";
        private string nmsaveButton = "Save Button";
        private string nmaddMeetingSequenceMonthColumn = "Add Meeting Sequence Month Column";
        private string nmaddMeetingTypeSequenceMonthColumn = "Add Meeting Type Sequence Month Column";
        private string nmdeleteIcon = "Delete meeting Sequence";
        private string nmreasonForUpdate = "Reason For Update Input";
        private string nmreasonForUpdateYesButton = "Reason for Update Yes Button";
        private string nmnewlyAddedMonth = "Newly Added Month {0}";
        private string nmerrormessageMonth = "Error Message On Month";
        private string nmerrormessageMeetingType = "Error Message On Meeting Type";
        private string nmaddMeetingDate = "Add meeting Date";
        private string nmmeetingDate = "Input Meeting Date";
        private string nmmeetingDateSaveButton = "Add Meeting Date Save Button ";
        private string nmmeetingDateColumn = "Meeting Date Column";
        private string nmmeetingDateStatusColumn = "Meeging Date Status Column";
        private string nmmeetingDateStatusActiveField = "Active in Status Column";
        private string nmmeetinDateEditButton = "Meeting Date Edit Button";
        private string nmmeetingDateInactiveButton = "Meeting Date Inactive Check Box";
        private string nmmeetinDateReason = "Meeting Date Reason Input";
        private string nmmeetingDateUpdateButton = "Meeting Date Update Button ";
        private string nmmeetingDateReasonError = "Error Meeting Date Reason";
        private string nmmeetingDateError = "Error Meeting Date";

        private string nmmanageOfficerTitle = "Manage Officer Titles";
        private string nmmanageCommitteType = "Manage Committee Types";
        private string nmcommitteeManagement = "Committee Management Menu";
        private string nmCommitteeAccountlink = "Committee link";
        private string nmtabCommuncationLog = "Communication Log Tab";
        private string nminputCommuncationLog = "Communcation Log Input";
        private string nmTagUsersList = "Tag users List";
        private string nmupdateMessageCommunicationLog = "Communication Log Message";
        private string nmremoveCommuncationLog = "Delete icon for Communication Log";
        private string nmtaguser = "Communication Log - Selected Tag User ({0}) ";
        private string nmsubmitBtnCommuncationLog = "Submit Btn Communcation Log";
        private string nmEditButtonCommuncationLog = "Edit Button Communcation Log ";
        private string nmEditinputCommuncationLog = "Edit Communcation Log Input";
        private string nmbtnCommunationLogPopupMessage = "Communcation Log Popup Message";
        private string nmcommuncationlogPopupOkButtron = "communcationlog Popup Ok Button";
        private string nmdimmerloading = "Dimmer loading";
        private string nmcommunicationLogUser = "Communication Log User Name";
        private string nmlogText = "log Text  : {0}";
        private string nmcommunicationLogDateTime = "Communication Log DateTime";
        private string nmtabMeetings = "Meetings Tab";
        private string nmmeetingSequenceHeader = "Meeting Sequence header";
        private string nmAddMeetingSequenceBtn = "Add Meeting Sequence button";
        private string nmaddMeetingSequencePopupHeader = "Add Meeting Sequence popup header";
        private string nmmeetingTypeDropdownListIcon = "Meeting Type Drop down List Icon";
        private string nmnewlyAddedMeetingType = "Newly Added Meeting Type";
        private string nmupdateAddMeetingSequenceLog = "Add Meeting Sequence Message";
        private string nmupdateDeleteMeetingSequenceLog = "Delete Meeting Sequence Message";
        private string nmupdateAddMeetingDateLog = "Add Meeting Date Message";
        private string nmupdateUpdateMeetingDateLog = "Update Meeting Date Message";

        public CommitteManagementPage(DriverContext driverContext)
        : base(driverContext)
        {
        }

        public void GetMeetingTypeExistsInDB(string commiteename, string meetingWeek, string compareText, string message)
        {
            this.Driver.GetSingleValueFromDBCompareWithExpectedValue(string.Format(SqlQuery.FunctionalNewMeetingTypeDbValidation, commiteename, meetingWeek), compareText, message);
        }

        public void GetMeetingDateExistsInDB(string commiteename, string commiteeDate, string compareText, string message)
        {
            this.Driver.GetSingleValueFromDBCompareWithExpectedValue(string.Format(SqlQuery.FunctionalNewMeetingDateDbValidation, commiteename, commiteeDate), compareText, message);
        }

        public void IscommunicationLogUserNameVisibleByIndex(string index)
        {
            this.Driver.IsElementVisible(this.communicationLogUser.Format(index), this.nmcommunicationLogUser);
        }

        public void IscommunicationLogDescriptionVisible(string expected, int index)
        {
            string text = this.Driver.GetTextForSelectedElementfromList(this.getIndexLogText, string.Format(this.nmlogText, expected), index);
            Assert.AreEqual(expected, text, "Expected text is not matching with return text from element");
        }

        public void IscommunicationLogDateTimeVisibleByIndex(string index)
        {
            this.Driver.IsElementVisible(this.communicationLogDateTime.Format(index), this.nmcommunicationLogDateTime);
        }

        public void IsUserIsAbleToClickcommuncationlogPopupOkButtron()
        {
            this.Driver.WaitUntilElementIsFound(this.communcationlogPopupOkButtron, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.communcationlogPopupOkButtron);
            this.Driver.JavaScriptClick(webElement, this.nmcommuncationlogPopupOkButtron);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserAbletoViewMessageOnCommuncationLogPOpupSoftAssertion(string updateSuccessMessage)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.btnCommunationLogPopupMessage, this.nmbtnCommunationLogPopupMessage, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.btnCommunationLogPopupMessage, this.nmbtnCommunationLogPopupMessage);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.btnCommunationLogPopupMessage, updateSuccessMessage, this.nmbtnCommunationLogPopupMessage);
        }

        public void IsUserIsAbleToClickRemoveCommunicationLog()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            var by = this.removeCommuncationLog.ToBy();
            var webElement = this.Driver.FindElement(by);
            this.Driver.JavaScriptClick(webElement, this.nmremoveCommuncationLog);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserAbleToViewCommuncationLog(string updateSuccessMessage)
        {
            this.Driver.IsElementVisible(this.updateSuccessMessage, this.nmupdateMessageCommunicationLog);
            this.Driver.IsExpectedTextMatchWithActualText(this.updateSuccessMessage, updateSuccessMessage, string.Format(this.nmupdateMessageCommunicationLog, updateSuccessMessage));
            this.Driver.WaitUntilElementIsNoLongerFound(this.updateSuccessMessage, BaseConfiguration.LongTimeout, string.Format(this.nmupdateMessageCommunicationLog, updateSuccessMessage));
            this.Driver.WaitForPageLoad();
        }

        public void IsUserIsAbleToClickEditCommunicationLogSubmitButtron()
        {
            this.Driver.WaitUntilElementIsFound(this.editSubmitButton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.editSubmitButton);
            this.Driver.JavaScriptClick(webElement, this.nmsubmitBtnCommuncationLog);
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
        }

        public void IsUserAbleToEnterTextInEditCommuncationLog(string communicationLog)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            this.Driver.WaitUntilElementIsFound(this.editinputCommuncationLog, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.editinputCommuncationLog, this.nmEditinputCommuncationLog);
            this.Driver.EnterText(this.editinputCommuncationLog, communicationLog, this.nmEditinputCommuncationLog);
        }

        public void IsUserIsAbleToClickEditCommunicationLog()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            var by = this.editCommuncationLog.ToBy();
            var webElement = this.Driver.FindElement(by);
            this.Driver.JavaScriptClick(webElement, this.nmEditButtonCommuncationLog);
            this.Driver.WaitForPageLoad();
        }

        public void GetEditedCommuncationLogExistsInDBSoftAssertion(string communcationLogText, string compareText, string message)
        {
            Verify.GetSingleValueFromDBCompareWithExpectedValue(this.DriverContext, string.Format(SqlQuery.FunctionalEditedCommitteManagementCommunicationExists, communcationLogText), compareText, message);
        }

        public void IscommunicationLogDescriptionVisibleSoftAssertion(string expected, int index)
        {
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.getLogFirstIndex.Format(index), expected);
        }

        public void IsUserAbleToViewCommuncationLogMessageSoftAssertion(string updateSuccessMessage)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateMessageCommunicationLog);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, updateSuccessMessage, string.Format(this.nmupdateMessageCommunicationLog, updateSuccessMessage));
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, string.Format(this.nmupdateMessageCommunicationLog, updateSuccessMessage), BaseConfiguration.LongTimeout);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserIsAbleToClickCommunicationLogSubmitButtron()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            var webElement = this.Driver.GetElement(this.submitBtnCommuncationLog);
            this.Driver.JavaScriptClick(webElement, this.nmsubmitBtnCommuncationLog);
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
        }

        public string InputTagUserinCommunicationLogInput(string index)
        {
            this.Driver.WaitUntilElementIsFound(this.selectTagUser.Format(index), BaseConfiguration.LongTimeout);
            string getusername = this.Driver.GetText(this.selectTagUser.Format(index));
            this.Driver.IsElementClickableWithSoftAssertion(this.selectTagUser.Format(index), string.Format(this.nmtaguser, getusername));
            this.Driver.WaitForPageLoad();
            return getusername;
        }

        public void VerifyTagUserListOnTypingAtSoftAssertion()
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.tagusersList, this.nmTagUsersList, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.tagusersList, this.nmTagUsersList);
        }

        public void IsUserAbleToEnterTextInCommuncationLog(string communicationLog)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            this.Driver.WaitUntilElementIsFound(this.inputCommuncationLog, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.inputCommuncationLog, this.nminputCommuncationLog);
            this.Driver.EnterText(this.inputCommuncationLog, communicationLog, this.nminputCommuncationLog);
        }

        public void IsCommiteeManagementSectionClickable()
        {
            this.Driver.IsElementVisible(this.committeeManagementmenu, this.nmcommitteeManagement);
            var webElement = this.Driver.GetElement(this.committeeManagementmenu);
            this.Driver.JavaScriptClick(webElement, this.nmcommitteeManagement);
            this.Driver.MouseOverOnWebElement(this.committeeManagementmenu);
        }

        public void IsManageOffierTitlesClickable(string manageOfficerTitle)
        {
            this.Driver.AreElementsVisible(this.committeManagementSectionOptions.Format(manageOfficerTitle), this.nmmanageOfficerTitle);
            this.Driver.IsElementClickableFromListofElementWithText(this.committeManagementSectionOptions.Format(manageOfficerTitle), this.nmmanageOfficerTitle);
        }

        public void IsManageCommitteTypeClickable(string manageCommitteType)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadbutton, 90, this.nmmanageCommitteType);
            this.Driver.AreElementsVisible(this.committeManagementSectionOptions.Format(manageCommitteType), this.nmmanageCommitteType);
            this.Driver.IsElementClickableFromListofElementWithText(this.committeManagementSectionOptions, manageCommitteType);
        }

        public void IsUserIsAbleToClickOnAccountNumber(string index)
        {
            this.Driver.WaitUntilElementIsFound(this.selectedCommitteeAccount.Format(index), BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.selectedCommitteeAccount.Format(index));
            this.Driver.JavaScriptClick(webElement, this.nmCommitteeAccountlink);
            this.Driver.WaitForPageLoad();
        }

        public string IsUserIsAbleToGetAccountNumber(string index)
        {
            this.Driver.WaitUntilElementIsFound(this.selectedCommitteeAccount.Format(index), BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.selectedCommitteeAccount.Format(index));
            return webElement.Text;
        }

        public void IsUserIsAbleToClickCommunicationLogTab()
        {
            this.Driver.WaitUntilElementIsFound(this.tabCommuncationLog, BaseConfiguration.LongTimeout);
            this.Driver.CheckElementPresentOrNot(this.tabCommuncationLog, this.nmtabCommuncationLog, string.Empty);
            var webElement = this.Driver.GetElement(this.tabCommuncationLog);
            this.Driver.JavaScriptClick(webElement, this.nmtabCommuncationLog);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserIsAbleToClickMeetingsTab()
        {
            this.Driver.WaitUntilElementIsFound(this.tabMeetings, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.tabMeetings, this.nmtabMeetings);
            var webElement = this.Driver.GetElement(this.tabMeetings);
            this.Driver.JavaScriptClick(webElement, this.nmtabMeetings);
        }

        public void IsMeetingSequenceHeaderDisplayed(string header)
        {
            var text = this.Driver.GetText(this.meetingSequenceHeader);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.meetingSequenceHeader.Format(header), this.nmmeetingSequenceHeader);
        }

        public void IsUserIsAbleToClickOnAddMeetingSequenceButton()
        {
            this.Driver.WaitUntilElementIsFound(this.addMeetingSequenceBtn, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.addMeetingSequenceBtn);
            this.Driver.JavaScriptClick(webElement, this.nmAddMeetingSequenceBtn);
        }

        public void IsAddMeetingSequencePopupHeaderVisible()
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.addMeetingSequencePopupHeader, this.nmaddMeetingSequencePopupHeader, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.addMeetingSequencePopupHeader, this.nmaddMeetingSequencePopupHeader);
        }

        public void IsUserIsAbleToClickOnMeetingTypeDropDownList()
        {
            this.Driver.WaitUntilElementIsFound(this.meetingTypeDropdownListIcon, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.meetingTypeDropdownListIcon);
            this.Driver.JavaScriptClick(webElement, this.nmmeetingTypeDropdownListIcon);
        }

        public void ValidateMeetingTypeDrpDwnOptionsHoldsNewlyAddedMeetingTypeFromRnE(string newlyAddedMeetingType)
        {
            try
            {
                this.Driver.WaitUntilElementIsFound(this.meetingTypeDrpDwnListOptions, BaseConfiguration.LongTimeout);

                IList<IWebElement> itemsMeetingTypes = this.Driver.GetElements(this.meetingTypeDrpDwnListOptions);
                IList<string> meetingTypesText = new List<string>();
                foreach (IWebElement i in itemsMeetingTypes)
                {
                    meetingTypesText.Add(i.Text);
                }

                if (meetingTypesText.Contains(newlyAddedMeetingType))
                {
                    Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.selectMeetingTypeInMeetingTypeDrpDwn.Format(newlyAddedMeetingType), this.nmnewlyAddedMeetingType);

                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify Meeting Type added (" + newlyAddedMeetingType + ") in Rules and Exception reflected on Add Meeting Sequence Popup while adding a Meeting Sequence for a Committee", "Meeting Type added (" + newlyAddedMeetingType + ") in Rules and Exception reflected on Add Meeting Sequence Popup while adding a Meeting Sequence for a Committee");
                    Logger.Info("Meeting Type added in Rules and Exception reflected on Add Meeting Sequence Popup while adding a Meeting Sequence for a Committee");
                }
                else
                {
                    Assert.IsFalse(true);
                    DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify Meeting Type added (" + newlyAddedMeetingType + ") in Rules and Exception reflected on Add Meeting Sequence Popup while adding a Meeting Sequence for a Committee", "Meeting Type added (" + newlyAddedMeetingType + ") in Rules and Exception is not reflected on Add Meeting Sequence Popup while adding a Meeting Sequence for a Committee");
                    Logger.Info("Meeting Type added in Rules and Exception is not reflected on Add Meeting Sequence Popup while adding a Meeting Sequence for a Committee");
                }
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify Meeting Type added in Rules and Exception reflected on Add Meeting Sequence Popup while adding a Meeting Sequence for a Committee ", "Meeting Type added in Rules and Exception is not reflected on Add Meeting Sequence Popup while adding a Meeting Sequence for a Committee");
                Logger.Error("Meeting Type added in Rules and Exception is not reflected on Add Meeting Sequence Popup while adding a Meeting Sequence for a Committee");
                throw new Exception("Meeting Type added in Rules and Exception is not reflected on Add Meeting Sequence Popup while adding a Meeting Sequence for a Committee");
            }
        }

        public void IsUserAbleToViewMonthLabel()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.monthLabel, this.nmmonthLabel);
        }

        public void IsUserAbleToViewMeetingTypeLabel()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.meetingTypeLabel, this.nmmeetingTypeLabel);
        }

        public void IsUserAbleToClickMonthDropDownIcon()
        {
            this.Driver.WaitUntilElementIsFound(this.monthDropDown, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.monthDropDown);
            this.Driver.IsElementVisible(this.monthDropDown, this.nmmonthDropDown);
            this.Driver.JavaScriptClick(webElement, this.nmmonthDropDown);
        }

        public void IsUserAbleToSelectMonthInDropdown(string name)
        {
            this.Driver.IsElementVisible(this.monthSelection.Format(name), string.Format(this.nmmonthSelection, name + " in "));
            var webElement = this.Driver.GetElement(this.monthSelection.Format(name));
            this.Driver.JavaScriptClick(webElement, string.Format(this.nmmonthSelection, name + " in "));
        }

        public void IsUserAbleToClickMeetingTypeDropDownIcon()
        {
            this.Driver.WaitUntilElementIsFound(this.meetingTypeDropDown, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.meetingTypeDropDown);
            this.Driver.IsElementVisible(this.meetingTypeDropDown, this.nmmeetingTypeDropDown);
            this.Driver.JavaScriptClick(webElement, this.nmmeetingTypeDropDown);
        }

        public void IsUserAbleToSelectMeetingTypeInDropdown(string name)
        {
            this.Driver.IsElementVisible(this.meetingTypeSelection.Format(name), string.Format(this.nmmeetingTypeSelection, name + " in "));
            var webElement = this.Driver.GetElement(this.meetingTypeSelection.Format(name));
            this.Driver.JavaScriptClick(webElement, string.Format(this.nmmeetingTypeSelection, name + " in "));
        }

        public void IsUserAbletoClickSaveButton()
        {
            this.Driver.WaitUntilElementIsFound(this.saveButton, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.saveButton, this.nmsaveButton);
            this.Driver.IsElementClickable(this.saveButton, this.nmsaveButton);
        }

        public void IsUserAbleToViewAddMeetingSequenceMonthColumn()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.addMeetingSequenceMonthColumn, this.nmaddMeetingSequenceMonthColumn);
        }

        public void IsUserAbleToViewAddMeetingTypeSequenceColumn()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.addMeetingTypeSequenceColumn, this.nmaddMeetingTypeSequenceMonthColumn);
        }

        public void IsUserAbleToViewMonthMeetingTypeColumn()
        {
            this.IsUserAbleToViewAddMeetingSequenceMonthColumn();
            this.IsUserAbleToViewAddMeetingTypeSequenceColumn();
        }

        public void IsUserAbleToClickOnDeleteIcon()
        {
            this.Driver.WaitUntilElementIsFound(this.deleteIcon, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.deleteIcon);
            this.Driver.IsElementVisible(this.deleteIcon, this.nmdeleteIcon);
            this.Driver.JavaScriptClick(webElement, this.nmdeleteIcon);
        }

        public void IsUserAbletoEnterTextInReasonForUpdateInput(string enterText)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.reasonForUpdate, this.nmreasonForUpdate);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.reasonForUpdate, enterText, this.nmreasonForUpdate);
        }

        public void IsUserAbletoClickReasonForUpdateYesButton()
        {
            this.Driver.IsElementVisible(this.reasonForUpdateYesButton, this.nmreasonForUpdateYesButton);
            this.Driver.IsElementClickable(this.reasonForUpdateYesButton, this.nmreasonForUpdateYesButton);
        }

        public void IsUserAbleToViewAddMeetingSequenceMessageSoftAssertion(string updateSuccessMessage)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateMessageCommunicationLog, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateMessageCommunicationLog);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, updateSuccessMessage, string.Format(this.nmupdateAddMeetingSequenceLog, updateSuccessMessage));
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, string.Format(this.nmupdateAddMeetingSequenceLog, updateSuccessMessage), BaseConfiguration.LongTimeout);
        }

        public void IsUserAbleToViewDeleteMeetingSequenceMessageSoftAssertion(string updateSuccessMessage)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateDeleteMeetingSequenceLog, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateDeleteMeetingSequenceLog);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, updateSuccessMessage, string.Format(this.nmupdateDeleteMeetingSequenceLog, updateSuccessMessage));
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, string.Format(this.nmupdateDeleteMeetingSequenceLog, updateSuccessMessage), BaseConfiguration.LongTimeout);
        }

        public void IsUserAbleToViewAddMeetingDateMessage(string updateSuccessMessage)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateAddMeetingDateLog, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateAddMeetingDateLog);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, updateSuccessMessage, string.Format(this.nmupdateAddMeetingDateLog, updateSuccessMessage));
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, string.Format(this.nmupdateAddMeetingDateLog, updateSuccessMessage), BaseConfiguration.LongTimeout);
        }

        public void IsUserAbleToViewUpdateMeetingDateMessage(string updateSuccessMessage)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateUpdateMeetingDateLog, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateUpdateMeetingDateLog);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, updateSuccessMessage, string.Format(this.nmupdateUpdateMeetingDateLog, updateSuccessMessage));
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, string.Format(this.nmupdateUpdateMeetingDateLog, updateSuccessMessage), BaseConfiguration.LongTimeout);
        }

        public void IsUserAbleToClickAddMeetingDate()
        {
            this.Driver.IsElementVisible(this.addMeetingDate, this.nmaddMeetingDate);
            this.Driver.IsElementClickable(this.addMeetingDate, this.nmaddMeetingDate);
        }

        public void IsUserAbletoEnterTextMeetingDate(string enterText)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.meetingDate, this.nmmeetingDate);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.meetingDate, enterText, this.nmmeetingDate);
        }

        public void IsUserAbleToClickAddMeetingDateSaveButton()
        {
            this.Driver.IsElementVisible(this.meetingDateSaveButton, this.nmmeetingDateSaveButton);
            this.Driver.IsElementClickable(this.meetingDateSaveButton, this.nmmeetingDateSaveButton);
        }

        public void IsUserAbleToViewMeetingDateAndStatusColumn()
        {
            this.IsUserAbleToViewMeetingDateColumn();
            this.IsUserAbleToViewMeetingDateStatusColumn();
        }

        public void IsUserAbleToViewMeetingDateColumn()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.meetingDateColumn, this.nmmeetingDateColumn);
        }

        public void IsUserAbleToViewMeetingDateStatusColumn()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.meetingDateStatusColumn, this.nmmeetingDateStatusColumn);
        }

        public void IsUserAbleToViewActiveInStatusColumn()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.meetingDateStatusActiveField, this.nmmeetingDateStatusActiveField);
        }

        public void IsUserAbleToClickMeetingDateEditButton()
        {
            this.Driver.WaitUntilElementIsFound(this.meetinDateEditButton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.meetinDateEditButton);
            this.Driver.IsElementVisible(this.meetinDateEditButton, this.nmmeetinDateEditButton);
            this.Driver.JavaScriptClick(webElement, this.nmmeetinDateEditButton);
        }

        public void IsUserAbleToClickMeetingDateInactiveCheckBox()
        {
            this.Driver.IsElementVisible(this.meetingDateInactiveButton, this.nmmeetingDateInactiveButton);
            this.Driver.IsElementClickable(this.meetingDateInactiveButton, this.nmmeetingDateInactiveButton);
        }

        public void IsUserAbletoEnterTextInMeetingDateReason(string enterText)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.meetinDateReason, this.nmmeetinDateReason);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.meetinDateReason, enterText, this.nmmeetinDateReason);
        }

        public void IsUserAbleToClickAddMeetingDatesSaveButton()
        {
            this.Driver.IsElementVisible(this.meetingDateUpdateButton, this.nmmeetingDateUpdateButton);
            this.Driver.IsElementClickable(this.meetingDateUpdateButton, this.nmmeetingDateUpdateButton);
        }

        public void IsUserAbleToViewErrorMessageOnMonth(string message)
        {
            Verify.IsElementVisibleFromListOfElementWithSoftAssertion(this.DriverContext, this.errormessageMonth, this.nmerrormessageMonth);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.errormessageMonth, message, this.nmerrormessageMonth);
        }

        public void IsUserAbleToViewErrorMessageOnMeetingType(string message)
        {
            Verify.IsElementVisibleFromListOfElementWithSoftAssertion(this.DriverContext, this.errormessageMeetingType, this.nmerrormessageMeetingType);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.errormessageMeetingType, message, this.nmerrormessageMeetingType);
        }

        public void IsUserAbletoViewErrorMessageOnReason(string message)
        {
            Verify.IsElementVisibleFromListOfElementWithSoftAssertion(this.DriverContext, this.meetingDateReasonError, this.nmmeetingDateReasonError);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.meetingDateReasonError, message, this.nmmeetingDateReasonError);
        }

        public void IsUserAbletoViewErrorMessageOnMeetingDate(string message)
        {
            Verify.IsElementVisibleFromListOfElementWithSoftAssertion(this.DriverContext, this.meetingDateError, this.nmmeetingDateError);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.meetingDateError, message, this.nmmeetingDateError);
        }

        public int IsUserAbleToGetCountOfMeetingType(string name)
        {
            var webElement = this.Driver.GetElements(this.countSelectedMeetingType.Format(name));
            return webElement.Count;
        }

        public int IsUserAbleToGetCountOfMeetingDate(string name)
        {
            var webElement = this.Driver.GetElements(this.countSelectedMeetingDate.Format(name));
            return webElement.Count;
        }
    }
}