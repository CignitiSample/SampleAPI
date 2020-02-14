// <copyright file="InternalStaffOrganizationAccountsPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Tests.PageObjects.PageObjects.MCS
{
    using System;
    using System.IO;
    using System.Threading;
    using global::MCS.Test.Automation.Common;
    using global::MCS.Test.Automation.Common.Extensions;
    using global::MCS.Test.Automation.Common.Helpers;
    using global::MCS.Test.Automation.Common.Types;
    using global::MCS.Test.Automation.Tests.PageObjects;
    using NLog;
    using NUnit.Framework;
    using RelevantCodes.ExtentReports;

    public class InternalStaffOrganizationAccountsPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ElementLocator
           organizationAccountsDropdownXPath = new ElementLocator(Locator.XPath, "//div[@class='menuWrapper']//a[text()='Organizational Accounts']");

        private readonly ElementLocator
          subMenuMemberManagement = new ElementLocator(Locator.XPath, "//div[@class='menuWrapper']//ul[@class='subMenu'][1]//a[text()='{0}']");

        private readonly ElementLocator
         organizationHeader = new ElementLocator(Locator.XPath, "//h2[text()='Organizational Accounts']");

        private readonly ElementLocator
           organizationdropdownInSearch = new ElementLocator(Locator.XPath, "//div[text()='Member']");

        private readonly ElementLocator
         organizationAccount = new ElementLocator(Locator.XPath, "(//a[@class='column--Account'])[1]");

        private readonly ElementLocator
         organizationName = new ElementLocator(Locator.XPath, "(//p[@class='column--OrganizationName'])[1]");

        private readonly ElementLocator
         organizationNameInBanner = new ElementLocator(Locator.XPath, "//span[@class='memberName ellip']");

        private readonly ElementLocator
         accountTypeInBanner = new ElementLocator(Locator.XPath, "//span[@class='roleTypenName']");

        private readonly ElementLocator
            representativeFirstName = new ElementLocator(Locator.XPath, "//input[@name='FirstName']");

        private readonly ElementLocator
          representativeLastName = new ElementLocator(Locator.XPath, "//input[@name='LastName']");

        private readonly ElementLocator
          resetButton = new ElementLocator(Locator.XPath, "//button[text()='Reset']");

        private readonly ElementLocator
         accountInBanner = new ElementLocator(Locator.XPath, "//span[@class='memberAccount']");

        private readonly ElementLocator
        joinDateInBanner = new ElementLocator(Locator.XPath, "//div[@class='memberRelatedInfo']//span[text()='Join Date']");

        private readonly ElementLocator
         paidDateInBanner = new ElementLocator(Locator.XPath, "//div[@class='memberRelatedInfo']//span[text()='Paid Date']");

        private readonly ElementLocator
         addressInBanner = new ElementLocator(Locator.XPath, "//div[@class='memberRelatedInfo']//span[text()='Address']");

        private readonly ElementLocator
         paidStatusInBanner = new ElementLocator(Locator.XPath, "//span[@class='paidStatus']");

        private readonly ElementLocator
         accountStatusInBanner = new ElementLocator(Locator.XPath, "//div[@class='memberRelatedInfo']//span[text()='Account Status']");

        private readonly ElementLocator
         representativeInBanner = new ElementLocator(Locator.XPath, "//div[@class='ui vertically divided four column grid']//span[text()='Representative']");

        private readonly ElementLocator
         lastUpdatedInBanner = new ElementLocator(Locator.XPath, "//span[@class='lastUpdated']");

        private readonly ElementLocator
         accountNumberInPersonalDetails = new ElementLocator(Locator.XPath, "//span[text()='Account Number']");

        private readonly ElementLocator
         nameInPersonalDetails = new ElementLocator(Locator.XPath, "//span[text()='Name']");

        private readonly ElementLocator
        joinDateInPersonalDetails = new ElementLocator(Locator.XPath, "//div[@class='memberPersonalDetail']//span[text()='Join Date']");

        private readonly ElementLocator
         webURLInPersonalDetails = new ElementLocator(Locator.XPath, "//div[@class='memberPersonalDetail']//span[text()='Web URL']");

        private readonly ElementLocator
         addressInPersonalDetails = new ElementLocator(Locator.XPath, "//div[@class='memberPersonalDetail']//span[text()='Address']");

        private readonly ElementLocator
         copyInPersonalDetails = new ElementLocator(Locator.XPath, "//button[@class='ui button secondary']");

        private readonly ElementLocator
         emailInPersonalDetails = new ElementLocator(Locator.XPath, "//div[@class='memberPersonalDetail']//span[text()='Email']");

        private readonly ElementLocator
         phoneInPersonalDetails = new ElementLocator(Locator.XPath, "//div[@class='memberPersonalDetail']//span[text()='Phone']");

        private readonly ElementLocator
         faxInPersonalDetails = new ElementLocator(Locator.XPath, "//div[@class='memberPersonalDetail']//span[text()='Fax']");

        private readonly ElementLocator
         membertypeInAstmGeneralInfo = new ElementLocator(Locator.XPath, "//div[@class='memberPersonalDetail']//span[text()='Member Type']");

        private readonly ElementLocator
         representativeInAstmGeneralInfo = new ElementLocator(Locator.XPath, "//div[@class='memberPersonalDetail']//span[text()='Representative']");

        private readonly ElementLocator
         feeGroupInAstmGeneralInfo = new ElementLocator(Locator.XPath, "//div[@class='memberPersonalDetail']//span[text()='Fee Group']");

        private readonly ElementLocator
         accountStatusInAstmGeneralInfo = new ElementLocator(Locator.XPath, "//h5[text()='Member Account Status Details']//..//span[text()='Account Status']");

        private readonly ElementLocator
         paidStatusInAstmGeneralInfo = new ElementLocator(Locator.XPath, "//h5[text()='Member Account Status Details']//..//span[text()='Paid Status']");

        private readonly ElementLocator
         paidDateInAstmGeneralInfo = new ElementLocator(Locator.XPath, "//h5[text()='Member Account Status Details']//..//span[text()='Paid Date']");

        private readonly ElementLocator
         searchBarDropDown = new ElementLocator(Locator.XPath, "//i[@class='dropdown icon openAdvSearch']");

        private readonly ElementLocator
         searchBarDropDownPopupInput = new ElementLocator(Locator.XPath, "//input[@name='organizationName']");

        private readonly ElementLocator
        searchBarDropDownPopupInputAutoSuggestive = new ElementLocator(Locator.XPath, "//div[contains(@class,'title')]");

        private readonly ElementLocator
        popupDiv = new ElementLocator(Locator.XPath, "//div[@class='advanced-search']");

        private readonly ElementLocator
        manageMemberMenu = new ElementLocator(Locator.XPath, "div.menuWrapper ul li a.active");

        private readonly ElementLocator
        btnIconWord = new ElementLocator(Locator.XPath, "//button[@class='ui button secondary ']");

        private readonly ElementLocator
       btnLinkWord = new ElementLocator(Locator.XPath, "//i[@class='icon file word mr5']");

        private readonly ElementLocator
       btnCommunationLogPopupMessage = new ElementLocator(Locator.XPath, "(//div[@class='content'])[12]");

        private readonly ElementLocator
                  tabCommuncationLog = new ElementLocator(Locator.XPath, "//a[@class='item'][contains(.,'Communication Log')]");

        private readonly ElementLocator
        inputCommuncationLog = new ElementLocator(Locator.XPath, "//div[@class='commentposteditor']");

        private readonly ElementLocator
        submitBtnCommuncationLog = new ElementLocator(Locator.XPath, "//button[@class='ui primary button mr10']");

        private readonly ElementLocator
        cancelBtnCommuncationLog = new ElementLocator(Locator.XPath, "//button[@class='ui button cancel mr0']");

        private readonly ElementLocator
       removeCommuncationLog = new ElementLocator(Locator.XPath, "(//i[contains(@class,'delete icon')])[1]");

        private readonly ElementLocator
       communcationlogPopupOkButtron = new ElementLocator(Locator.XPath, "(//button[@class='ui primary button'])[6]");

        private readonly ElementLocator
       communcationlogPopupNoButtron = new ElementLocator(Locator.XPath, "(//button[@class='ui button'])[6]");

        private readonly ElementLocator
       dimmerVisible = new ElementLocator(Locator.XPath, "//div[@class='ui active transition visible dimmer']");

        private readonly ElementLocator
       msgSuccess = new ElementLocator(Locator.XPath, "//p[contains(.,'Comment added successfully.')]");

        private readonly ElementLocator
      logText = new ElementLocator(Locator.XPath, "//div[@class='text'][contains(.,'{0}')]");

        private readonly ElementLocator
        updateSuccessMessage = new ElementLocator(Locator.CssSelector, "div.content > p");

        private readonly ElementLocator
      allcommunicationLogDateTime = new ElementLocator(Locator.XPath, "//div[@class='metadata']//div");

        private readonly ElementLocator
    communicationLogUser = new ElementLocator(Locator.XPath, "(//a[@class='author'])[{0}]");

        private readonly ElementLocator
    communicationLogDateTime = new ElementLocator(Locator.XPath, "(//div[@class='metadata']/div)[{0}]");

        private readonly ElementLocator
      getIndexLogText = new ElementLocator(Locator.XPath, "//div[@class='content']//div[@class='text']");

        private readonly ElementLocator
        emptyCommunicationlogText = new ElementLocator(Locator.XPath, "//div[@class='ui minimal comments']");

        private readonly ElementLocator
        loginUserName = new ElementLocator(Locator.XPath, "//span[contains(@class,'maxName ellip')]");

        private readonly ElementLocator
        redirectedPageName = new ElementLocator(Locator.XPath, "//div[@class='active section']");

        private readonly ElementLocator
       taggeduserlist = new ElementLocator(Locator.XPath, "//div[@class='tribute-container']//li[1]");

        private readonly ElementLocator
       istaggedusersaved = new ElementLocator(Locator.XPath, " //a[contains(@title,'{0}')]");

        private readonly ElementLocator
      viewReprentativeHistory = new ElementLocator(Locator.XPath, "//a[@class='linkTxt'][contains(.,'{0}')]");

        private readonly ElementLocator
      viewRepresntativeHistoryPopupHeader = new ElementLocator(Locator.XPath, "//div[@class='header'][contains(.,'{0}')]");

        private readonly ElementLocator
      viewRepresntativeHistoryAccountNo = new ElementLocator(Locator.CssSelector, "table.customTable.fixHeadertable tbody tr td a");

        private readonly ElementLocator
      viewRepresntativeHistoryAccountNoInfoInMemberDetailsPage = new ElementLocator(Locator.XPath, "//span[@class='memberAccount']");

        private readonly ElementLocator
      viewRepresntativeHistoryPopupFields = new ElementLocator(Locator.XPath, "//th[text()='{0}']");

        private string nmviewReprentativeHistory = "View Reprentative History";
        private string nmviewRepresntativeHistoryPopupFields = "View Represntative History Popup Fields";
        private string nmviewRepresntativeHistoryPopupHeader = "View Represntative History Popup Header";
        private string nmviewRepresntativeHistoryAccountNo = "View Represntative History Account No";
        private string nmviewRepresntativeHistoryAccountNoInfoInMemberDetailsPage = "View Represntative History Account No in Member Details Page";
        private string nmtaggeduserlists = "Communication Log - Tag Users List";
        private string nmtaggeduserlist = "Communication Log - Tag User :' {0} ' in Comment";
        private string nmSubmitButton = "Submit Button";
        private string nmCancelButton = "Cancel Button";
        private string nmredirectedPageName = "Page Name : {0}";
        private string nmemptyCommunicationlogText = "Empty Communication Log";
        private string nmlogText = "log Text  : {0}";
        private string nmbtnCommunationLogPopupMessage = "Communcation Log Popup Message";
        private string nminputCommuncationLog = "Communcation Log Input";
        private string nmupdateMessageCommunicationLog = "Communication Log Message : {0}";
        private string nmbtnIconWord = "Word Icon Button";
        private string nmtab = "Communication Log tab";
        private string nmdeleteicon = "Communication Log delete icon";
        private string nmsubMenuMemberManagement = "Sub Menu : {0}";
        private string nmsearchBarDropDownPopupInputAutoSuggestive = "Organization Name Auto Suggestive";
        private string nmsearchBarDropDownPopupInput = "Search Bar Popup Organization Name";
        private string nmbtnLinkWord = "Word Icon Link";
        private string nmcommuncationlogPopupOkButton = "CommunicationLog Ok Button";
        private string nmcommuncationlogPopupNoButton = "CommunicationLog No Button";
        private string nmPopupDiv = "Organization Popup";
        private string nmsearchBarDropDown = "Search Bar DropDown";
        private string nmorganizationAccounts = "Organization Accounts";
        private string nmorganizationAccount = "Organization Account";
        private string nmaccountType = "Account Type";
        private string nmresetBtn = "Reset Button in Advance Search Popup";
        private string nmjoinDateInBanner = "join Date In Banner";
        private string nmpaidDateInBanner = "paid Date In Banner";
        private string nmaddressInBanner = "address In Banner";
        private string nmpaidStatusInBanner = "paid Status In Banner";
        private string nmaccountStatusInBanner = "account Status In Banner";
        private string nmrepresentativeInBanner = "Representative In Banner";
        private string nmaccountNumber = "Account Number";
        private string nmrepresentativeFirstName = "Representative First Name";
        private string nmrepresentativeLastName = "Representative Last Name";
        private string nmlastUpdated = "Account Number";
        private string nmaccountNumberInPersonalDetails = "Account Number In Personal Details";
        private string nmjoinDateInPersonalDetails = "Join Date In Personal Details";
        private string nmnameInPersonalDetails = "Name In Personal Details";
        private string nmaddressInPersonalDetails = "Address In Personal Details";
        private string nmwebURLInPersonalDetails = "WebURL In Personal Details";
        private string nmcopyInPersonalDetails = "copy In Personal Details";
        private string nmemailInPersonalDetails = "Email In Personal Details";
        private string nmphoneInPersonalDetails = "Phone In Personal Details";
        private string nmfaxInPersonalDetails = "Fax In Personal Details";
        private string nmmembertypeInAstmGeneralInfo = "Member Type In Astm General Info";
        private string nmrepresentativeInAstmGeneralInfo = "Representative In Astm General Info";
        private string nmfeeGroupInAstmGeneralInfo = "Fee Grooup In Astm General Info";
        private string nmaccountStatusInAstmGeneralInfo = "Account Status In Astm General Info";
        private string nmpaidStatusInAstmGeneralInfo = "Paid Status In Astm General Info";
        private string nmpaidDateInAstmGeneralInfo = "Paid Date In Astm General Info";
        private string nmallcommunicationLogDateTime = "Communication Log Dates";
        private string nmcommunicationLogUser = "Communication Log User Name";
        private string nmdimmerloading = "dimmer loading";
        private string nmcommunicationLogDateTime = "Communication Log DateTime";

        public InternalStaffOrganizationAccountsPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public void IsViewReprentativeHistoryClicked(string name)
        {
            this.Driver.IsElementVisible(this.viewReprentativeHistory.Format(name), this.nmviewReprentativeHistory);
            var webElement = this.Driver.GetElement(this.viewReprentativeHistory.Format(name));
            this.Driver.JavaScriptClick(webElement, this.nmviewReprentativeHistory);
        }

        public void IsviewRepresntativeHistoryPopupFields(string name)
        {
            this.Driver.IsElementVisible(this.viewRepresntativeHistoryPopupFields.Format(name), this.nmviewRepresntativeHistoryPopupFields);
        }

        public void IsviewRepresntativeHistoryPopupHeader(string name)
        {
            this.Driver.IsElementVisible(this.viewRepresntativeHistoryPopupHeader.Format(name), this.nmviewRepresntativeHistoryPopupHeader);
        }

        public string IsViewRepresntativeHistoryAccountNoClicked()
        {
            this.Driver.IsElementVisible(this.viewRepresntativeHistoryAccountNo, this.nmviewRepresntativeHistoryAccountNo);
            string getaccno = this.Driver.GetText(this.viewRepresntativeHistoryAccountNo);
            this.Driver.IsElementClickable(this.viewRepresntativeHistoryAccountNo, this.nmviewRepresntativeHistoryAccountNo);
            return getaccno;
        }

        public void IsViewRepresntativeHistoryAccountNoClicked(int index)
        {
            this.Driver.WaitUntilElementIsFound(this.viewRepresntativeHistoryAccountNo, 90);
            this.Driver.IsElementClickableBasedonIndex(this.viewRepresntativeHistoryAccountNo, this.nmviewRepresntativeHistoryAccountNo, 0);
        }

        public string GetAccountNoFromHistory(int i)
        {
            var actualapplication = this.Driver.GetTextFromListOfElementsBasedOnIndex(this.viewRepresntativeHistoryAccountNo, this.nmviewRepresntativeHistoryAccountNo, i);
            return actualapplication;
        }

        public void IsviewRepresntativeHistoryAccountNoInfoInMemberDetailsPage(string expected)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.IsElementVisible(this.viewRepresntativeHistoryAccountNoInfoInMemberDetailsPage, this.nmviewRepresntativeHistoryAccountNoInfoInMemberDetailsPage);
            this.Driver.IsExpectedTextMatchWithActualText(this.viewRepresntativeHistoryAccountNoInfoInMemberDetailsPage, "Account Number: " + expected, string.Empty);
        }

        public void IscommunicationLogDateTimeSortedInDecending()
        {
            this.Driver.AreElementsSortedInorderForDateTime(this.allcommunicationLogDateTime, this.nmallcommunicationLogDateTime, "desc", "DateTime", "at, • Edited");
        }

        public void IscommunicationLogUserNameVisibleByIndex(string index)
        {
            this.Driver.IsElementVisible(this.communicationLogUser.Format(index), this.nmcommunicationLogUser);
        }

        public void VerifycommunicationLogUserNameVisibleByIndexMatchingWithLoginUser(string index, string expected)
        {
            this.Driver.IsElementVisible(this.communicationLogUser.Format(index), this.nmcommunicationLogUser);
            this.Driver.IsExpectedTextMatchWithActualText(this.communicationLogUser.Format(index), expected);
        }

        public void IscommunicationLogUserNameIndexClicked(string index)
        {
            this.Driver.IsElementVisible(this.communicationLogUser.Format(index), this.nmcommunicationLogUser);
            this.Driver.IsElementClickable(this.communicationLogUser.Format(index), this.nmcommunicationLogUser);
        }

        public void IspageRedirectedtoUserDetailsPage(string expected)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
            this.Driver.IsElementVisible(this.redirectedPageName.Format(expected), string.Format(this.nmredirectedPageName, expected));
            this.Driver.IsExpectedTextMatchWithActualText(this.redirectedPageName.Format(expected), expected);
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

        public void IsExportWordFileName(string expectedfilename)
        {
            this.Driver.WaitForPageLoad();
            Thread.Sleep(3000);
            try
            {
                string filePath = TestContext.CurrentContext.TestDirectory + "\\" + BaseConfiguration.DownloadFolder;
                int getFileCount = FilesHelper.GetFilesOfGivenType(filePath, FileType.Docx).Count;
                if (getFileCount <= 0)
                {
                    Assert.IsTrue(false, "Unable to download word file after clicking export button");
                }
                else
                {
                    string[] files = Directory.GetFiles(filePath);
                    string filename = Path.GetFileName(files[0]);
                    Assert.AreEqual(filename, expectedfilename);
                    Logger.Error("File downloaded , word file name : " + filename);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "File downloaded , word file name : " + filename);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while downloading word file : " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Exception occured while downloading word file");
            }
        }

        public void IsExportWordFileDownloaded()
        {
            this.Driver.WaitForPageLoad();
            Thread.Sleep(3000);
            try
            {
                string filePath = TestContext.CurrentContext.TestDirectory + "\\" + BaseConfiguration.DownloadFolder;
                int getFileCount = FilesHelper.GetFilesOfGivenType(filePath, FileType.Docx).Count;
                if (getFileCount <= 0)
                {
                    Assert.IsTrue(false, "Unable to download word file after clicking export button");
                }
                else
                {
                    string[] files = Directory.GetFiles(filePath);
                    string filename = Path.GetFileName(files[0]);
                    Logger.Error("File downloaded , word file name : " + filename);
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "File downloaded , word file name : " + filename);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while downloading word file : " + ex.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Exception occured while downloading word file");
            }
        }

        public void IsSearchBarOrganisationNameAutoSuggestiveVisible(string text)
        {
            this.Driver.IsElementVisible(this.searchBarDropDownPopupInput, this.nmsearchBarDropDownPopupInput);
            this.Driver.EnterText(this.searchBarDropDownPopupInput, text, this.nmsearchBarDropDownPopupInput);
            this.Driver.IsElementVisible(this.searchBarDropDownPopupInputAutoSuggestive, this.nmsearchBarDropDownPopupInputAutoSuggestive);
            string suggestiveText = this.Driver.GetText(this.searchBarDropDownPopupInputAutoSuggestive);
            if (!suggestiveText.Contains(text))
            {
                Assert.IsTrue(false);
            }
        }

        public void IsWordIconandTextDisplayed(string text)
        {
            this.Driver.IsElementVisible(this.btnIconWord, this.nmbtnIconWord);
            this.Driver.IsExpectedTextMatchWithActualText(this.btnIconWord, text);
            this.Driver.CheckElementPresentOrNot(this.btnLinkWord, this.nmbtnLinkWord, string.Empty);
        }

        public void IsUserIsAbleToClickSearchDropDownICon()
        {
            this.Driver.IsElementVisible(this.searchBarDropDown, this.nmsearchBarDropDown);
            this.Driver.IsElementClickable(this.searchBarDropDown, this.nmsearchBarDropDown);
        }

        public void IsUserIsAbleToSearchPopupVisible()
        {
            this.Driver.IsElementVisible(this.popupDiv, this.nmPopupDiv);
        }

        public void IsUserabletoViewSubMenuMember(string expected)
        {
            this.Driver.CheckElementPresentOrNot(this.subMenuMemberManagement.Format(expected), string.Format(this.nmsubMenuMemberManagement, expected), string.Empty);
        }

        public void IsUserabletoViewSubMenuOrganisation(string expected)
        {
            this.Driver.CheckElementPresentOrNot(this.subMenuMemberManagement.Format(expected), string.Format(this.nmsubMenuMemberManagement, expected), string.Empty);
        }

        public void IsUserIsAbleToOrganizationAccountsInDropDownList()
        {
            this.Driver.IsElementClickable(this.organizationAccountsDropdownXPath, this.nmorganizationAccounts);
        }

        public void IsUserIsAbleToViewOrganizationAccountsHeader(string name)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.organizationHeader, name);
        }

        public void IsUserAbleToViewOrganizationAccountInDropDownBesideSeachBox(string name)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.organizationdropdownInSearch, name);
        }

        public void IsUserAbleToViewRecordInDynamicGrid()
        {
            this.Driver.WaitForPageLoad();
            this.Driver.CheckElementPresentOrNot(this.organizationAccount, this.nmorganizationAccount, string.Empty);
        }

        public string IsUserIsAbleToSelectOrganizationAccount()
        {
            this.Driver.WaitUntilElementIsFound(this.organizationAccount, BaseConfiguration.LongTimeout);
            string actual = this.Driver.GetText(this.organizationAccount);
            this.Driver.IsElementClickable(this.organizationAccount, this.nmorganizationAccount);
            return actual;
        }

        public string IsUserIsAbleToGetOrganizationName()
        {
            string actual = this.Driver.GetText(this.organizationName);
            return actual;
        }

        public void IsUserIsAbleToViewOrganizatonDetailsPage(string name)
        {
            try
            {
                string title = this.Driver.Title;
                Assert.AreEqual(name, title);
                DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify whether title is matching " + name + ".", "Both the Titles are same");
                Logger.Info("Both Titles are same");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed as the values are differed - Due to exception: " + ex.ToString());

                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify whether Title Value is same ", "An exception occurred while verifying title value");
                throw;
            }
        }

        public void IsUserIsAbleToViewOrganizationNameInBanner(string name)
        {
            this.Driver.IsExpectedTextMatchWithActualText(this.organizationNameInBanner, name);
        }

        public void IsUserableToEnterTextInFirstNameField(string text)
        {
            this.Driver.IsElementClickable(this.representativeFirstName, this.nmrepresentativeFirstName);
            this.Driver.EnterText(this.representativeFirstName, text, this.nmrepresentativeFirstName);
        }

        public void IsUserableToEnterTextInLastNameField(string text)
        {
            this.Driver.IsElementClickable(this.representativeLastName, this.nmrepresentativeLastName);
            this.Driver.EnterText(this.representativeLastName, text, this.nmrepresentativeLastName);
        }

        public void ClickReset()
        {
            this.Driver.IsElementVisible(this.resetButton, this.nmresetBtn);
            this.Driver.IsElementClickable(this.resetButton, this.nmresetBtn);
        }

        public void VerifyAfterResetOperation()
        {
            Assert.IsTrue(this.Driver.IsElementPresentOrNot(this.representativeLastName, this.nmrepresentativeLastName, string.Empty));
            Assert.IsTrue(this.Driver.IsElementPresentOrNot(this.representativeFirstName, this.nmrepresentativeFirstName, string.Empty));
        }

        public void IsUserIsAbleToViewFieldsInBanner()
        {
            this.Driver.IsElementVisible(this.accountTypeInBanner, this.nmaccountType);
            this.Driver.IsElementVisible(this.accountInBanner, this.nmaccountNumber);
            this.Driver.IsElementVisible(this.joinDateInBanner, this.nmjoinDateInBanner);
            this.Driver.IsElementVisible(this.paidDateInBanner, this.nmpaidDateInBanner);
            this.Driver.IsElementVisible(this.addressInBanner, this.nmaddressInBanner);
            this.Driver.IsElementVisible(this.paidStatusInBanner, this.nmpaidStatusInBanner);
            this.Driver.IsElementVisible(this.accountStatusInBanner, this.nmaccountStatusInBanner);
            this.Driver.IsElementVisible(this.representativeInBanner, this.nmrepresentativeInBanner);
            this.Driver.IsElementVisible(this.lastUpdatedInBanner, this.nmlastUpdated);
        }

        public void IsUserIsAbleToViewFieldsInPersonalDetails()
        {
            this.Driver.WaitUntilElementIsFound(this.accountNumberInPersonalDetails, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.accountNumberInPersonalDetails, this.nmaccountNumberInPersonalDetails);
            this.Driver.IsElementVisible(this.nameInPersonalDetails, this.nmnameInPersonalDetails);
            this.Driver.IsElementVisible(this.joinDateInPersonalDetails, this.nmjoinDateInPersonalDetails);
            this.Driver.IsElementVisible(this.addressInPersonalDetails, this.nmaddressInPersonalDetails);
            this.Driver.IsElementVisible(this.webURLInPersonalDetails, this.nmwebURLInPersonalDetails);
            this.Driver.IsElementVisible(this.copyInPersonalDetails, this.nmcopyInPersonalDetails);
            this.Driver.IsElementVisible(this.emailInPersonalDetails, this.nmemailInPersonalDetails);
            this.Driver.IsElementVisible(this.phoneInPersonalDetails, this.nmphoneInPersonalDetails);
            this.Driver.IsElementVisible(this.faxInPersonalDetails, this.nmfaxInPersonalDetails);
        }

        public void IsUserIsAbleToViewFieldsInAstmGeneralInfo()
        {
            this.Driver.WaitUntilElementIsFound(this.membertypeInAstmGeneralInfo, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.membertypeInAstmGeneralInfo, this.nmmembertypeInAstmGeneralInfo);
            this.Driver.IsElementVisible(this.representativeInAstmGeneralInfo, this.nmrepresentativeInAstmGeneralInfo);
            this.Driver.IsElementVisible(this.feeGroupInAstmGeneralInfo, this.nmfeeGroupInAstmGeneralInfo);
            this.Driver.IsElementVisible(this.accountStatusInAstmGeneralInfo, this.nmaccountStatusInAstmGeneralInfo);
            this.Driver.IsElementVisible(this.paidStatusInAstmGeneralInfo, this.nmpaidStatusInAstmGeneralInfo);
            this.Driver.IsElementVisible(this.paidDateInAstmGeneralInfo, this.nmpaidDateInAstmGeneralInfo);
        }

        public void IsUserIsAbleToClickWordICon()
        {
            this.Driver.IsElementVisible(this.btnIconWord, this.nmbtnIconWord);
            this.Driver.IsElementClickable(this.btnIconWord, this.nmbtnIconWord);
        }

        public void IselementVisibleAfterDlete(string expected)
        {
            Assert.IsTrue(this.Driver.IsElementPresentOrNot(this.logText.Format(expected), string.Format(this.nmlogText, expected), string.Empty), "Unable to find Element Text :" + expected);
        }

        public void IsUserIsAbleToClickCommunicationLogTab()
        {
            this.Driver.WaitUntilElementIsFound(this.tabCommuncationLog, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.tabCommuncationLog);
            this.Driver.JavaScriptClick(webElement, this.nmtab);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserAbleToEnterTextInCommuncationLog(string communicationLog)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nminputCommuncationLog);
            this.Driver.WaitUntilElementIsFound(this.inputCommuncationLog, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.inputCommuncationLog, this.nminputCommuncationLog);
            this.Driver.EnterText(this.inputCommuncationLog, communicationLog, this.nminputCommuncationLog);
        }

        public void IsUserIsAbleToClickCommunicationLogSubmitButtron()
        {
            this.Driver.WaitUntilElementIsFound(this.submitBtnCommuncationLog, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.submitBtnCommuncationLog);
            this.Driver.JavaScriptClick(webElement, this.nmSubmitButton);
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nmdimmerloading);
        }

        public void IsUserIsAbleToClickcommuncationlogPopupOkButtron()
        {
            this.Driver.WaitUntilElementIsFound(this.communcationlogPopupOkButtron, BaseConfiguration.LongTimeout);
            var webElement = this.Driver.GetElement(this.communcationlogPopupOkButtron);
            this.Driver.JavaScriptClick(webElement, this.nmcommuncationlogPopupOkButton);
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
            this.Driver.IsElementVisible(this.updateSuccessMessage, string.Format(this.nmupdateMessageCommunicationLog, updateSuccessMessage));
            this.Driver.IsExpectedTextMatchWithActualText(this.updateSuccessMessage, updateSuccessMessage, string.Format(this.nmupdateMessageCommunicationLog, updateSuccessMessage));
        }

        public void IsUserIsAbleToClickRemoveCommunicationLog()
        {
            var by = this.removeCommuncationLog.ToBy();
            var webElement = this.Driver.FindElement(by);
            this.Driver.JavaScriptClick(webElement, this.nmdeleteicon);
            this.Driver.WaitForPageLoad();
        }

        public void IsUserAbletoViewMessageOnCommuncationLogPOpup(string updateSuccessMessage)
        {
            this.Driver.WaitUntilElementIsFound(this.btnCommunationLogPopupMessage, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.btnCommunationLogPopupMessage, this.nmbtnCommunationLogPopupMessage);
            this.Driver.IsExpectedTextMatchWithActualText(this.btnCommunationLogPopupMessage, updateSuccessMessage, this.nmbtnCommunationLogPopupMessage);
        }

        public void IsUserAbletoViewCommuncationLogInput()
        {
            this.Driver.WaitForPageLoad();
            this.Driver.WaitUntilElementIsNoLongerFound(this.dimmerVisible, BaseConfiguration.LongTimeout, this.nminputCommuncationLog);
            this.Driver.WaitUntilElementIsFound(this.inputCommuncationLog, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.inputCommuncationLog, this.nminputCommuncationLog);
        }

        public void VerifySubmitCancelButtonDiplay(string beforeAfter)
        {
            if (beforeAfter == "before")
            {
                Assert.IsFalse(this.Driver.IsElementPresentOrNot(this.submitBtnCommuncationLog, this.nmSubmitButton, string.Empty));
                Assert.IsFalse(this.Driver.IsElementPresentOrNot(this.cancelBtnCommuncationLog, this.nmCancelButton, string.Empty));
            }
            else
            {
                Assert.IsTrue(this.Driver.IsElementPresentOrNot(this.submitBtnCommuncationLog, this.nmSubmitButton, string.Empty));
                Assert.IsTrue(this.Driver.IsElementPresentOrNot(this.cancelBtnCommuncationLog, this.nmCancelButton, string.Empty));
            }
        }

        public string VerifyUsersinCommunicationLogInput()
        {
            this.Driver.WaitUntilElementIsFound(this.taggeduserlist, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.taggeduserlist, this.nmtaggeduserlists);
            string getusername = this.Driver.GetText(this.taggeduserlist);
            this.Driver.IsElementClickable(this.taggeduserlist, this.nmtaggeduserlists);
            this.Driver.WaitForPageLoad();
            return getusername;
        }

        public void IsCancelButtonClicked()
        {
            this.Driver.WaitUntilElementIsFound(this.cancelBtnCommuncationLog, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.cancelBtnCommuncationLog, this.nmCancelButton);
            var webElement = this.Driver.GetElement(this.cancelBtnCommuncationLog);
            this.Driver.JavaScriptClick(webElement, this.nmCancelButton);
            this.Driver.WaitForPageLoad();
        }

        public void IsTaggedUserSavedAfterClickingCancelButton(string expected)
        {
            Assert.IsFalse(this.Driver.IsElementPresentOrNot(this.istaggedusersaved.Format(expected), string.Format(this.nmtaggeduserlist, expected), string.Empty));
        }

        public void IsTaggedUserSavedAfterClickingSubmitButton(string expected)
        {
            Assert.IsTrue(this.Driver.IsElementPresentOrNot(this.istaggedusersaved.Format(expected), string.Format(this.nmtaggeduserlist, expected), string.Empty));
        }
    }
}