// <copyright file="ApplicationManagementPage.cs" company="PlaceholderCompany">
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

    public class ApplicationManagementPage : ProjectPageBase
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
          sortedIConForApplicationName = new ElementLocator(Locator.CssSelector, "th.appName i.long.arrow.alternate.up.icon.activeSort");

        private readonly ElementLocator
          sortedIConForContactName = new ElementLocator(Locator.CssSelector, "th.contactName i.long.arrow.alternate.up.icon.activeSort");

        private readonly ElementLocator
            manageMembershipFAQs = new ElementLocator(Locator.CssSelector, "ul > li:nth-child(2)");

        private readonly ElementLocator
            manageMembershipDocuments = new ElementLocator(Locator.CssSelector, "ul > li: nth - child(3)");

        private readonly ElementLocator
            manageMemberClassifications = new ElementLocator(Locator.CssSelector, "ul > li: nth - child(4)");

        private readonly ElementLocator
            applicationNameLink = new ElementLocator(Locator.CssSelector, "td.appName a");

        private readonly ElementLocator
            applicationManagementEditBtn = new ElementLocator(Locator.XPath, "//a[@class = 'editBtn']");

        private readonly ElementLocator
            updateApplicationPopupWindowHeader = new ElementLocator(Locator.XPath, "//div[@class = 'header']");

        private readonly ElementLocator
            editApplicationName = new ElementLocator(Locator.Name, "appName");

        private readonly ElementLocator
            editContactName = new ElementLocator(Locator.Name, "contactName");

        private readonly ElementLocator
            editEmailId = new ElementLocator(Locator.Name, "email");

        private readonly ElementLocator
            popupClose = new ElementLocator(Locator.XPath, "//i[@class='close']");

        private readonly ElementLocator
            editApplicationError = new ElementLocator(Locator.XPath, "//label[text()='Application Name']/following-sibling::span");

        private readonly ElementLocator
            editContactApplicationError = new ElementLocator(Locator.XPath, "//label[text()='Contact Name']/following-sibling::span");

        private readonly ElementLocator
            editEmailApplicationError = new ElementLocator(Locator.XPath, "//label[text()='Email ID']/following-sibling::span");

        private readonly ElementLocator
            applicationNameColumn = new ElementLocator(Locator.CssSelector, "th.appName");

        private readonly ElementLocator
            applicationIdColumn = new ElementLocator(Locator.CssSelector, "th.appId");

        private readonly ElementLocator
            secureKeyColumn = new ElementLocator(Locator.CssSelector, "th.secureKey");

        private readonly ElementLocator
            contactNameColumn = new ElementLocator(Locator.CssSelector, "th.contactName");

        private readonly ElementLocator
            emailIdColumn = new ElementLocator(Locator.CssSelector, "th.emailId");

        private readonly ElementLocator
            statusColumn = new ElementLocator(Locator.CssSelector, "th.status");

        private readonly ElementLocator
            viewApplicationList = new ElementLocator(Locator.XPath, "//table[@class='customTable appTable']//tbody//tr");

        private readonly ElementLocator
            securekey = new ElementLocator(Locator.XPath, "//*[@name='secureKey' and @placeholder='Secure Key']");

        private readonly ElementLocator
            refreshSecureKey = new ElementLocator(Locator.XPath, "//div[@class='commonToolTip']");

        private readonly ElementLocator
          pageRefresh = new ElementLocator(Locator.XPath, "//body");

        private readonly ElementLocator
            statusActive = new ElementLocator(Locator.XPath, "//a[text()='{0}']/../following-sibling::td[@class='status']");

        private readonly ElementLocator
            sortIconOfApplicationName = new ElementLocator(Locator.CssSelector, "th.appName i.sort.icon");

        private readonly ElementLocator
            sortIconOfContactName = new ElementLocator(Locator.CssSelector, "th.contactName i.sort.icon");

        private readonly ElementLocator
            contactNamesort = new ElementLocator(Locator.CssSelector, "td.contactName");

        private readonly ElementLocator
            sortIconOfStatus = new ElementLocator(Locator.CssSelector, "th.status i.sort.icon");

        private readonly ElementLocator
            statussort = new ElementLocator(Locator.CssSelector, "td.status");

        private readonly ElementLocator
          sortedIConForStatus = new ElementLocator(Locator.CssSelector, "th.status i.long.arrow.alternate.up.icon.activeSort");

        private readonly ElementLocator
        sortIconOfApplicationId = new ElementLocator(Locator.CssSelector, "th.appId i.sort.icon");

        private readonly ElementLocator
            applicationIdsort = new ElementLocator(Locator.CssSelector, "td.appId");

        private readonly ElementLocator
          sortedIConForApplicationId = new ElementLocator(Locator.CssSelector, "th.appId i.long.arrow.alternate.up.icon.activeSort");

        private readonly ElementLocator
        sortIconOfEmailId = new ElementLocator(Locator.CssSelector, "th.emailId i.sort.icon");

        private readonly ElementLocator
            emailIdsort = new ElementLocator(Locator.CssSelector, "td.emailId");

        private readonly ElementLocator
          sortedIConForEmailId = new ElementLocator(Locator.CssSelector, "th.emailId i.long.arrow.alternate.up.icon.activeSort");

        private readonly ElementLocator
        sortIconOfSecureKey = new ElementLocator(Locator.CssSelector, "th.secureKey i.sort.icon");

        private readonly ElementLocator
            secureKeysort = new ElementLocator(Locator.CssSelector, "td.secureKey");

        private readonly ElementLocator
          sortedIConForSecureKey = new ElementLocator(Locator.CssSelector, "th.secureKey i.long.arrow.alternate.up.icon.activeSort");

        private string nmcustomerlogo = "Customer logo";
        private string nmmembership = "Memebership Management Menu";
        private string nmcommitteemanagement = "Committee Management Menu";
        private string nmapplicationmanagement = "Application Management Menu";
        private string nmmanagememebershiptype = "Manage Membership Type option";
        private string nmsortedIconOfApplicationName = "Application Name Sorted ICon";
        private string nmmanagemembershipFaqs = "Manage Membership FAQ's option";
        private string nmmanagemembershipdocument = "Manage Membership Documents option";
        private string nmmanagememeberclassifications = "Manage Member Classifications option";
        private string nmapplicationnamelink = "Application Name";
        private string nmaaplicationmanagementeditbtn = "Application Management Edit";
        private string nmupdateapplicationpopupwindowheader = "Update Application popup window";
        private string nmeditapplicationname = "Application Name field";
        private string nmeditcontactName = "Contact Name field";
        private string nmeditemail = "Email Field";
        private string nmclose = "Application Popup Close";
        private string nmapplicationnamecolumn = "Application Name Column";
        private string nmapplicationidcolumn = "Application Id Column";
        private string nmsecurekey = "Secure Key Column";
        private string nmcontactnamecolumn = "Contact Name Column";
        private string nmemailidcolumn = "Email Id Column";
        private string nmstatuscolumn = "Status Column";
        private string nmviewapplicationlist = "View Application List";
        private string nmrefreshsecurekey = "Refresh Secure Key";
        private string nmsortIconOfApplicationName = "Application Name Sort ICon";
        private string nmsortIconOfContactName = "Contact Name Sort ICon";
        private string nmsortedIconOfContactName = "Contact Name Sorted ICon";
        private string nmcontactNameSort = "Contact Name sort";
        private string nmsortIconOfStatus = "Status Sort ICon";
        private string nmsortedIconOfStatus = "Status Sorted ICon";
        private string nmstatusSort = "Status sort";
        private string nmsortIconOfApplicationId = "Application Id Sort ICon";
        private string nmsortedIconOfApplicationId = "Application Id Sorted ICon";
        private string nmapplicationIdSort = "Application Id sort";
        private string nmsortIconOfEmailId = "Email Id Sort ICon";
        private string nmsortedIconOfEmailId = "Email Id Sorted ICon";
        private string nmemailIdSort = "Email Id sort";
        private string nmsortIconOfSecureKey = "Secure Key Sort ICon";
        private string nmsortedIconOfSecureKey = "Secure Key Sorted ICon";
        private string nmsecureKeySort = "Secure Key sort";

        public ApplicationManagementPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        public void IsCustomerLogoDisplayed()
        {
            this.Driver.IsElementVisible(this.customerLogo, this.nmcustomerlogo);
        }

        public void IsMembershipManagementSectionClickable(string name)
        {
            this.Driver.IsElementVisible(this.membershipmenu.Format(name), this.nmmembership);
            this.Driver.IsElementClickable(this.membershipmenu.Format(name), this.nmmembership);
        }

        public void IsCommitteeManagementSectionClickable(string name)
        {
            this.Driver.IsElementVisible(this.committeemanagement.Format(name), this.nmcommitteemanagement);
            this.Driver.IsElementClickable(this.committeemanagement.Format(name), this.nmcommitteemanagement);
        }

        public void IsApplicationManagementSectionClickable(string name)
        {
            this.Driver.IsElementVisible(this.applicationManagement.Format(name), this.nmapplicationmanagement);
            this.Driver.IsElementClickable(this.applicationManagement.Format(name), this.nmapplicationmanagement);
        }

        public void IsManageMembershipTypeClickable(string name)
        {
            this.Driver.IsElementVisible(this.managemembershiptype.Format(name), this.nmmanagememebershiptype);
            this.Driver.IsElementClickable(this.managemembershiptype.Format(name), this.nmmanagememebershiptype);
        }

        public void AreListOfElementsDisplayInAlphabeticalOrderOrNot()
        {
            this.Driver.AreElementsSortedInAlphabeticalOrder(this.applicationNameLink, this.nmapplicationnamelink);
        }

        public void IsUserAbleToClickOnSortIconFromApplicationName()
        {
            this.Driver.WaitUntilElementIsFound(this.sortIconOfApplicationName, 90);
            this.Driver.IsElementClickable(this.sortIconOfApplicationName, this.nmsortIconOfApplicationName);
        }

        public void IsApplicationNameIConSortedSuccessfully()
        {
            this.Driver.WaitUntilElementIsFound(this.sortedIConForApplicationName, 90);
            this.Driver.IsElementVisible(this.sortedIConForApplicationName, this.nmsortedIconOfApplicationName);
        }

        public void IsManageMembershipFAQsClickable(string name)
        {
            this.Driver.IsElementVisible(this.manageMembershipFAQs.Format(name), this.nmmanagemembershipFaqs);
            this.Driver.IsElementClickable(this.manageMembershipFAQs.Format(name), this.nmmanagemembershipFaqs);
        }

        public void IsManageMembershipDocumentsClickable(string name)
        {
            this.Driver.IsElementVisible(this.manageMembershipDocuments.Format(name), this.nmmanagemembershipdocument);
            this.Driver.IsElementClickable(this.manageMembershipDocuments.Format(name), this.nmmanagemembershipdocument);
        }

        public void IsManageMembershipClassificationsClickable(string name)
        {
            this.Driver.IsElementVisible(this.manageMemberClassifications.Format(name), this.nmmanagememeberclassifications);
            this.Driver.IsElementClickable(this.manageMemberClassifications.Format(name), this.nmmanagememeberclassifications);
        }

        public void IsUserAbleToSelectApplicationNameFromListOfApplications(int i)
        {
            this.Driver.WaitUntilElementIsFound(this.applicationNameLink, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisibleFromListOfElement(this.applicationNameLink, this.nmapplicationnamelink);
            this.Driver.IsElementClickableFromListOfElemetsBasedOnIndex(this.applicationNameLink, this.nmapplicationnamelink, i);
        }

        public void IsEditApplicationButtonClickable()
        {
            this.Driver.IsElementVisible(this.applicationManagementEditBtn, this.nmaaplicationmanagementeditbtn);
            this.Driver.IsElementClickable(this.applicationManagementEditBtn, this.nmaaplicationmanagementeditbtn);
        }

        public void IsUpdatePopUpWindowVisible()
        {
            this.Driver.WaitUntilElementIsFound(this.updateApplicationPopupWindowHeader, BaseConfiguration.LongTimeout);
            this.Driver.IsElementVisible(this.updateApplicationPopupWindowHeader, this.nmupdateapplicationpopupwindowheader);
        }

        public void IsUserAbletoEditApplicationName(string name)
        {
            this.Driver.WaitForPageLoad();
            this.Driver.IsElementVisible(this.editApplicationName.Format(name), this.nmeditapplicationname);
            this.Driver.EnterText(this.editApplicationName.Format(name), name, this.nmeditapplicationname);
        }

        public void IsUserAbletoEditContactName(string name)
        {
            this.Driver.IsElementVisible(this.editContactName.Format(name), this.nmeditcontactName);
            this.Driver.EnterText(this.editContactName.Format(name), name, this.nmeditcontactName);
        }

        public void IsUserAbletoEditEmailID(string name)
        {
            this.Driver.IsElementVisible(this.editEmailId.Format(name), this.nmeditemail);
            this.Driver.EnterText(this.editEmailId.Format(name), name, this.nmeditemail);
        }

        public void AreEditApplicationValidationMessagesDisplayed(string appError, string contactError, string emailError)
        {
            this.IsApplicationNameErrorMsgDisplayed(appError);
            this.IsApplicationContactNameErrorMsgDisplayed(contactError);
            this.IsApplicationEmialIdErrorMsgDisplayed(emailError);
        }

        public void IsApplicationNameErrorMsgDisplayed(string expectedApplicationErrormsg)
        {
            var actualApplicationNameErrortext = this.Driver.GetText(this.editApplicationError);
            this.Driver.IsExpectedTextMatchWithActualText(this.editApplicationError, expectedApplicationErrormsg, actualApplicationNameErrortext);
        }

        public void IsApplicationContactNameErrorMsgDisplayed(string expectedApplicationErrormsg)
        {
            var actualApplicationContactNameErrortext = this.Driver.GetText(this.editContactApplicationError);
            this.Driver.IsExpectedTextMatchWithActualText(this.editContactApplicationError, expectedApplicationErrormsg, actualApplicationContactNameErrortext);
        }

        public void IsApplicationEmialIdErrorMsgDisplayed(string expectedApplicationErrormsg)
        {
            var actualApplicationEmailIdErrortext = this.Driver.GetText(this.editEmailApplicationError);
            this.Driver.IsExpectedTextMatchWithActualText(this.editEmailApplicationError, expectedApplicationErrormsg, actualApplicationEmailIdErrortext);
        }

        public void IsUserAbleCloseEditApplicationPopupWindow()
        {
            this.Driver.WaitUntilElementIsFound(this.popupClose, 90);
            this.Driver.IsElementVisible(this.popupClose, this.nmclose);
            this.Driver.IsElementClickable(this.popupClose, this.nmclose);
        }

        public void IsUserAbleToViewApplicationListColumns()
        {
            this.Driver.IsElementVisible(this.applicationNameColumn, this.nmapplicationnamecolumn);
            this.Driver.IsElementVisible(this.applicationIdColumn, this.nmapplicationidcolumn);
            this.Driver.IsElementVisible(this.secureKeyColumn, this.nmsecurekey);
            this.Driver.IsElementVisible(this.contactNameColumn, this.nmcontactnamecolumn);
            this.Driver.IsElementVisible(this.emailIdColumn, this.nmemailidcolumn);
            this.Driver.IsElementVisible(this.statusColumn, this.nmstatuscolumn);
        }

        public void IsUserAbleToViewApplicationList()
        {
            try
            {
                this.Driver.IsElementVisibleFromListOfElement(this.viewApplicationList, this.nmviewapplicationlist);
                ICollection<IWebElement> itemCount = this.Driver.GetElements(this.viewApplicationList);
                if (itemCount.Count > 0)
                {
                    Logger.Info(itemCount.Count + " element visible successfully");
                    DriverContext.ExtentStepTest.Log(LogStatus.Pass, "To verify application List is visible", "the list of Application count is " + itemCount.Count + " displayed successfully");
                    Logger.Info(itemCount.Count + " are displayed/Enabled successfully");
                }
                else
                {
                    throw new Exception("No Records Found in Application List");
                }
            }
            catch (Exception e)
            {
                Logger.Error("An exception occured while waiting for the element to become visible " + e.ToString());
                DriverContext.ExtentStepTest.Log(LogStatus.Fail, "To verify  Application count is not visible with in provided time " + BaseConfiguration.ShortTimeout, "An exception occurred waiting for application copunt to become visible");
                throw;
            }
        }

        public void IsUserAbleSeeSecureKeyChangeAfterRefreshAndNotMatchWithOldKey()
        {
            var beforeRefresh = this.Driver.GetValueFromDisabledElement(this.securekey);

            this.Driver.IsElementClickable(this.refreshSecureKey, this.nmrefreshsecurekey);

            var afterRefresh = this.Driver.GetValueFromDisabledElement(this.securekey);

            Assert.AreNotEqual(beforeRefresh, afterRefresh);
        }

        public string IsUserAbleToViewApplicationManagementListInLifoOrder(int i, string expected)
        {
            var actualapplication = this.Driver.GetTextFromListOfElementsBasedOnIndex(this.applicationNameLink, this.nmapplicationnamelink, i);
            this.Driver.IsExpectedTextMatchWithActualText(this.applicationNameLink, expected, actualapplication);
            return actualapplication;
        }

        public void IsUserAbleToViewNewApplicationIsByDefaultActiveStatus(string expected, string applicationLink)
        {
                var actualStatus = this.Driver.GetText(this.statusActive.Format(applicationLink));
                this.Driver.IsExpectedTextMatchWithActualText(this.statusActive.Format(applicationLink), expected, actualStatus);
        }

        public void IsUserAbleToClickOnSortIconFromContactName()
        {
            this.Driver.WaitUntilElementIsFound(this.sortIconOfContactName, 90);
            this.Driver.IsElementVisible(this.sortIconOfContactName, this.nmsortIconOfContactName);
            var webElement = this.Driver.GetElement(this.sortIconOfContactName);
            this.Driver.JavaScriptClick(webElement, this.nmsortIconOfContactName);
        }

        public void AreListOfElementsDisplayInAlphabeticalOrderOrNotForContactName()
        {
            this.Driver.AreElementsSortedInAlphabeticalOrder(this.contactNamesort, this.nmcontactNameSort);
        }

        public void IsContactNameIConSortedSuccessfully()
        {
            this.Driver.WaitUntilElementIsFound(this.sortedIConForContactName, 90);
            this.Driver.IsElementVisible(this.sortedIConForContactName, this.nmsortedIconOfContactName);
        }

        public void IsUserAbleToClickOnSortIconFromStatus()
        {
            this.Driver.WaitUntilElementIsFound(this.sortIconOfStatus, 90);
            this.Driver.IsElementVisible(this.sortIconOfStatus, this.nmsortIconOfStatus);
            var webElement = this.Driver.GetElement(this.sortIconOfStatus);
            this.Driver.JavaScriptClick(webElement, this.nmsortedIconOfStatus);
        }

        public void AreListOfElementsDisplayInAlphabeticalOrderOrNotForStatus()
        {
            this.Driver.AreElementsSortedInAlphabeticalOrder(this.statussort, this.nmstatusSort);
        }

        public void IsStatusIConSortedSuccessfully()
        {
            this.Driver.WaitUntilElementIsFound(this.sortedIConForStatus, 90);
            this.Driver.IsElementVisible(this.sortedIConForStatus, this.nmsortedIconOfStatus);
        }

        public void IsUserAbleToClickOnSortIconFromApplicationId()
        {
            this.Driver.WaitUntilElementIsFound(this.sortIconOfApplicationId, 90);
            this.Driver.IsElementVisible(this.sortIconOfApplicationId, this.nmsortIconOfApplicationId);
            var webElement = this.Driver.GetElement(this.sortIconOfApplicationId);
            this.Driver.JavaScriptClick(webElement, this.nmsortIconOfApplicationId);
        }

        public void AreListOfElementsDisplayInAlphabeticalOrderOrNotForApplicationId()
        {
            this.Driver.AreElementsSortedInAlphabeticalOrder(this.applicationIdsort, this.nmapplicationIdSort);
        }

        public void IsApplicationIdIConSortedSuccessfully()
        {
            this.Driver.WaitUntilElementIsFound(this.sortedIConForApplicationId, 90);
            this.Driver.IsElementVisible(this.sortedIConForApplicationId, this.nmsortedIconOfApplicationId);
        }

        public void IsUserAbleToClickOnSortIconFromEmailId()
        {
            this.Driver.WaitUntilElementIsFound(this.sortIconOfEmailId, 90);
            this.Driver.IsElementVisible(this.sortIconOfEmailId, this.nmsortIconOfEmailId);
            var webElement = this.Driver.GetElement(this.sortIconOfEmailId);
            this.Driver.JavaScriptClick(webElement, this.nmsortIconOfEmailId);
        }

        public void AreListOfElementsDisplayInAlphabeticalOrderOrNotForEmailId()
        {
            this.Driver.AreElementsSortedInAlphabeticalOrder(this.emailIdsort, this.nmemailIdSort);
        }

        public void IsEmailIdIConSortedSuccessfully()
        {
            this.Driver.WaitUntilElementIsFound(this.sortedIConForEmailId, 90);
            this.Driver.IsElementVisible(this.sortedIConForEmailId, this.nmsortedIconOfEmailId);
        }

        public void IsUserAbleToClickOnSortIconFromSecureKey()
        {
            this.Driver.WaitUntilElementIsFound(this.sortIconOfSecureKey, 90);
            this.Driver.IsElementVisible(this.sortIconOfSecureKey, this.nmsortIconOfSecureKey);
            var webElement = this.Driver.GetElement(this.sortIconOfSecureKey);
            this.Driver.JavaScriptClick(webElement, this.nmsortIconOfSecureKey);
        }

        public void AreListOfElementsDisplayInAlphabeticalOrderOrNotForSecureKey()
        {
            this.Driver.AreElementsSortedInAlphabeticalOrder(this.secureKeysort, this.nmsecureKeySort);
        }

        public void IsSecureKeyIConSortedSuccessfully()
        {
            this.Driver.WaitUntilElementIsFound(this.sortedIConForSecureKey, 90);
            this.Driver.IsElementVisible(this.sortedIConForSecureKey, this.nmsortedIconOfSecureKey);
        }
    }
}
