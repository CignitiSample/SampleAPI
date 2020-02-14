// <copyright file="InternalStaffMembershipManagementPage.cs" company="PlaceholderCompany">
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
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Helpers;
    using global::MCS.Test.Automation.Common.Types;
    using global::MCS.Test.Automation.Tests.PageObjects;
    using NLog;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using RelevantCodes.ExtentReports;

    public class InternalStaffMembershipManagementPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator
           memberTypeDropdown = new ElementLocator(Locator.XPath, "//label[text()='Member Type']//..//div[@class='ui selection dropdown']//i");

        private readonly ElementLocator
         smallLoaderOnCardSection = new ElementLocator(Locator.XPath, "//div[contains(@class,'ui small text loader small-loader')]");

        private readonly ElementLocator
         memberTypeDropDownOptions = new ElementLocator(Locator.CssSelector, "div.visible.menu.transition div.item span.text");

        private readonly ElementLocator
         memberAccountLink = new ElementLocator(Locator.XPath, "//table[@id='customGrid']//..//tr[1]//td[1]//a[@class='column--Account']");

        private readonly ElementLocator
        editAstmGeneralInformation = new ElementLocator(Locator.XPath, "//h5[text()='ASTM General Information']//..//a[@class='editBtn']");

        private readonly ElementLocator
         graduationDateField = new ElementLocator(Locator.XPath, "//input[@name='GraduationDate']");

        private readonly ElementLocator
         updateAstmGeneralInformation = new ElementLocator(Locator.XPath, "//button[@title='Update']");

        private readonly ElementLocator
         viewStudentInfo = new ElementLocator(Locator.XPath, "//span[text()='View Student Information']");

        private readonly ElementLocator
         major = new ElementLocator(Locator.XPath, "//div[@class='column']//span[text()='{0}']");

        private readonly ElementLocator
         popupText = new ElementLocator(Locator.XPath, "//div[@class='description']//p[contains(text(),'{0}')]");

        private readonly ElementLocator
         memberAccountStatus = new ElementLocator(Locator.XPath, "//h5[text()='Member Account Status Details']//..//span[text()='{0}']");

        private readonly ElementLocator
         errorMsgReasonForUpdate = new ElementLocator(Locator.XPath, "//span[@class='errorMessage'][text()='Please enter reason for update.']");

        private readonly ElementLocator
         errorMsgHistoricalReason = new ElementLocator(Locator.XPath, "//span[@class='errorMessage'][text()='Historical Reason is required.']");

        private readonly ElementLocator
        errorMsgGraduationDate = new ElementLocator(Locator.XPath, "//span[@class='errorMessage'][text()='Graduation Date is required.']");

        private readonly ElementLocator
            highlightedMandatoryGraduationDate = new ElementLocator(Locator.XPath, "//label[text()='Graduation Date']/following::div[@class='field error'][1]");

        private readonly ElementLocator
            txtboxReasonForUpdate = new ElementLocator(Locator.XPath, "//textarea[@placeholder='Please enter reason for update']");

        private readonly ElementLocator
           txtboxAccountStatusReasonForUpdate = new ElementLocator(Locator.XPath, "//textarea[@placeholder='Please enter reason for update']");

        private readonly ElementLocator
           updateSuccessMessage = new ElementLocator(Locator.CssSelector, "div.content > p");

        private readonly ElementLocator
            committeesOfInterest = new ElementLocator(Locator.XPath, "//div[@name='InterestedCommittee']");

        private readonly ElementLocator
           universityTextField = new ElementLocator(Locator.XPath, "//input[@name='FacilityName']");

        private readonly ElementLocator
    incorrectDataGraduationError = new ElementLocator(Locator.XPath, "//span[@class='errorMessage'][text(column--Account')='Incorrect input.']");

        private readonly ElementLocator
                  majorTextField = new ElementLocator(Locator.XPath, "//input[@name='StudyField']");

        private readonly ElementLocator
         committeeOfInterestOptions = new ElementLocator(Locator.XPath, "//label[text()='Committees of Interest']/..//div[@role='option'][1]");

        private readonly ElementLocator
            existingCommitteesDeleteIcon = new ElementLocator(Locator.XPath, "//div[@name='InterestedCommittee']/a/i[@class='delete icon']");

        private readonly ElementLocator
           checkDataInCommittee = new ElementLocator(Locator.XPath, "//div[@name='InterestedCommittee']//a");

        private readonly ElementLocator
            committeeOfInterestInput = new ElementLocator(Locator.XPath, "//div[@name='InterestedCommittee']//input");

        private readonly ElementLocator
            selectNoOfItemsperPage = new ElementLocator(Locator.XPath, "//div[text()='25']/../i[@class='dropdown icon']");

        private readonly ElementLocator
            selectpageItemsCount = new ElementLocator(Locator.XPath, "//div[@class='visible menu transition']//span[text()='{0}']");

        private readonly ElementLocator
            defaultValueperPage = new ElementLocator(Locator.XPath, "//div[@class='itemPerPage']//div[@class='text']");

        private readonly ElementLocator
        accountStatusDropDown = new ElementLocator(Locator.XPath, "//label[text()='Account Status']//..//i[@class='dropdown icon']");

        private readonly ElementLocator
        memberTypeField = new ElementLocator(Locator.XPath, "//span[text()='Member Type']//..//span[text()='{0}']");

        private readonly ElementLocator
        memberTypeFieldlink = new ElementLocator(Locator.XPath, "//span[text()='Representative']//..//span[@class='labelValue']//a");

        private readonly ElementLocator
        organizationField = new ElementLocator(Locator.XPath, "//span[text()='Organization']//..//span[@class='labelValue']//a[contains(text(),'{0}')]");

        private readonly ElementLocator
        organizationlink = new ElementLocator(Locator.XPath, "//span[text()='Organization']//..//span[@class='labelValue']//a");

        private readonly ElementLocator
        accountStatusDropDownSelection = new ElementLocator(Locator.XPath, "//div[@class='ui active visible selection dropdown']//span[text()='{0}']");

        private readonly ElementLocator
        paidStatusDropDown = new ElementLocator(Locator.XPath, "//label[text()='Paid Status']//..//i[@class='dropdown icon']");

        private readonly ElementLocator
           accountStatusSelectActiveInDropDown = new ElementLocator(Locator.XPath, "//div[@class='visible menu transition']//span[text()='{0}']");

        private readonly ElementLocator
           okButtonInPopup = new ElementLocator(Locator.XPath, "//button[text()='Ok']");

        private readonly ElementLocator
           updateAccountStatus = new ElementLocator(Locator.XPath, "//div[@class='ui radio checkbox']//..//label[text()='{0}']");

        private readonly ElementLocator
           statusColumn = new ElementLocator(Locator.XPath, "//p[@class='column--Status']");

        private readonly ElementLocator
           memberdropdownInSearch = new ElementLocator(Locator.XPath, "//div[text()='Member']");

        private readonly ElementLocator
           searchtextfield = new ElementLocator(Locator.XPath, "//div[@class='ui input searchInput']//input");

        private readonly ElementLocator
           searchclick = new ElementLocator(Locator.XPath, "//i[@class='search icon']");

        private readonly ElementLocator
           errorMessageForNoData = new ElementLocator(Locator.XPath, "//div[@class='noRecordMessage']");

        private readonly ElementLocator
                         hearAboutTextField = new ElementLocator(Locator.XPath, "//input[@name = 'HearaboutStudentMembership']");

        private readonly ElementLocator
                  degreeSoughtOptions = new ElementLocator(Locator.XPath, "//label[text()='Degree Sought']//..//div[@role='listbox']//div[@role='option']//span[text()]");

        private readonly ElementLocator
                  mailingListOption = new ElementLocator(Locator.XPath, "//*[@class='ui radio checkbox']/label");

        private readonly ElementLocator
        radioWrap = new ElementLocator(Locator.XPath, "//*[@class='radioWrap']");

        private readonly ElementLocator
          degreeSoughtDropdownArrow = new ElementLocator(Locator.XPath, "//label[text()='Degree Sought']//..//div[@role='listbox']//..//i");

        private readonly ElementLocator
         mailingListLabel = new ElementLocator(Locator.XPath, "//label[text()='Mailing List']");

        private readonly ElementLocator
           majorTextFieldVerify = new ElementLocator(Locator.XPath, "//*[@name='StudyField']");

        private readonly ElementLocator
            memberTypeDropDownVerify = new ElementLocator(Locator.XPath, "//label[text()='Member Type']//..//div[@class='ui selection dropdown']");

        private readonly ElementLocator
                    majorLabelText = new ElementLocator(Locator.XPath, "//*[text()='Major']");

        private readonly ElementLocator
            memberTypeLabelText = new ElementLocator(Locator.XPath, "//*[text()='Member Type']");

        private readonly ElementLocator
               editMembershipTypeSection = new ElementLocator(Locator.XPath, "//h5[text()='Membership Type']//..//a[@class='editBtn']");

        private readonly ElementLocator
              editMemberAccountStatusDetailsSection = new ElementLocator(Locator.XPath, "//h5[text()='Member Account Status Details']//..//a[@class='editBtn']/i");

        private readonly ElementLocator
            copyButtonMemberDetails = new ElementLocator(Locator.XPath, "//button[text()='Copy']");

        private readonly ElementLocator
            memberTypeMemberDetails = new ElementLocator(Locator.XPath, "//label[text()='Member Type']//..//i");

        private readonly ElementLocator
         memberTypeMemberDetailsOptions = new ElementLocator(Locator.CssSelector, "div.visible.menu.transition div.item span.text");

        private readonly ElementLocator
         btnWordIcon = new ElementLocator(Locator.XPath, "//button[@class='ui button secondary']");

        private readonly ElementLocator
         btnWordPopupNo = new ElementLocator(Locator.XPath, "//button[@class='ui button']");

        private readonly ElementLocator
         btnWordPopupYes = new ElementLocator(Locator.XPath, "//button[@class='ui primary button']");

        private readonly ElementLocator
        btnPopupYes = new ElementLocator(Locator.XPath, "//button[@class='ui primary button']");

        private readonly ElementLocator
       btnWordPopupNoMessage = new ElementLocator(Locator.XPath, "//div[@class='content']");

        private readonly ElementLocator
       btnCommunationLogPopupMessage = new ElementLocator(Locator.XPath, "//div[@class='ui page modals dimmer transition visible active'][1]//div[@class='content'][1]");

        private readonly ElementLocator
            communicationLogCommentsText = new ElementLocator(Locator.XPath, "//a[@class='avatar']//..//div[@class='text']");

        private readonly ElementLocator
      btnWordNoDivPopup = new ElementLocator(Locator.XPath, "//div[@class='ui page modals dimmer transition visible active']");

        private readonly ElementLocator
         personalDetailsTab = new ElementLocator(Locator.XPath, "//a[text()='Personal Details']");

        private readonly ElementLocator
            aSTMGeneralInfoHeader = new ElementLocator(Locator.XPath, "//h5[text()='ASTM General Information']");

        private readonly ElementLocator
            aSTMGeneralInfoLabel = new ElementLocator(Locator.XPath, "//label");

        private readonly ElementLocator
                 memberTypeByDefaultInMemberDetailsPage = new ElementLocator(Locator.XPath, "//label[text()='Member Type']//..//div[@class='text']");

        private readonly ElementLocator
           membershipTypesRulesOptions = new ElementLocator(Locator.CssSelector, "table.customTable.memberShipTable tbody tr td a");

        private readonly ElementLocator
            historicalReasonDropdown = new ElementLocator(Locator.XPath, "//label[text()='Historical Reason']//..//i[@class='dropdown icon']");

        private readonly ElementLocator
            cancelButton = new ElementLocator(Locator.XPath, "//button[@title='Cancel']/i");

        private readonly ElementLocator
            accountStatusText = new ElementLocator(Locator.XPath, "//span[@class='labelTitle' and text()='Account Status']/..//span[@class='labelValue viewModeData']");

        private readonly ElementLocator
            accountPaidStatusText = new ElementLocator(Locator.XPath, "//span[@class='labelTitle' and text()='Paid Status']/..//span[@class='labelValue viewModeData']");

        private readonly ElementLocator
          tabCommuncationLog = new ElementLocator(Locator.XPath, "//a[@class='item'][contains(.,'Communication Log')]");

        private readonly ElementLocator
        inputCommuncationLog = new ElementLocator(Locator.XPath, "//div[@contenteditable='true']");

        private readonly ElementLocator
        submitBtnCommuncationLog = new ElementLocator(Locator.XPath, "//button[@class='ui primary button mr10']");

        private readonly ElementLocator
       removeCommuncationLog = new ElementLocator(Locator.XPath, "(//i[contains(@class,'delete icon')])[1]");

        private readonly ElementLocator
       communcationlogPopupOkButtron = new ElementLocator(Locator.XPath, "(//button[@class='ui primary button'])[1]");

        private readonly ElementLocator
       communcationlogPopupNoButtron = new ElementLocator(Locator.XPath, "(//button[@class='ui button'])[2]");

        private readonly ElementLocator
       dimmerVisible = new ElementLocator(Locator.XPath, "//div[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
       msgSuccess = new ElementLocator(Locator.XPath, "//p[contains(.,'Comment added successfully.')]");

        private readonly ElementLocator
      logText = new ElementLocator(Locator.XPath, "//div[@class='text'][contains(.,'{0}')]");

        private readonly ElementLocator
   communicationLogUser = new ElementLocator(Locator.XPath, "(//a[@class='author'])[{0}]");

        private readonly ElementLocator
              getIndexLogText = new ElementLocator(Locator.XPath, "//div[@class='content']//div[@class='text']");

        private readonly ElementLocator
             getLogFirstIndex = new ElementLocator(Locator.XPath, "//div[@class='content']//div[@class='text'][{0}]");

        private readonly ElementLocator
            communicationLogDateTime = new ElementLocator(Locator.XPath, "(//div[@class='metadata']/div)[{0}]");

        private readonly ElementLocator
       editCommuncationLog = new ElementLocator(Locator.XPath, "(//i[contains(@class,'pencil icon')])[1]");

        private readonly ElementLocator
       editinputCommuncationLog = new ElementLocator(Locator.XPath, "(//div[@class='commentposteditor'])[2]");

        private readonly ElementLocator
        editsubmitBtnCommuncationLog = new ElementLocator(Locator.XPath, "//button[@type='submit']");

        private readonly ElementLocator
       editCancelBtnCommuncationLog = new ElementLocator(Locator.XPath, "//button[@class='ui button cancel mr0']");

        private readonly ElementLocator
        cancelBtnCommuncationLog = new ElementLocator(Locator.XPath, "//button[@class='ui button cancel mr0']");

        private readonly ElementLocator
        editsubmitBtnEnabledLoginUser = new ElementLocator(Locator.XPath, "//a[text()='{0}']/parent::div/following-sibling::span/i[@class='pencil icon']");

        private readonly ElementLocator
        logUsersList = new ElementLocator(Locator.XPath, "//div[@class='content']//div/a");

        private readonly ElementLocator
        taggeduser = new ElementLocator(Locator.XPath, "//div[@class='tribute-container']//li[1]");

        private readonly ElementLocator
        selectTagUser = new ElementLocator(Locator.XPath, "//div[@class='tribute-container']//li[{0}]");

        private readonly ElementLocator
       istaggedusersaved = new ElementLocator(Locator.XPath, " //a[contains(@title,'{0}')]");

        private readonly ElementLocator
        taggeduserInput = new ElementLocator(Locator.XPath, "(//div[@class='commentposteditor'])[2]//a");

        private readonly ElementLocator
        redirectedPageName = new ElementLocator(Locator.XPath, " //div[text()='{0}']");

        private readonly ElementLocator
        emptyCommunicationlogText = new ElementLocator(Locator.XPath, "//div[@class='ui minimal comments']");

        private readonly ElementLocator
        editAstmMemberAccountStatusDetails = new ElementLocator(Locator.XPath, "//h5[text()='Member Account Status Details']//..//a[@class='editBtn']");

        private readonly ElementLocator
 organizationHeaderLink = new ElementLocator(Locator.XPath, "//h5[text()='Membership Type']//..//span[@class='labelValue']/a");

        private readonly ElementLocator
            activePage = new ElementLocator(Locator.XPath, "//div[contains(@class,'active section')]");

        private readonly ElementLocator
        organizationHeader = new ElementLocator(Locator.XPath, "//h5[text()='Membership Type']//..//span[@class='labelTitle'][contains(.,'{0}')]");

        private readonly ElementLocator
           selectedmemberAccount = new ElementLocator(Locator.XPath, "//table[@id='customGrid']//..//tr[{0}]//a[@class='column--Account']");

        private readonly ElementLocator
          selectedmemberName = new ElementLocator(Locator.XPath, "//table[@id='customGrid']//..//tr[{0}]//p[@class='column--Name']");

        private readonly ElementLocator
          selectedmemberCompany = new ElementLocator(Locator.XPath, "//table[@id='customGrid']//..//tr[{0}]//p[@class='column--Company']");

        private readonly ElementLocator
          selectedmemberRepresentaveCompany = new ElementLocator(Locator.XPath, "//table[@id='customGrid']//..//tr[{0}]//a[@class='column--Company']");

        private readonly ElementLocator
          selectedmemberEmail = new ElementLocator(Locator.XPath, "//table[@id='customGrid']//..//tr[{0}]//a[@class='column--Email']");

        private readonly ElementLocator
         selectedmemberShipName = new ElementLocator(Locator.XPath, "//table[@id='customGrid']//..//tr[{0}]//p[@class='column--MembershipTypeId']");

        private readonly ElementLocator
           comparememberAccount = new ElementLocator(Locator.XPath, "//span[@class='memberAccount'][contains(.,'Account Number: {0}')]");

        private readonly ElementLocator
          comparememberName = new ElementLocator(Locator.XPath, "//span[@class='memberName ellip'][contains(.,'{0}')]");

        private readonly ElementLocator
          comparememberCompany = new ElementLocator(Locator.XPath, "//span[@class='titleInfo ellip'][contains(.,'{0}')]");

        private readonly ElementLocator
          comparememberEmail = new ElementLocator(Locator.XPath, "//a[@class='ellip'][contains(.,'{0}')]");

        private readonly ElementLocator
        comparememberShipName = new ElementLocator(Locator.XPath, "//span[@class='roleTypenName'][contains(.,'{0}')]");

        private readonly ElementLocator
        banneraccountStatus = new ElementLocator(Locator.XPath, "//span[@class='titleInfo' and text()='{0}']");

        private readonly ElementLocator
        bannerPaidStatus = new ElementLocator(Locator.XPath, "//span[@class='pdStatus']");

        private readonly ElementLocator
       accountStatusPaidStatus = new ElementLocator(Locator.XPath, "//span[@class='labelValue viewModeData']");

        private readonly ElementLocator
       updateUserStatus = new ElementLocator(Locator.XPath, "//span[@class='lastUpdated'][contains(.,'{0}')]");

        private readonly ElementLocator
       editMemberTypeDropdown = new ElementLocator(Locator.XPath, "//label[text()='Member Type']//..//i[@class='dropdown icon']");

        private readonly ElementLocator
       mouContactDropdown = new ElementLocator(Locator.XPath, "//label[text()='MOU Contact Code']//..//i[@class='dropdown icon']");

        private readonly ElementLocator
                tagusersList = new ElementLocator(Locator.XPath, "//div[@class='tribute-container']");

        private readonly ElementLocator
        editSubmitButton = new ElementLocator(Locator.XPath, "//span[@class='update-cancel']//button[@class='ui primary button mr10']");

        private string nmTagUsersList = "Tag users List";
        private string nmUpdatedUserStatus = "Updated User DateTime Status";
        private string nmbanneraccountStatus = "Banner Account Status";
        private string nmbannerPaidStatus = "Banner Paid Status";
        private string nmselectedmemberAccount = "Selected Account Number ({0})";
        private string nmselectedmemberName = "Selected member Name ({0})";
        private string nmselectedmemberCompany = "Selected Company Name ({0})";
        private string nmselectedmemberEmail = "Selected Email Name ({0})";
        private string nmselectedmemberShipName = "Selected MemberShip Name ({0})";
        private string nmmemberAccount = "Account Number";
        private string nmmemberName = "Member Name";
        private string nmmemberCompany = "Company Name";
        private string nmmemberEmail = "Email";
        private string nmmemberShipName = "MemberShip Name";
        private string nmorganizationHeader = "Organisation Label";
        private string nmorganizationName = "Organisation Name";
        private string nmrepresentaive = "Representative";
        private string nmActivePage = "Organizational Account Details";
        private string nmMemberTypeHeader = "Member Type Label";
        private string nmCancelButton = "Cancel Button";
        private string nmcommuncationlogPopupOkButtron = "communcationlog Popup OkButton";
        private string nmcommuncationlogPopupNoButton = "communcationlog Popup No Button";
        private string nmtaggedUserInInput = "Communication Log Tagged User Link";
        private string nmEditButtonVisibilityCommuncationLog = "Communication Log Edit button Visibility : {0}";
        private string nmEditinputCommuncationLog = "Edit Communcation Log Input";
        private string nmEditButtonCommuncationLog = "Edit Button Communcation Log ";
        private string nmcommunicationLogDateTime = "Communication Log DateTime";
        private string nmcommunicationLogUser = "Communication Log User Name";
        private string nmlogText = "log Text  : {0}";
        private string nmbtnCommunationLogPopupMessage = "Communcation Log Popup Message";
        private string nminputCommuncationLog = "Communcation Log Input";
        private string nmsubmitBtnCommuncationLog = "Submit Btn Communcation Log";
        private string nmpersonalDetailsTab = "Personal Details Tab";
        private string nmhearAboutTextField = "Heard about Student Membership From Text Field";
        private string nmdegreeSoughtDropdownArrow = " Degree Sought dropdown icon";
        private string nmeditAstmGeneralInformation = "Edit ASTM General Information";
        private string nmeditAstmMemberAccountStatusDetails = "Edit ASTM Member Account Status Details";
        private string nmGraduationDateField = "Graduation Date Text Field";
        private string nmupdateAstmGeneralInformation = "Update button for ASTM General Information";
        private string nmviewStudentInformationlink = "View Student Information Link";
        private string nmtxtboxReasonForUpdate = "Text Area Reason for Update";
        private string nmupdateSuccessMessage = "Record updated successfully.";
        private string nmcommitteesOfInterest = "Committees Of Interest";
        private string nmuniversityTextField = "University Text Field";
        private string nmmajorTextField = "Major Text Field";
        private string nmexistingCommitteesDeleteIcon = "Delete Icon of Existing Committees";
        private string nmaccountStatusDropDown = "Account Status DropDown";
        private string nmokButton = "Ok Button";
        private string nmpaidStatusDropDown = "Paid Status DropDown";
        private string nmhistoricalReasonDropDown = "Historical  Reason DropDown";
        private string nmaccountStatusSelectActiveInDropDown = "{0} Account Status DropDown";
        private string nmaccountStatusDropDownSelection = "{0} Account Status Details Select In DropDown";
        private string nmSearchField = "Search Field";
        private string nmSearchClicked = "Search";
        private string nmhintTextInSearch = "Hint Text In Search Text";
        private string nmmailingListOption = "Mailing List Option";
        private string nmeditMembershipTypeSection = "Edit Membership Type";
        private string nmMemberType = "Membership Type dropdown icon";
        private string nmeditMemberAccountStatusDetailsSection = "Edit icon for MemberAccountStatus";
        private string nmmemberTypeDropdownicon = "Membership Type Dropdown icon";
        private string nmmouContactDropdown = "Mou Contact Dropdown";
        private string nmMemberAccountlink = "Any Member account number link from list";
        private string nmbtnWordNoDivPopup = "Word Popup Div";
        private string nmbtnwordicon = "Word icon";
        private string nmbtnWordPopupNoBtn = "Word Popup No Button";
        private string nmbtnWordPopupYesBtn = "Word Popup Yes Button";
        private string nmbtnPopupYesBtn = "Popup Yes Button";
        private string nmupdateMessageCommunicationLog = "Communication Log Message";
        private string nmremoveCommuncationLog = "Delete icon for Communication Log";
        private string nmtaguser = "Communication Log - Selected Tag User ({0}) ";
        private string nmtabCommuncationLog = "Communication Log Tab";
        private string nmemptyCommunicationlogText = "Empty Communication Log";
        private string nmGraduationErrorMsg = "Error Message For Graduation Date";
        private string nmReasonForUpdateErrorMsg = "Error Message For Reason For Update";
        private string nmHistoricalResaonErrorMsg = "Error Message For Historical Resaon";
        private string nmdimmerloading = "Dimmer loading";
        private string nmsmalldimmerloading = "Small Dimmer loading";

        public InternalStaffMembershipManagementPage(DriverContext driverContext)
            : base(driverContext)
        {
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

        public void GetEditedCommuncationLogExistsInDBSoftAssertion(string communcationLogText, string compareText, string message)
        {
            Verify.GetSingleValueFromDBCompareWithExpectedValue(this.DriverContext, string.Format(SqlQuery.FunctionalEditedMemberDetailsCommunicationExists, communcationLogText), compareText, message);
        }

        public void GetUpdateStudentMemberExistsInDB(string accountNumber, List<string> columnnames, List<string> values)
        {
            DataSet resultTable = SqlHelper.ExecuteSqlCommandQuery(string.Format(SqlQuery.FunctionalUpdateStudentMemberExists, accountNumber));
            Dictionary<string, string> actualDict = SqlHelper.GetTableColumnValueInDictionary(resultTable.Tables[0], columnnames);
            Dictionary<string, string> expectedDict = SqlHelper.GenerateDictionaryfromLists(columnnames, values);
            this.Driver.CompareTwoDictionaryFromPageAndDB(actualDict, expectedDict);
        }

        public void GetUpdateRepresentativeMemberExistsInDB(string accountNumber, List<string> columnnames, List<string> values)
        {
            DataSet resultTable = SqlHelper.ExecuteSqlCommandQuery(string.Format(SqlQuery.FunctionalRepresenativeStudentMemberExists, accountNumber));
            Dictionary<string, string> actualDict = SqlHelper.GetTableColumnValueInDictionary(resultTable.Tables[0], columnnames);
            Dictionary<string, string> expectedDict = SqlHelper.GenerateDictionaryfromLists(columnnames, values);
            this.Driver.CompareTwoDictionaryFromPageAndDB(actualDict, expectedDict);
        }

        public void SelecteRecordFromListWithIndex(out string selectedaccountno, out string selectedmembername, out string selectedcompayname, out string selectedEmail, out string selectedMemberShipName, string listindex)
        {
            selectedaccountno = this.GetSelectedAccountText(listindex);
            selectedmembername = this.GetSelectedMemberNameText(listindex);
            selectedcompayname = this.GetSelectedCompanyNameText(listindex);
            selectedEmail = this.GetSelectedEmailNameText(listindex);
            selectedMemberShipName = this.GetSelectedMemberShipNameText(listindex);
            this.IsUserIsAbleToClickOnAccountNumber(listindex);
        }

        public void SelecteRepresentativeRecordFromListWithIndex(out string selectedaccountno, out string selectedmembername, out string selectedcompayname, out string selectedEmail, out string selectedMemberShipName, string listindex)
        {
            selectedaccountno = this.GetSelectedAccountText(listindex);
            selectedmembername = this.GetSelectedMemberNameText(listindex);
            selectedcompayname = this.GetSelectedReprestativeCompanyNameText(listindex);
            selectedEmail = this.GetSelectedEmailNameText(listindex);
            selectedMemberShipName = this.GetSelectedMemberShipNameText(listindex);
            this.IsUserIsAbleToClickOnAccountNumber(listindex);
        }

        public string GetListofValues(string studyField, string graduationDate, string degree, string reasons, string isActive, string firstName, string lastName, string interestedCommittee)
        {
            return studyField + "|" + graduationDate + "|" + degree + "|" + reasons + "|" + isActive + "|" + interestedCommittee + "|" + firstName + "|" + lastName;
        }

        public string GetListofValuesOfRepresentative(string companyName, string membershipTypeName, string firstName, string lastName)
        {
            return companyName + "|" + membershipTypeName + "|" + firstName + "|" + lastName;
        }

        public void ValidateUpdatedUserwithDateTime(string expected, string currentdatetime, string username)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.updateSuccessMessage, BaseConfiguration.LongTimeout, this.nmupdateSuccessMessage);
            this.Driver.IsElementVisible(this.updateUserStatus.Format(string.Format(expected, username.ToLower(), currentdatetime)), this.nmUpdatedUserStatus);
            this.Driver.IsExpectedTextMatchWithActualText(this.updateUserStatus.Format(string.Format(expected, username.ToLower(), currentdatetime)), string.Format(expected, username.ToLower(), currentdatetime), this.nmbanneraccountStatus);
        }

        public void ValidateBannerAccountStatus(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.banneraccountStatus.Format(expected), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.banneraccountStatus.Format(expected), this.nmbanneraccountStatus);
            this.Driver.IsExpectedTextMatchWithActualText(this.banneraccountStatus.Format(expected), expected, this.nmbanneraccountStatus);
        }

        public void ValidateBannerPaidStatus(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.bannerPaidStatus, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.bannerPaidStatus, this.nmbannerPaidStatus);
            this.Driver.IsExpectedTextMatchWithActualText(this.bannerPaidStatus, expected, this.nmbannerPaidStatus);
        }

        public string GetSelectedAccountText(string index)
        {
            string accountNumber = Verify.getTextIfElementIsVisibleWithSoftAssertion(this.DriverContext, this.selectedmemberAccount.Format(index), string.Format(this.nmmemberAccount, string.Empty));
            return accountNumber;
        }

        public string GetSelectedMemberNameText(string index)
        {
            string memberName = Verify.getTextIfElementIsVisibleWithSoftAssertion(this.DriverContext, this.selectedmemberName.Format(index), string.Format(this.nmmemberName, string.Empty));
            return memberName;
        }

        public string GetSelectedCompanyNameText(string index)
        {
            string company = Verify.getTextIfElementIsVisibleWithSoftAssertion(this.DriverContext, this.selectedmemberCompany.Format(index), string.Format(this.nmmemberCompany, string.Empty));
            return company;
        }

        public string GetSelectedReprestativeCompanyNameText(string index)
        {
            string company = Verify.getTextIfElementIsVisibleWithSoftAssertion(this.DriverContext, this.selectedmemberRepresentaveCompany.Format(index), string.Format(this.nmmemberCompany, string.Empty));
            return company;
        }

        public string GetSelectedEmailNameText(string index)
        {
            string email = Verify.getTextIfElementIsVisibleWithSoftAssertion(this.DriverContext, this.selectedmemberEmail.Format(index), string.Format(this.nmmemberEmail, string.Empty));
            return email;
        }

        public string GetSelectedMemberShipNameText(string index)
        {
            string memberShipName = Verify.getTextIfElementIsVisibleWithSoftAssertion(this.DriverContext, this.selectedmemberShipName.Format(index), string.Format(this.nmmemberShipName, string.Empty));
            return memberShipName;
        }

        public void ValidateAccountNumber(string expected)
        {
            this.Driver.IsElementVisibleWithSoftAssertion(this.comparememberAccount.Format(expected), string.Format(this.nmselectedmemberAccount, expected));
        }

        public void ValidateAccountNumberWithWait(string expected)
        {
            this.Driver.WaitUntilElementIsFound(this.comparememberAccount.Format(expected), BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisibleWithSoftAssertion(this.comparememberAccount.Format(expected), string.Format(this.nmselectedmemberAccount, expected));
        }

        public void ValidateMemberName(string expected)
        {
            this.Driver.IsElementVisibleWithSoftAssertion(this.comparememberName.Format(expected), string.Format(this.nmselectedmemberName, expected));
        }

        public void ValidateCompanyName(string expected)
        {
            this.Driver.IsElementVisibleWithSoftAssertion(this.comparememberCompany.Format(expected), string.Format(this.nmselectedmemberCompany, expected));
        }

        public void ValidateEmailName(string expected)
        {
            this.Driver.IsElementVisibleWithSoftAssertion(this.comparememberEmail.Format(expected), string.Format(this.nmselectedmemberEmail, expected));
        }

        public void ValidateMembershipName(string expected)
        {
            this.Driver.IsElementVisibleWithSoftAssertion(this.comparememberShipName.Format(expected), string.Format(this.nmselectedmemberShipName, expected));
        }

        public void VerifyTagUserListOnTypingAtSoftAssertion()
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.tagusersList, this.nmTagUsersList, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.tagusersList, this.nmTagUsersList);
        }

        public string VerifyTagUsersinCommunicationLogInput()
        {
            this.Driver.WaitUntilElementIsFound(this.taggeduser, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.taggeduser, this.nmtaguser);
            string getusername = this.Driver.GetText(this.taggeduser);
            this.Driver.IsElementClickable(this.taggeduser, this.nmtaguser);
            this.Driver.WaitForPageLoad();
            return getusername;
        }

        public string InputTagUserinCommunicationLogInput(string index)
        {
            this.Driver.WaitUntilElementIsFound(this.selectTagUser.Format(index), BaseConfiguration.LongTimeout);
            string getusername = this.Driver.GetText(this.selectTagUser.Format(index));
            this.Driver.IsElementClickableWithSoftAssertion(this.selectTagUser.Format(index), string.Format(this.nmtaguser, getusername));
            this.Driver.WaitForPageLoad();
            return getusername;
        }

        public void IsMemberTypeClickableInMemberTypeDrpDwn(string membershipType)
        {
            this.Driver.WaitUntilElementIsFound(this.memberTypeDropDownOptions, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickableFromListofElementWithText(this.memberTypeDropDownOptions, membershipType);
            System.Threading.Thread.Sleep(2000);
        }

        public void IsUserIsAbleToClickOrganisationalLink()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            var by = this.organizationHeaderLink.ToBy();
            var webElement = this.Driver.FindElement(by).GetAttribute("href");
            if (webElement is null)
            {
                Assert.IsTrue(false, "Organization link is not a Hyperlink");
            }
            else
            {
                var webElementLink = this.Driver.FindElement(by);
                this.Driver.JavaScriptClick(webElementLink, this.nmorganizationHeader);
                this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmActivePage);
                this.Driver.IsExpectedTextMatchWithActualText(this.activePage, this.nmActivePage, string.Empty);
            }
        }

        public void IsOrgnanisationHeaderVisible(string expected, bool visible)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            if (visible == true)
            {
                Assert.IsTrue(this.Driver.IsElementPresentOrNot(this.organizationHeader.Format(expected), string.Format(this.nmorganizationHeader, expected), string.Empty), "Failed : Verify " + this.nmorganizationHeader + " is not displayed successfully");
            }
            else
            {
                Assert.IsFalse(this.Driver.IsElementPresentOrNot(this.organizationHeader.Format(expected), string.Format(this.nmorganizationHeader, expected), string.Empty), "Failed :  Verify " + this.nmorganizationHeader + " is  displayed successfully");
            }
        }

        public void IsMemberTypeHeaderVisible(string expected, bool visible)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            if (visible == true)
            {
                Assert.IsTrue(this.Driver.IsElementPresentOrNot(this.organizationHeader.Format(expected), string.Format(this.nmMemberTypeHeader, expected), string.Empty), "Failed : Verify " + this.nmMemberTypeHeader + " is not displayed successfully");
            }
            else
            {
                Assert.IsFalse(this.Driver.IsElementPresentOrNot(this.organizationHeader.Format(expected), string.Format(this.nmorganizationHeader, expected), string.Empty), "Verify " + this.nmorganizationHeader + " is  displayed successfully");
            }
        }

        public void IselementVisibleAfterDlete(string expected)
        {
            Assert.IsTrue(this.Driver.IsElementPresentOrNot(this.logText.Format(expected), string.Format(this.nmlogText, expected), string.Empty), "Unable to find Element Text :" + expected);
        }

        public void IsUserIsAbleToClickCommunicationLogTab()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmtabCommuncationLog);
            this.Driver.WaitUntilElementIsFound(this.tabCommuncationLog, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.tabCommuncationLog);
            this.Driver.JavaScriptClick(webElement, this.nmtabCommuncationLog);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserAbleToEnterTextInCommuncationLog(string communicationLog)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nminputCommuncationLog);
            this.Driver.IsElementVisible(this.inputCommuncationLog, this.nminputCommuncationLog);
            this.Driver.EnterText(this.inputCommuncationLog, communicationLog, this.nminputCommuncationLog);
        }

        public void IsUserIsAbleToClickCommunicationLogSubmitButtron()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            var webElement = this.Driver.GetElement(this.submitBtnCommuncationLog);
            this.Driver.JavaScriptClick(webElement, this.nmsubmitBtnCommuncationLog);
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
        }

        public void IsCancelButtonClicked()
        {
            this.Driver.WaitUntilElementIsFound(this.cancelBtnCommuncationLog, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.cancelBtnCommuncationLog, this.nmCancelButton);
            var webElement = this.Driver.GetElement(this.cancelBtnCommuncationLog);
            this.Driver.JavaScriptClick(webElement, this.nmCancelButton);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserIsAbleToClickcommuncationlogPopupOkButtron()
        {
            this.Driver.WaitUntilElementIsFound(this.communcationlogPopupOkButtron, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.communcationlogPopupOkButtron);
            this.Driver.JavaScriptClick(webElement, this.nmcommuncationlogPopupOkButtron);
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            if (this.Driver.IsElementPresentOrNot(this.communcationlogPopupOkButtron, this.nmcommuncationlogPopupOkButtron, string.Empty))
            {
                webElement = this.Driver.GetElement(this.communcationlogPopupOkButtron);
                this.Driver.JavaScriptClick(webElement, this.nmcommuncationlogPopupOkButtron);
            }

            this.Driver.WaitForPageLoad();
        }

        public void IsUserIsAbleToClickcommuncationlogPopupNoButtron()
        {
            this.Driver.WaitUntilElementIsFound(this.communcationlogPopupNoButtron, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.communcationlogPopupNoButtron);
            this.Driver.JavaScriptClick(webElement, this.nmcommuncationlogPopupNoButton);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserAbleToViewCommuncationLog(string updateSuccessMessage)
        {
            this.Driver.IsElementVisible(this.updateSuccessMessage, this.nmupdateMessageCommunicationLog);
            this.Driver.IsExpectedTextMatchWithActualText(this.updateSuccessMessage, updateSuccessMessage, string.Format(this.nmupdateMessageCommunicationLog, updateSuccessMessage));
            this.Driver.WaitUntilElementIsNoLongerFound(this.updateSuccessMessage, BaseConfiguration.LongTimeout, this.nmupdateSuccessMessage);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserAbleToViewCommuncationLogMessage(string updateSuccessMessage)
        {
            this.Driver.IsElementVisible(this.updateSuccessMessage, this.nmupdateMessageCommunicationLog);
            this.Driver.IsExpectedTextMatchWithActualText(this.updateSuccessMessage, updateSuccessMessage, string.Format(this.nmupdateMessageCommunicationLog, updateSuccessMessage));
            this.Driver.WaitUntilElementIsNoLongerFound(this.updateSuccessMessage, BaseConfiguration.LongTimeout, string.Format(this.nmupdateMessageCommunicationLog, updateSuccessMessage));
            this.Driver.WaitForPageLoad();
        }

        public void IsUserAbleToViewCommuncationLogMessageSoftAssertion(string updateSuccessMessage)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateMessageCommunicationLog, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateMessageCommunicationLog);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, updateSuccessMessage, string.Format(this.nmupdateMessageCommunicationLog, updateSuccessMessage));
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, string.Format(this.nmupdateMessageCommunicationLog, updateSuccessMessage), BaseConfiguration.LongTimeout);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserIsAbleToClickRemoveCommunicationLog()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmremoveCommuncationLog);
            var by = this.removeCommuncationLog.ToBy();
            var webElement = this.Driver.FindElement(by);
            this.Driver.JavaScriptClick(webElement, this.nmremoveCommuncationLog);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserAbletoViewMessageOnCommuncationLogPOpup(string updateSuccessMessage)
        {
            this.Driver.WaitUntilElementIsFound(this.btnCommunationLogPopupMessage, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.btnCommunationLogPopupMessage, this.nmbtnCommunationLogPopupMessage);
            this.Driver.IsExpectedTextMatchWithActualText(this.btnCommunationLogPopupMessage, updateSuccessMessage, this.nmbtnCommunationLogPopupMessage);
        }

        public void IsUserAbletoViewMessageOnCommuncationLogPOpupSoftAssertion(string updateSuccessMessage)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.btnCommunationLogPopupMessage, this.nmbtnCommunationLogPopupMessage, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.btnCommunationLogPopupMessage, this.nmbtnCommunationLogPopupMessage);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.btnCommunationLogPopupMessage, updateSuccessMessage, this.nmbtnCommunationLogPopupMessage);
        }

        public void IsUserAbleToViewAllCommunicationLogAgainstTask()
        {
            try
            {
                this.Driver.WaitUntilElementIsFound(this.communicationLogCommentsText, BaseConfiguration.LongTimeout);
                IList<IWebElement> items = this.Driver.GetElements(this.communicationLogCommentsText);
                int cardCount = items.Count();
                if (cardCount != 0)
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Validate that the  user can view all Communication Log against that Task", "user can view all Communication Log against that Task");
                    Logger.Info("user can view all the communication logged against that Task");
                }
                else
                {
                    throw new Exception("An exception occured while the user views the Communication Log against the Task");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while the user views the Communication Log against the Task " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Validate that the  user can view all the communication logged against the Task", "An exception occured while the user views the Communication Log against the Task");
            }
        }

        public void IsUserIsAbleToClickWordIcon()
        {
            this.Driver.WaitUntilElementIsFound(this.btnWordIcon, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.btnWordIcon);
            this.Driver.JavaScriptClick(webElement, this.nmbtnwordicon);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserIsAbleToClickWordPopupNoIcon()
        {
            this.Driver.WaitUntilElementIsFound(this.btnWordPopupNo, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.btnWordPopupNo);
            this.Driver.JavaScriptClick(webElement, this.nmbtnWordPopupNoBtn);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserIsAbleToClickWordPopupYesIcon()
        {
            this.Driver.WaitUntilElementIsFound(this.btnWordPopupYes, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.btnWordPopupYes);
            this.Driver.JavaScriptClick(webElement, this.nmbtnWordPopupYesBtn);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserIsAbleToClickPopupYesIcon()
        {
            this.Driver.WaitUntilElementIsFound(this.btnPopupYes, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.btnPopupYes);
            this.Driver.JavaScriptClick(webElement, this.nmbtnPopupYesBtn);
        }

        public void GetTextToVerifyFromDocx(string expectedText)
        {
            try
            {
                string filePath = TestContext.CurrentContext.TestDirectory + "\\" + BaseConfiguration.DownloadFolder;
                int getFileCount = FilesHelper.GetFilesOfGivenType(filePath, FileType.Docx).Count;
                string[] files = Directory.GetFiles(TestContext.CurrentContext.TestDirectory + "\\" + BaseConfiguration.DownloadFolder);
                if (files.Count() >= 1)
                {
                    bool isExpectedText = FilesHelper.ExtractAndValidateTextFromDocx(files[0], expectedText);
                    Assert.IsTrue(isExpectedText);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while reading file : " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Exception occured while reading file");
            }
        }

        public void IsUserAbleToViewPopupMessageOnWordIConCliked(string name)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.btnWordPopupNoMessage, name);
        }

        public void IsuserAbleToViewPopupOnClikingNoButton()
        {
            bool isElementVisible = this.Driver.IsElementPresentOrNot(this.btnWordNoDivPopup, this.nmbtnWordNoDivPopup, string.Empty);
            Assert.IsFalse(isElementVisible);
        }

        public void IsUserIsAbleToClickOnMemberTypeDropDownList()
        {
            this.Driver.WaitUntilElementIsFound(this.memberTypeDropdown, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.memberTypeDropdown);
            this.Driver.JavaScriptClick(webElement, this.nmmemberTypeDropdownicon);
            this.Driver.WaitUntilElementIsNoLongerFound(this.smallLoaderOnCardSection, BaseConfiguration.LongTimeout, this.nmsmalldimmerloading);
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
        }

        public void IsStudentMemberTypeClickableInMemberTypeDrpDwn(string membershipType)
        {
            this.Driver.WaitUntilElementIsFound(this.memberTypeDropDownOptions, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickableFromListofElementWithText(this.memberTypeDropDownOptions, membershipType);
            this.Driver.WaitUntilElementIsNoLongerFound(this.smallLoaderOnCardSection, BaseConfiguration.MediumTimeout, this.nmsmalldimmerloading);
        }

        public void IsUserIsAbleToClickOnAccountNumberOfMemberType()
        {
            this.Driver.WaitUntilElementIsFound(this.memberAccountLink, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.memberAccountLink);
            this.Driver.JavaScriptClick(webElement, this.nmMemberAccountlink);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserIsAbleToClickOnAccountNumber(string index)
        {
            this.Driver.WaitUntilElementIsFound(this.selectedmemberAccount.Format(index), BaseConfiguration.MediumTimeout);
            var webElement = this.Driver.GetElement(this.selectedmemberAccount.Format(index));
            this.Driver.JavaScriptClick(webElement, this.nmMemberAccountlink);
            this.Driver.WaitForPageLoad();
        }

        public void IsASTMGeneralInformationEditable()
        {
            this.Driver.ScrollToWebElement(this.editAstmGeneralInformation);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.editAstmGeneralInformation, this.nmeditAstmGeneralInformation);
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.editAstmGeneralInformation, this.nmeditAstmGeneralInformation, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.editAstmGeneralInformation, this.nmeditAstmGeneralInformation);
        }

        public void IsAstmMemberAccountStatusDetailsEditable()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.updateSuccessMessage, BaseConfiguration.LongTimeout, this.nmupdateSuccessMessage);
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            this.Driver.IsElementVisibleWithSoftAssertion(this.editAstmMemberAccountStatusDetails, this.nmeditAstmMemberAccountStatusDetails);
            var webElement = this.Driver.GetElement(this.editAstmMemberAccountStatusDetails);
            this.Driver.JavaScriptClick(webElement, this.nmeditAstmMemberAccountStatusDetails);
            if (this.Driver.IsElementPresentOrNot(this.accountStatusDropDown, this.nmaccountStatusDropDown, string.Empty) == false)
            {
                this.Driver.IsElementClickable(this.editAstmMemberAccountStatusDetails, this.nmeditAstmMemberAccountStatusDetails);
            }
        }

        public void IsUserAbleToEnterTextInGraduationDateField(string graduationDate)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.graduationDateField, this.nmGraduationDateField);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.graduationDateField, graduationDate, this.nmGraduationDateField);
        }

        public void IsUserAbleToEnterTextInReasonForUpdateField(string reasonForUpdate)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.txtboxReasonForUpdate, this.nmtxtboxReasonForUpdate);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.txtboxReasonForUpdate, reasonForUpdate, this.nmtxtboxReasonForUpdate);
        }

        public void IsUserAbleToEnterTextInAccountStatusReasonForUpdateField(string reasonForUpdate)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.txtboxAccountStatusReasonForUpdate, this.nmtxtboxReasonForUpdate);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.txtboxAccountStatusReasonForUpdate, reasonForUpdate, this.nmtxtboxReasonForUpdate);
        }

        public void IsASTMGeneralInformationUpdateButtonClickable()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.updateAstmGeneralInformation, this.nmupdateAstmGeneralInformation);
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.updateAstmGeneralInformation, this.nmupdateAstmGeneralInformation, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.updateAstmGeneralInformation, this.nmupdateAstmGeneralInformation);
        }

        public void IsGetErrorMessageForReasonForUpdateField(string errorMsg)
        {
            Verify.ValiadteTextPresentOnElementWithSoftAssertion(this.DriverContext, this.errorMsgReasonForUpdate, this.nmReasonForUpdateErrorMsg, errorMsg);
            /////Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.highlightedMandatoryGraduationDate, "Highlighted Graduation Date");
        }

        public void IsTaskRecordUpdatedSuccessfully(string updateSuccessMessage)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateSuccessMessage, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateSuccessMessage);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, updateSuccessMessage, this.nmupdateSuccessMessage);
        }

        public void IsAccountStatusTaskRecordUpdatedSuccessfully(string updateSuccessMessage, string updatedAlreadyMessage)
        {
            Verify.WaitUntilElementIsNoLongerFoundWithSoftAssertion(this.DriverContext, this.dimmerVisible, this.nmupdateSuccessMessage, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, this.nmupdateSuccessMessage);
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            this.Driver.IsElementVisible(this.updateSuccessMessage, this.nmupdateSuccessMessage);
            var element = this.Driver.GetElement(this.updateSuccessMessage);
            if (element.Text == updateSuccessMessage)
            {
                Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, updateSuccessMessage, this.nmupdateSuccessMessage);
            }
            else
            {
                Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.updateSuccessMessage, updatedAlreadyMessage, this.nmupdateSuccessMessage);
            }
        }

        public string IsUserAbleToSelectMoreCommitteeOfInterest()
        {
            var element = this.Driver.GetText(this.committeeOfInterestOptions);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.committeeOfInterestOptions, element);
            return element;
        }

        public void IsUserAbleToClearCommitteeOfInterest()
        {
            int element = 0;
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.committeesOfInterest, this.nmcommitteesOfInterest);
            element = this.Driver.CountForElements(this.checkDataInCommittee);

            if (element != 0)
            {
                for (int i = 1; i <= element; i++)
                {
                    Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.existingCommitteesDeleteIcon, this.nmexistingCommitteesDeleteIcon);
                }
            }
        }

        public void IsUserAbleToEnterTextInCommitteesOfInterestDropDown()
        {
            this.Driver.IsElementClickable(this.committeesOfInterest, this.nmcommitteesOfInterest);
        }

        public void IsUserAbleToEnterTextInCommitteesOfInterest(string committee)
        {
            int element = 0;
            try
            {
                this.Driver.IsElementVisible(this.committeesOfInterest, this.nmcommitteesOfInterest);
                element = this.Driver.CountForElements(this.checkDataInCommittee);

                if (element != 0)
                {
                    this.Driver.IsElementVisible(this.existingCommitteesDeleteIcon, this.nmexistingCommitteesDeleteIcon);
                    this.Driver.IsElementClickable(this.existingCommitteesDeleteIcon, this.nmexistingCommitteesDeleteIcon);
                }

                var committeeInterestInput = this.Driver.GetElement(this.committeeOfInterestInput);
                committeeInterestInput.SendKeys(committee);
                this.Driver.WaitUntilElementIsFound(this.committeeOfInterestOptions, BaseConfiguration.LongTimeout);
                this.Driver.IsElementClickableFromListofElementWithText(this.committeeOfInterestOptions, committee);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify User should able to update Committee of Interest field", "User is able to update Committee of Interest field successfully");
                Logger.Info("User is able to update Committee of Interest successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while updating Committee of Interest field " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify User should able to update Committee of Interest field", "An exception occurred while while updating Committee of Interest field");
                throw;
            }
        }

        public void IsASTMGeneralInformationMandatoryFieldsCleared()
        {
            IWebElement graduationWebElement = this.Driver.GetElement(this.graduationDateField);
            string graduationDateText = graduationWebElement.GetAttribute("value");
            if (graduationDateText != string.Empty)
            {
                graduationWebElement.Clear();
            }
        }

        public void IsGetErrorMessageForGraduationField(string errorMsg)
        {
            Verify.ValiadteTextPresentOnElementWithSoftAssertion(this.DriverContext, this.errorMsgGraduationDate, this.nmGraduationErrorMsg, errorMsg);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.highlightedMandatoryGraduationDate, "Highlighted Graduation Date");
        }

        public void IsUserAbleToEnterTextInUniversityField(string university)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.universityTextField, this.nmuniversityTextField);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.universityTextField, university, this.nmuniversityTextField);
        }

        public void IsIncorrectDataErrorMsgDisplayedAndFieldHighlighted(string errorMsg)
        {
            this.Driver.WaitUntilElementIsFound(this.incorrectDataGraduationError, BaseConfiguration.LongTimeout);
            var errorMsgWebElement = this.Driver.GetElement(this.incorrectDataGraduationError);
            if (errorMsgWebElement.Text.Equals(errorMsg.Trim()))
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Graduation Date Incorrect Input Error Message is visible", "Graduation Date Incorrect Input Error Message is visible successfully");
                Logger.Info("Graduation Date Incorrect Input Error message is visible successfully");
            }

            this.Driver.WaitUntilElementIsFound(this.highlightedMandatoryGraduationDate, BaseConfiguration.LongTimeout);
            var grauadtionFieldHighlighted = this.Driver.GetElement(this.highlightedMandatoryGraduationDate);
            if (grauadtionFieldHighlighted.Displayed)
            {
                var webElementLocator = this.Driver.GetElement(this.graduationDateField);
                if (webElementLocator.Displayed)
                {
                    this.Driver.HighlightingWebElement(webElementLocator);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Graduation Date Incorrect Input Field is Highlighted", "Graduation Date Incorrect Input Field is Highlighted successfully");
                    Logger.Info("Graduation Date Incorrect Input Field is Highlighted successfully");
                }
            }
            else
            {
                throw new Exception("Graduation Date Incorrect Input Field is not Highlighted");
            }
        }

        public void IsUserAbleToEnterTextInMajorTextField(string major)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.majorTextField, this.nmmajorTextField);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.majorTextField, major, this.nmmajorTextField);
        }

        public void IsDefaultValuedisplayedAs25(string text)
        {
            this.Driver.ScrollToWebElement(this.selectNoOfItemsperPage);
            this.Driver.IsExpectedTextMatchWithActualText(this.defaultValueperPage, text);
        }

        public void IsUserableToSeeResultsPerPageDropdownWithValues()
        {
            try
            {
                IList<string> lstElements1 = new List<string>() { "25", "50", "75", "100" };
                IList<string> lstElements2 = new List<string>();
                IList<IWebElement> webelementslist = this.Driver.GetElements(this.selectNoOfItemsperPage);
                foreach (IWebElement i in webelementslist)
                {
                    lstElements2.Add(i.Text);
                }

                var result = lstElements1.Except(lstElements2).ToList();
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify whether Items displayed for page in Member Management contains 25, 50, 75, 100 count in list", " Items displayed for page in Member Management contains 25, 50, 75, 100 count in list");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to verify drop down values from items per page Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify item per page drop down An exception occurred while finding count on ");
            }
        }

        public void IsUserAbleToClickOnAccountStatusDropDown()
        {
            this.Driver.IsElementVisible(this.accountStatusDropDown, this.nmaccountStatusDropDown);
            var webElement = this.Driver.GetElement(this.accountStatusDropDown);
            this.Driver.JavaScriptClick(webElement, this.nmaccountStatusDropDown);
        }

        public void IsUserAbleToClickOnPaidStatusDropDown()
        {
            this.Driver.IsElementVisible(this.paidStatusDropDown, this.nmpaidStatusDropDown);
            var webElement = this.Driver.GetElement(this.paidStatusDropDown);
            this.Driver.JavaScriptClick(webElement, this.nmpaidStatusDropDown);
        }

        public void IsUserAbleToClickOnHistoricalStatusDropDown()
        {
            this.Driver.IsElementVisible(this.historicalReasonDropdown, this.nmhistoricalReasonDropDown);
            var webElement = this.Driver.GetElement(this.historicalReasonDropdown);
            this.Driver.JavaScriptClick(webElement, this.nmhistoricalReasonDropDown);
        }

        public void IsUserAbleToClickOnAccountStatusSelectActiveInDropdown(string name)
        {
            this.Driver.IsElementVisible(this.accountStatusSelectActiveInDropDown.Format(name), string.Format(this.nmaccountStatusSelectActiveInDropDown, name + " in "));
            var webElement = this.Driver.GetElement(this.accountStatusSelectActiveInDropDown.Format(name));
            this.Driver.JavaScriptClick(webElement, string.Format(this.nmaccountStatusSelectActiveInDropDown, name + " in "));
        }

        public void IsUserAbleToClickOnAccountPaidStatusSelectActiveInDropdown(string name)
        {
            this.Driver.IsElementVisible(this.accountStatusDropDownSelection.Format(name), string.Format(this.nmaccountStatusDropDownSelection, name + " in "));
            var webElement = this.Driver.GetElement(this.accountStatusDropDownSelection.Format(name));
            this.Driver.JavaScriptClick(webElement, string.Format(this.nmaccountStatusDropDownSelection, name + " in "));
        }

        public void IsUserAbleToCancelStatusDetailsInformation()
        {
            this.Driver.WaitUntilElementIsFound(this.cancelButton, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.cancelButton, this.nmCancelButton);
            var webElement = this.Driver.GetElement(this.cancelButton);
            this.Driver.JavaScriptClick(webElement, this.nmCancelButton);
        }

        public string GetTextOfAccountStatus()
        {
            var actualValue = this.Driver.GetText(this.accountStatusText);
            return actualValue;
        }

        public string GetTextOfAccountPaidStatus()
        {
            var actualValue = this.Driver.GetText(this.accountPaidStatusText);
            return actualValue;
        }

        public void VerifyaccountStatusafterCancelInformation(string expectedValue, string actualValue)
        {
            Verify.That(this.DriverContext, () => Assert.AreEqual(expectedValue, actualValue), "Verifying account Status Value after Cancelling updated Information", "Value is same as expected", "Value has been modified even after cancellation");
        }

        public void IsUserAbleToViewStatusColumn(string name)
        {
            try
            {
                this.Driver.WaitUntilElementIsFound(this.statusColumn, BaseConfiguration.LongTimeout);
                IList<IWebElement> lstTableElements = this.Driver.GetElements(this.statusColumn);
                bool exist = lstTableElements.All(x => x.Text == name);
                if (exist == true)
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is visible under the User column ", name + " is visible successfully under status Column");
                    Logger.Info(name + " is visible successfully under status Column");
                }
                else
                {
                    Assert.IsFalse(exist);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.LongTimeout, "An exception occurred waiting for " + name + " to become visible");
            }
        }

        public void IsUserAbleToViewMemberInDropDownBesideSeachBox(string name)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.memberdropdownInSearch, name);
        }

        public void IsUserAbleToEnterTextInSearchField(string name)
        {
            this.Driver.EnterText(this.searchtextfield, name, this.nmSearchField);
        }

        public void IsUserAbleToEnterTextInSearchFieldWithWait(string name)
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.updateSuccessMessage, BaseConfiguration.LongTimeout, this.nmSearchField);
            this.Driver.EnterText(this.searchtextfield, name, this.nmSearchField);
        }

        public void IsUserAbleToClickOnSearch()
        {
            this.Driver.IsElementVisible(this.searchclick, this.nmSearchClicked);
            this.Driver.WaitUntilElementIsFound(this.searchclick, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickable(this.searchclick, this.nmSearchClicked);
        }

        public void IsUserAbleToViewNoDataFound(string name)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.errorMessageForNoData, name);
        }

        public void IsHintTextVisibleInSearchBox(string expectedHintText)
        {
            this.Driver.IsElementVisible(this.searchtextfield, this.nmhintTextInSearch);
            var webelement = this.Driver.GetElement(this.searchtextfield);
            string actualHintText = webelement.GetAttribute("placeholder");
            try
            {
                Verify.That(this.DriverContext, () => Assert.AreEqual(expectedHintText, actualHintText), "Verifying search placeholder from search box :" + expectedHintText, actualHintText + " search box place holder displayed successfully", "search box placeholder is not displaying");
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Expected Value is matching with Actual text ", "The expected Value is " + expectedHintText + " and  actual value is " + actualHintText + " matching successfully");
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify text on " + expectedHintText + "with Actual Text " + actualHintText, "An exception occurred while finding text on " + expectedHintText);
                throw;
            }
        }

        public void IsUserIsAbleToClickOnDegreeSoughtDropDown()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.degreeSoughtDropdownArrow, this.nmdegreeSoughtDropdownArrow);
            this.Driver.WaitUntilElementIsFound(this.degreeSoughtDropdownArrow, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.degreeSoughtDropdownArrow, this.nmdegreeSoughtDropdownArrow);
        }

        public void IsUserAbleToSelectDegreeSought(string degree)
        {
            this.Driver.WaitUntilElementIsFound(this.degreeSoughtOptions, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableFromListofElementWithTextWithSoftAssertion(this.DriverContext, this.degreeSoughtOptions, degree);
        }

        public void IsUserAbleToEnterTextInHearAboutMembershipTextField(string heardFrom)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.hearAboutTextField, this.nmhearAboutTextField);
            Verify.EnterTextWithSoftAssertion(this.DriverContext, this.hearAboutTextField, heardFrom, this.nmhearAboutTextField);
        }

        public string GetHearAboutMembershipText()
        {
            this.Driver.IsElementVisible(this.hearAboutTextField, this.nmhearAboutTextField);
            return this.Driver.GetText(this.hearAboutTextField);
        }

        public void IsUserAbleToSelectMailingListOption()
        {
            this.Driver.MouseOverOnWebElementAndClick(this.mailingListOption, this.nmmailingListOption);
        }

        public void IsUserAbleToValidateMajorFieldInStudentMemberType(string graduationDate, string reasonForUpdate, string updateSuccessMsg, string majorText)
        {
            var element = this.Driver.GetElement(this.majorLabelText);
            if (element.TagName == "label")
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Major is Label", "Major is Label verified successfully");
                Logger.Info("Major is Label verified successfully");
            }
            else
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Major is Label", "Major is not a Label");
                Logger.Info("Major is not a Label");
                throw new Exception("Major is not a Label");
            }

            var elementField = this.Driver.GetElement(this.majorTextFieldVerify);
            if (elementField.TagName == "input")
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Field Type is a Text field", "Field Type is a Text field verified successfully");
                Logger.Info("Field Type is a Text field verified successfully");
            }
            else
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Field Type is a Text field", "Field Type is not a Text field");
                Logger.Info("Field Type is not a Text field");
                throw new Exception("Field Type is not a Text field");
            }

            this.IsUserAbleToEnterTextInGraduationDateField(graduationDate);
            this.IsUserAbleToEnterTextInReasonForUpdateField(reasonForUpdate);
            this.IsASTMGeneralInformationUpdateButtonClickable();
            this.IsTaskRecordUpdatedSuccessfully(updateSuccessMsg);
            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Major Field is not a Mandatory field", "Major Field is not a Mandatory field  verified successfully");
            Logger.Info("Major Field is not a Mandatory field verified successfully");
            this.IsASTMGeneralInformationEditable();
            this.IsUserAbleToEnterTextInMajorTextField(majorText);
            this.IsASTMGeneralInformationUpdateButtonClickable();
            DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Field Limitation Major text field is All Characters", "Field Limitation Major text field is All Characters");
            Logger.Info("Field Limitation Major text field is All Characters");
        }

        public void IsMembershipTypeSectionEditable()
        {
            this.Driver.ScrollToWebElement(this.copyButtonMemberDetails);
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.editMembershipTypeSection, this.nmeditMembershipTypeSection, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.editMembershipTypeSection, this.nmeditMembershipTypeSection);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.editMembershipTypeSection, this.nmeditMembershipTypeSection);
        }

        public void IsMemberAccountStatusDetailsSectionEditable()
        {
            this.Driver.WaitUntilElementIsFound(this.editMemberAccountStatusDetailsSection, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.editMemberAccountStatusDetailsSection);
            this.Driver.JavaScriptClick(webElement, this.nmeditMemberAccountStatusDetailsSection);
        }

        public void IsMemberDetailsMemberTypeClickable(string membershipType)
        {
            this.Driver.WaitUntilElementIsFound(this.memberTypeDropDownOptions, BaseConfiguration.LongTimeout);
            this.Driver.IsElementClickableFromListofElementWithText(this.memberTypeDropDownOptions, membershipType);
            System.Threading.Thread.Sleep(2000);
        }

        public void IsUserIsAbleToClickOnMemberTypeInMemberDetailsPage()
        {
            this.Driver.WaitUntilElementIsFound(this.memberTypeMemberDetails, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.memberTypeMemberDetails);
            this.Driver.JavaScriptClick(webElement, this.nmMemberType);
        }

        public void IsRepresentativeInMemberTypeDropDown(string representative)
        {
            bool representativeDisplayed = Verify.IsElementVisibleFromListOfElementWithSoftAssertion(this.DriverContext, this.memberTypeMemberDetailsOptions, representative);
            if (representativeDisplayed)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Representative option is not visible in Member Type drop down", "Representative option is visible in Member Type drop down");
                Logger.Info("Representative option is visible in Member Type dropdown");
                throw new Exception("Representative option is visible in Member Type dropdown");
            }
            else
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Representative option is not visible in Member Type drop down", "Representative option is not visible in Member Type drop down");
                Logger.Info("Representative option is not visible in Member Type dropdown");
            }
        }

        public void IsstudentInMemberTypeDropDown(string representative)
        {
            bool representativeDisplayed = Verify.IsElementVisibleFromListOfElementWithSoftAssertion(this.DriverContext, this.memberTypeMemberDetailsOptions, representative);
            if (representativeDisplayed)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + representative + " option is visible in Member Type drop down", representative + " option is visible in Member Type drop down");
                Logger.Info(representative + " option is visible in Member Type dropdown");
                throw new Exception(representative + " option is visible in Member Type dropdown");
            }
            else
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify  " + representative + " option is not visible in Member Type drop down", representative + " option is not visible in Member Type drop down");
                Logger.Info(representative + " option is not visible in Member Type dropdown");
            }
        }

        public void IsOrganizationInMemberTypeDropDown(string organization)
        {
            bool organizationDisplayed = Verify.IsElementVisibleFromListOfElementWithSoftAssertion(this.DriverContext, this.memberTypeMemberDetailsOptions, organization);
            if (organizationDisplayed)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Organization option is not visible in Member Type drop down", "Organization option is visible in Member Type drop down");
                Logger.Info("Organization option is visible in Member Type dropdown");
                throw new Exception("Organization option is visible in Member Type dropdown");
            }
            else
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Organization option is not visible in Member Type drop down", "Organization option is not visible in Member Type drop down");
                Logger.Info("Organization option is not visible in Member Type dropdown");
            }
        }

        public void IsUserAbleToViewAllStatusColumn(string name, string name1)
        {
            try
            {
                this.Driver.WaitUntilElementIsFound(this.statusColumn, BaseConfiguration.LongTimeout);
                IList<IWebElement> lstTableElements = this.Driver.GetElements(this.statusColumn);
                bool exist = lstTableElements.All(x => x.Text == name || x.Text == name1);
                if (exist == true)
                {
                    name = name + "," + name1;
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + name + " is visible under the User column ", name + " is visible successfully under status Column");
                    Logger.Info(name + " is visible successfully under status Column");
                }
                else
                {
                    Assert.IsFalse(exist);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + name + " is visible with in provided time " + BaseConfiguration.LongTimeout, "An exception occurred waiting for " + name + " to become visible");
            }
        }

        public void IsUserAbleToViewPersonalDetailsTab()
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.personalDetailsTab, this.nmpersonalDetailsTab, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.personalDetailsTab, this.nmpersonalDetailsTab);
        }

        public void IsUserAbleToViewASTMGeneralInformationSection()
        {
            this.Driver.ScrollToWebElement(this.aSTMGeneralInfoHeader);
            this.Driver.WaitUntilElementIsFound(this.aSTMGeneralInfoHeader, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.aSTMGeneralInfoHeader, this.nmpersonalDetailsTab);
        }

        public void GetvalIsUserAbleToViewRequiredFieldsInAstmGeneralInfoidate(string fieldLabelNames)
        {
            try
            {
                this.Driver.WaitUntilElementIsFound(this.aSTMGeneralInfoLabel, BaseConfiguration.LongTimeout);
                IList<IWebElement> lstTableElements = this.Driver.GetElements(this.aSTMGeneralInfoLabel);
                string[] items = fieldLabelNames.Split(',');
                foreach (string s in items)
                {
                    bool exist = lstTableElements.Any(x => x.Text == s);
                    Assert.True(exist);
                }

                Logger.Info(fieldLabelNames + " are displayed succesully");
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + fieldLabelNames + " are displayed ", fieldLabelNames + " are displayed successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("An exception occured while waiting for the fields to get displayed " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + fieldLabelNames + " are displayed in provided time " + BaseConfiguration.LongTimeout, "An exception occurred waiting for " + fieldLabelNames + " to get displayed");
            }
        }

        public void IsUserAbleToValidateMemberTypeFieldInStudentMemberType(string selectedMemberType, string representative, string organization)
        {
            try
            {
                var element = this.Driver.GetElement(this.memberTypeLabelText);
                if (element.TagName == "label")
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Member Type is Label", "Member Type is Label verified successfully");
                    Logger.Info("Member Type is Label verified successfully");
                }
                else
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Member Type is Label", "Member Type is not a Label");
                    Logger.Info("Member Type is not a Label");
                    throw new Exception("Member Type is not a Label");
                }

                this.Driver.WaitUntilElementIsFound(this.memberTypeDropDownVerify, BaseConfiguration.LongTimeout);
                var elementField = this.Driver.GetElement(this.memberTypeDropDownVerify);
                if (elementField.TagName == "div")
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Field Type is a Dropdown field", "Field Type is a Dropdown field verified successfully");
                    Logger.Info("Field Type is a Dropdown field verified successfully");
                }
                else
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Field Type is a Dropdown field", "Field Type is not a Dropdown field");
                    Logger.Info("Field Type is not a Dropdown field");
                    throw new Exception("Field Type is not a Dropdown field");
                }

                this.Driver.WaitUntilElementIsFound(this.memberTypeByDefaultInMemberDetailsPage, BaseConfiguration.LongTimeout);
                string defaultMemberTypeSelected = this.Driver.GetText(this.memberTypeByDefaultInMemberDetailsPage);
                if (selectedMemberType == defaultMemberTypeSelected)
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify Member Type Field is  a Mandatory field", "Member Type Field is  a Mandatory field  verified successfully");
                    Logger.Info("Member Type Field is a Mandatory field verified successfully");
                }
                else
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify Member Type Field is  a Mandatory field", "Member Type Field is not a Mandatory field");
                    Logger.Info("Member Type Field is not a Mandatory field");
                }

                this.IsUserIsAbleToClickOnMemberTypeInMemberDetailsPage();
                this.IsRepresentativeInMemberTypeDropDown(representative);
                this.IsOrganizationInMemberTypeDropDown(organization);
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Exception occured while Validating Member Type Field");
                Logger.Error("Exception occured while Validating Member Type Field : " + ex.ToString());
            }
        }

        public IList<IWebElement> IsUserAbleToGetMemberTypeDropDownOptionsList()
        {
            this.Driver.WaitUntilElementIsFound(this.memberTypeMemberDetailsOptions, BaseConfiguration.LongTimeout);
            IList<IWebElement> itemsMemberTypeDropDown = this.Driver.GetElements(this.memberTypeMemberDetailsOptions);
            int memberTypeCount = itemsMemberTypeDropDown.Count();
            return itemsMemberTypeDropDown;
        }

        public void ValidateMemberTypeDropDownOptionsDerivedFromMembershipTypesInRulesAndExceptions(IList<IWebElement> membertypesfrominternalStaff)
        {
            try
            {
                int memberTypeCount = membertypesfrominternalStaff.Count();
                IList<IWebElement> itemsMembershipTypes = this.Driver.GetElements(this.membershipTypesRulesOptions);

                // Excluding Representative and Organization Count from List as they are not included in Member Type drop down
                int membershipTypesCount = itemsMembershipTypes.Count() - 2;
                if (memberTypeCount == membershipTypesCount)
                {
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "Member Type DropDown Options are Derived from Membership Types In Rules And Exceptions, Validation Successful");
                    Logger.Error("Member Type DropDown Options are Derived from Membership Types In Rules And Exceptions,Validation Successful");
                }
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Member Type DropDown Options not Derived from Membership Types In Rules And Exceptions");
                Logger.Error("Member Type DropDown Options not Derived from Membership Types In Rules And Exceptions");
                throw new Exception("Member Type DropDown Options not Derived from Membership Types In Rules And Exceptions");
            }
        }

        public void IscommunicationLogUserNameVisibleByIndexMatchingWithLogUser(string index, string expected)
        {
            this.Driver.IsElementVisible(this.communicationLogUser.Format(index), this.nmcommunicationLogUser);
            this.Driver.IsExpectedTextMatchWithActualText(this.communicationLogUser.Format(index), expected);
        }

        public void IsMemberTypeRepresentative(string expected)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.memberTypeField.Format(expected), expected);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.memberTypeField.Format(expected), expected);
        }

        public void IsMemberTypeRepresentativeBlank()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.memberTypeFieldlink, "Representative");
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.memberTypeFieldlink, string.Empty);
        }

        public void IsUserAbleToClickOnRepresentative()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.memberTypeFieldlink, this.nmrepresentaive);
            this.Driver.IsElementClickable(this.memberTypeFieldlink, this.nmrepresentaive);
        }

        public void IsUserAbleToClickOnAssociatedOrganizationInMemberType()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.organizationlink, this.nmorganizationName);
            this.Driver.MouseOverOnWebElementAndClick(this.organizationlink, this.nmorganizationName);
        }

        public void IsOrganizationNameMatching(string expected)
        {
            var text = this.Driver.GetText(this.organizationField.Format(expected));
            string pattern = @"\d+$";
            string replacement = "";
            Regex reg = new Regex(pattern);
            string result = reg.Replace(text, replacement).Trim();
            string verifyMessage = "To verify  " + expected + "Matching " + result + "with " + expected;
            string passMessage = "Matching " + result + "with " + expected;
            string failMessage = "Error occcured while Matching" + result + "with " + expected;
            Verify.That(this.DriverContext, () => Assert.AreEqual(result, expected), verifyMessage, passMessage, failMessage);
        }

        public string IsOrganizationMatchingAccount(string expected)
        {
            var elements = this.Driver.GetText(this.organizationField.Format(expected));
            string text = Regex.Replace(elements, "[^0-9.]", "").Trim('.');
            return text;
        }

        ////public void IscommunicationLogDescriptionVisible(string expected, int index)
        ////{
        ////    string text = this.Driver.GetTextForSelectedElementfromList(this.getIndexLogText, string.Format(this.nmlogText, expected), index);
        ////    Assert.AreEqual(expected, text, "Expected text is not matching with return text from element");
        ////}

        public void IscommunicationLogDescriptionVisibleSoftAssertion(string expected, int index)
        {
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.getLogFirstIndex.Format(index), expected);
        }

        public void IscommunicationLogDescriptionVisibleInFirstIndex(string expected, int index)
        {
            string text = this.Driver.GetTextForSelectedElementfromList(this.getIndexLogText, string.Format(this.nmlogText, expected), index);
            Assert.AreEqual(expected, text, "Expected text is not matching with return text from element");
        }

        public void IscommunicationLogDescriptionVisibleInCancelButtronClick(string expected, int index)
        {
            try
            {
                string text = this.Driver.GetTextForSelectedElementfromList(this.getIndexLogText, string.Format(this.nmlogText, expected), index);
                Assert.AreNotEqual(expected, text, "Expected text should not match with return text from element");
            }
            catch (AssertionException ae)
            {
                Logger.Error("Failed to find on " + expected + "Due to exception: " + ae.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + expected + " text ", "An exception occurred while clicking on " + expected);
                throw;
            }
        }

        public void IscommunicationTagUserLogDescriptionVisible(bool isactive)
        {
            if (isactive == false)
            {
                Assert.IsFalse(this.Driver.IsElementPresentOrNot(this.taggeduserInput, this.nmtaggedUserInInput, string.Empty));
            }
            else
            {
                Assert.IsTrue(this.Driver.IsElementPresentOrNot(this.taggeduserInput, this.nmtaggedUserInInput, string.Empty));
            }
        }

        public void IsUserIsAbleToClickEditCommunicationLog()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            var by = this.editCommuncationLog.ToBy();
            var webElement = this.Driver.FindElement(by);
            this.Driver.JavaScriptClick(webElement, this.nmEditButtonCommuncationLog);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserAbleToEnterTextInEditCommuncationLog(string communicationLog)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmEditinputCommuncationLog);
            this.Driver.WaitUntilElementIsFound(this.editinputCommuncationLog, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.editinputCommuncationLog, this.nmEditinputCommuncationLog);
            this.Driver.EnterText(this.editinputCommuncationLog, communicationLog, this.nmEditinputCommuncationLog);
        }

        public void IsUserIsAbleToClickEditCommunicationLogSubmitButtron()
        {
            this.Driver.WaitUntilElementIsFound(this.editSubmitButton, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.editSubmitButton);
            this.Driver.JavaScriptClick(webElement, this.nmsubmitBtnCommuncationLog);
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
        }

        public void IsUserIsAbleToClickEditCommunicationLogCancelButtron()
        {
            this.Driver.WaitUntilElementIsFound(this.editCancelBtnCommuncationLog, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.editCancelBtnCommuncationLog);
            this.Driver.JavaScriptClick(webElement, this.nmCancelButton);
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
        }

        public void IsEditDeleteIConVisibleToNonLoggedUser(string expected)
        {
            string nonLoggedUser = string.Empty;
            try
            {
                this.Driver.WaitUntilElementIsFound(this.logUsersList, BaseConfiguration.LongTimeout);
                IList<IWebElement> lstTableElements = this.Driver.GetElements(this.logUsersList);
                foreach (IWebElement webElement in lstTableElements)
                {
                    if (webElement.Text != expected)
                    {
                        nonLoggedUser = webElement.Text;
                        Assert.IsFalse(this.Driver.IsElementPresentOrNot(this.editsubmitBtnEnabledLoginUser.Format(nonLoggedUser), string.Format(this.nmEditButtonVisibilityCommuncationLog, nonLoggedUser), string.Empty));
                    }
                }
            }
            catch (Exception)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Edit Icon Visible for non logged User :" + nonLoggedUser);
                Logger.Error("Edit Icon Visible for non logged User :" + nonLoggedUser);
                throw new Exception("Edit Icon Visible for non logged Users");
            }
        }

        public void IscommunicationLogUserNameIndexClicked(string index)
        {
            this.Driver.IsElementVisible(this.communicationLogUser.Format(index), this.nmcommunicationLogUser);
            this.Driver.IsElementClickable(this.communicationLogUser.Format(index), this.nmcommunicationLogUser);
        }

        public void IspageRedirectedtoDetailsPage(string expectedText)
        {
            this.Driver.WaitForPageLoad();
            string verifyMessage = "To verify user is redirected to  " + expectedText + " Page upon clicking member account number ";
            string passMessage = "User is redirected to " + expectedText + " Page successfully clicking member account number";
            string failMessage = "Error occcured while user is redirected to " + expectedText + " Page";
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            string actualHeadingText = this.Driver.GetText(this.redirectedPageName.Format(expectedText));
            Verify.That(this.DriverContext, () => Assert.AreEqual(actualHeadingText, expectedText), verifyMessage, passMessage, failMessage);
        }

        public void IsemptyCommunicationTextLogDisplay(string expectedfilename)
        {
            this.Driver.IsElementVisible(this.emptyCommunicationlogText, this.nmemptyCommunicationlogText);
            string text = this.Driver.GetText(this.emptyCommunicationlogText);
            if (text.Contains(expectedfilename))
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To Verify Comminucation logs present or not ", "Expected Text Found  : " + expectedfilename);
            }
            else
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To Verify Comminucation logs are not present ", "Expected Text not Found : " + expectedfilename + " , Logs available");
            }
        }

        public void IsGetErrorMessageForHistoricalReasonField(string errorMsg)
        {
            Verify.ValiadteTextPresentOnElementWithSoftAssertion(this.DriverContext, this.errorMsgHistoricalReason, this.nmHistoricalResaonErrorMsg, errorMsg);
        }

        public void IsUserAbleToClickOnMemberTypeDropDown()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.editMemberTypeDropdown, this.nmmemberTypeDropdownicon);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.editMemberTypeDropdown, this.nmmemberTypeDropdownicon);
        }

        public void IsUserAbleToClickOnMouContactDropDown()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.mouContactDropdown, this.nmmouContactDropdown);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.mouContactDropdown, this.nmmouContactDropdown);
        }

        public void IsUserAbleToClickOnMemberTypeSelectInDropdown(string name)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.accountStatusSelectActiveInDropDown.Format(name), string.Format(this.nmaccountStatusSelectActiveInDropDown, name + " in "));
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.accountStatusSelectActiveInDropDown.Format(name), string.Format(this.nmaccountStatusSelectActiveInDropDown, name + " in "));
        }

        public void IsViewStudentInformationClickable()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.viewStudentInfo, this.nmviewStudentInformationlink);
            ////Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.viewStudentInfo, BaseConfiguration.LongTimeout);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.viewStudentInfo, this.nmviewStudentInformationlink);
        }

        public void ValidateDataInStudentInformationPopup(string major)
        {
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.major.Format(major), major);
        }

        public void IsUserAbleToViewTextInThePopoup(string expected)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.popupText.Format(expected), expected, BaseConfiguration.LongTimeout);
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.popupText.Format(expected), expected);
        }

        public void IsUserAbleToClickOnMOUContactSelectInDropdown(string name)
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.accountStatusSelectActiveInDropDown.Format(name), string.Format(this.nmaccountStatusSelectActiveInDropDown, name + " in "));
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.accountStatusSelectActiveInDropDown.Format(name), string.Format(this.nmaccountStatusSelectActiveInDropDown, name + " in "));
        }

        public void IsUserAbleToClickTextInPopup(string name)
        {
            this.Driver.IsElementClickable(this.updateAccountStatus.Format(name), name);
        }

        public void IsUserAbleToClickOnOkButtonInPopUp()
        {
            Verify.IsElementVisibleWithSoftAssertion(this.DriverContext, this.okButtonInPopup, this.nmokButton);
            Verify.IsElementClickableWithSoftAssertion(this.DriverContext, this.okButtonInPopup, this.nmokButton);
        }

        public void IsNewMembershipTypeVisibleinTheList(string membershipTypeName)
        {
            bool membershipTypeDisplayed = this.Driver.IsElementVisibleFromListOfElement(this.memberTypeMemberDetailsOptions, membershipTypeName);
            if (membershipTypeDisplayed)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify " + membershipTypeName + " membershipType is visible in Member Type drop down list ", membershipTypeName + " Membertype is visible in Member Type drop down list ");
                Logger.Info(membershipTypeName + " MembershipType  is visible in Member Type dropdown List");
            }
            else
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify " + membershipTypeName + " membershipType is visible in Member Type drop down list ", membershipTypeName + " Membertype is not visible in Member Type drop down list");
                Logger.Info(membershipTypeName + " MembershipType  is not visible in Member Type dropdown List");
                throw new Exception(membershipTypeName + " MembershipType  is not visible in Member Type dropdown List");
            }
        }

        public void GetMembershipNameExistsInDB(string membershipName, string compareText, string message)
        {
            this.Driver.GetSingleValueFromDBCompareWithExpectedValue(string.Format(SqlQuery.FunctionalAddMembershipTypeInIntenalAppExists, membershipName), compareText, message);
        }

        public void IsUserAbleToViewTextInMemberAccountStatusDetails(string expected)
        {
            Verify.WaitUntilElementIsFoundWithSoftAssertion(this.DriverContext, this.memberAccountStatus.Format(expected), expected, BaseConfiguration.LongTimeout);
            Verify.IsExpectedTextMatchWithActualTextWithSoftAssertion(this.DriverContext, this.memberAccountStatus.Format(expected), expected);
        }
    }
}
